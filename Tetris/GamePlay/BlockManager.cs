using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 旋转方式(顺时针or逆时针)
    /// </summary>
    enum E_RotateType
    {
        Left,
        Right,
    }
    /// <summary>
    /// 水平移动方式
    /// </summary>
    enum E_MoveType
    {
        Left,
        Right,
    }
    internal class BlockManager : IDraw
    {
        public static Random r = new Random();
        //当前方块类型信息
        private BlockInfo nowBlockInfo;
        //当前方块旋转形态的索引
        private int nowBlockIndex;
        //下一个方块的类型
        public E_ObjectType nextBlockType; 
        //四个要来摆放的方块
        private List<Object> blocks;
        //方块种类与信息的键值对
        public Dictionary<E_ObjectType, BlockInfo> blockTypeInfo;
        /// <summary>
        /// 初始化要打印的方块
        /// </summary>
        public BlockManager()
        {
            blockTypeInfo = new Dictionary<E_ObjectType, BlockInfo>()
            {
                {E_ObjectType.Cube , new BlockInfo(E_ObjectType.Cube)},
                {E_ObjectType.Line , new BlockInfo(E_ObjectType.Line)},
                {E_ObjectType.Tank , new BlockInfo(E_ObjectType.Tank)},
                {E_ObjectType.Left_Z , new BlockInfo(E_ObjectType.Left_Z)},
                {E_ObjectType.Right_Z , new BlockInfo(E_ObjectType.Right_Z)},
                {E_ObjectType.Left_L , new BlockInfo(E_ObjectType.Left_L)},
                {E_ObjectType.Right_L , new BlockInfo(E_ObjectType.Right_L)},
            };
            //第一次生成方块
            nextBlockType = (E_ObjectType)r.Next(1, 8);
            //随机生成方块
            RandomCreateBlock();
        }
        /// <summary>
        /// 随机生成方块的方法
        /// </summary>
        public void RandomCreateBlock()
        {
            //随机出的这一个要生成的方块
            E_ObjectType type = nextBlockType;
            //创建新的四个小正方形
            blocks = new List<Object>()
            {
                new Object(type),
                new Object(type),
                new Object(type),
                new Object(type),
            };
            //初始化原点位置
            blocks[0].position = new Position(22, -5);
            //存储现在方块具体的类型信息
            nowBlockInfo = blockTypeInfo[type];
            //随机方块的形态索引并存储
            nowBlockIndex = r.Next(0, nowBlockInfo.Count);
            //取出对应形态所有方格的坐标
            Position[] positions = nowBlockInfo[nowBlockIndex];
            //从1开始遍历因为0是原点
            for (int i = 0; i < positions.Length; i++)
            {
                //其他方格的坐标 = 原点坐标 + 方格与原点的相对坐标
                blocks[i + 1].position = blocks[0].position + positions[i];
            }
            //生成下一个方块类型并记录(用于HUD中提示下一个方块)
            nextBlockType = (E_ObjectType)r.Next(1, 8);
        }
        
        #region 旋转相关
        /// <summary>
        /// 判断能否旋转
        /// </summary>
        /// <param name="rotateType">旋转方式</param>
        /// <param name="map">地图信息，判断是否出界</param>
        /// <returns>是否能旋转</returns>
        public bool JudgeRotate(E_RotateType rotateType, Map map)
        {
            //模拟旋转一次来判断是否能旋转
            //用一个临时变量来记录当前索引
            //变化临时变量从而不改变当前索引
            int nowIndex = nowBlockIndex;
            //进行模拟旋转
            switch (rotateType)
            {
                case E_RotateType.Left:
                    nowIndex--;
                    if (nowIndex < 0)
                        nowIndex = nowBlockInfo.Count - 1;
                    break;
                case E_RotateType.Right:
                    nowIndex++;
                    if (nowIndex >= nowBlockInfo.Count)
                        nowIndex = 0;
                    break;
            }
            //通过临时索引取出形态信息用于判断
            Position[] positions = nowBlockInfo[nowIndex];
            //判断超出地图边界
            Position tempPosition;
            //得到其他方格在地图上的实际坐标并判断
            for (int i = 0; i < positions.Length; i++)
            {
                tempPosition = blocks[0].position + positions[i];
                if (tempPosition.x < 2 || tempPosition.x >= map.width || tempPosition.y >= map.height)
                {
                    return false;
                }
            }
            //判断与动态墙壁重合
            for (int i = 0; i < positions.Length; i++)
            {
                tempPosition = blocks[0].position + positions[i];
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (tempPosition == map.dynamicWalls[j].position)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 实现方块的旋转
        /// </summary>
        /// <param name="rotateType">旋转类型(顺时针or逆时针)</param>
        public void Rotate(E_RotateType rotateType)
        {
            //先清除旋转前的方块
            CleanDraw();

            //进行旋转
            switch (rotateType)
            {
                case E_RotateType.Left:
                    nowBlockIndex--;
                    if (nowBlockIndex < 0)
                        nowBlockIndex = nowBlockInfo.Count - 1;
                    break;
                case E_RotateType.Right:
                    nowBlockIndex++;
                    if (nowBlockIndex >= nowBlockInfo.Count)
                        nowBlockIndex = 0;
                    break;
            }
            //得到索引用于设置其他三个方块位置
            Position[] positions = nowBlockInfo[nowBlockIndex];
            //从1开始遍历因为0是原点
            for (int i = 0; i < positions.Length; i++)
            {
                //其他方格的坐标 = 原点坐标 + 方格与原点的相对坐标
                blocks[i + 1].position = blocks[0].position + positions[i];
            }

            //打印旋转后的方块
            Draw();
        }
        #endregion

        #region 水平移动相关
        /// <summary>
        /// 判断能否水平移动
        /// </summary>
        /// <param name="moveType">移动方向</param>
        /// <param name="map">地图</param>
        /// <returns></returns>
        public bool JudgeMoveLR(E_MoveType moveType , Map map)
        {
            //通过模拟移动来判断是否能移动
            Position moveLRStep = new Position(moveType == E_MoveType.Left ? -2 : 2, 0);
            //遍历进行模拟移动 并
            //判断超出地图边界
            Position tempPosition;
            for (int i = 0; i < blocks.Count; i++)
            {
                tempPosition = blocks[i].position + moveLRStep;
                
                if(tempPosition.x < 2 || tempPosition.x >= map.width)
                {
                    return false;
                }
            }
            //判断与动态墙壁重合
            for (int i = 0 ; i < blocks.Count ; i++)
            {
                tempPosition = blocks[i].position + moveLRStep;
                
                for(int j = 0 ; j < map.dynamicWalls.Count ; j++)
                {
                    if(tempPosition == map.dynamicWalls[j].position)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 方块水平移动方法
        /// </summary>
        /// <param name="moveType">移动方向</param>
        public void MoveLR(E_MoveType moveType)
        {
            //先清除旧的
            CleanDraw();

            //水平移动 左移-2，右移+2
            Position moveLRStep = new Position(moveType == E_MoveType.Left ? -2 : 2, 0);
            //遍历四个方格让四个方格都移动
            for(int i = 0 ; i < blocks.Count ; i++)
            {
                blocks[i].position += moveLRStep;
            }

            //打印移动后的方块
            Draw();
        }
        #endregion

        #region 方块自动向下移动
        /// <summary>
        /// 判断能否向下移动
        /// </summary>
        /// <param name="map">地图信息</param>
        /// <returns>判断能否向下移动的结果</returns>
        public bool JudgeAutoMove(Map map)
        {
            //通过模拟向下移动来判断能否向下移动
            Position tempPosition;
            Position autoMoveStep = new Position(0, 1);
            //判断是否会与地图下边界重合
            for (int i = 0 ; i < blocks.Count ; i++)
            {
                tempPosition = blocks[i].position + autoMoveStep;
                if(tempPosition.y >= map.height)
                {
                    //方块变成动态墙壁
                    map.AddWalls(blocks);
                    //生成新的
                    RandomCreateBlock();
                    return false;
                }
            }
            //判断是否会与动态墙壁重合
            for (int i = 0; i < blocks.Count; i++)
            {
                tempPosition = blocks[i].position + autoMoveStep;
                for(int j = 0 ; j < map.dynamicWalls.Count ; j++)
                {
                    if(tempPosition == map.dynamicWalls[j].position)
                    {
                        //方块变成动态墙壁
                        map.AddWalls(blocks);
                        //生成新的
                        RandomCreateBlock();
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 自动向下移动
        /// </summary>
        public void AutoMove()
        {
            //清除旧的
            CleanDraw();

            Position autoMoveStep = new Position(0, 1);
            for(int i = 0 ; i < blocks.Count ; i++)
            {
                blocks[i].position += autoMoveStep;
            }

            //打印新的
            Draw();
        }
        #endregion
        /// <summary>
        /// 打印方块的方法
        /// </summary>
        public void Draw()
        {
            for(int i = 0 ; i < blocks.Count ; i++)
            {
                blocks[i].Draw();
            }
        }
        /// <summary>
        /// 实现清除方块打印的方法
        /// </summary>
        public void CleanDraw()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].CleanDraw();
            }
        }
    }
}
