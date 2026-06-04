using System;
using System.Collections.Generic;
using System.Text;


namespace Tetris
{
    internal class HUD:IDraw
    {
        BlockManager blockManager;
        Map map;

        //存储方块类型信息
        BlockInfo blockPromptInfo;
        //记录当前显示的是什么方块，用于打印时判断
        E_ObjectType lastDisplayedType;

        //提示变量
        List <Object> blocksPrompt;
        string rotateButtonPrompt;
        string moveButtonPrompt;
        string speedButtonPrompt;
        public HUD(Map map , BlockManager blockManager)
        {
            this.map = map;
            this.blockManager = blockManager;
            lastDisplayedType = (E_ObjectType)0;

            #region 固定打印部分

            //下一个文字提示
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(6, map.height + 2);
            Console.Write("下一个方块是：");

            //打印HUD分界线
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = map.height + 1; i < Game.height - 1; i++)
            {
                Console.SetCursorPosition(22, i);
                Console.Write("||");
            }
            //四行提示
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(26, map.height + 2);
            Console.Write("目前得分：" + GameScene.score);

            rotateButtonPrompt = "← → 逆/顺时针旋转";
            Console.SetCursorPosition(26, map.height + 3);
            Console.Write(rotateButtonPrompt);

            moveButtonPrompt = "A/D 左/右水平移动";
            Console.SetCursorPosition(26, map.height + 4);
            Console.Write(moveButtonPrompt);

            speedButtonPrompt = "S 加快下落速度";
            Console.SetCursorPosition(26, map.height + 5);
            Console.Write(speedButtonPrompt);

            #endregion

        }
        /// <summary>
        /// 打印变化的“下一个方块”的提示
        /// </summary>
        public void Draw()
        {
            //只有在产生新的方块的时候才进行 清理——修改——打印
            //能够减少性能浪费
            if(blockManager.nextBlockType != lastDisplayedType)
            {
                //如果有则清理旧的提示信息
                if (blocksPrompt != null)
                {
                    for (int i = 0; i < blocksPrompt.Count; i++)
                    {
                        blocksPrompt[i].CleanDraw();
                    }

                }

                //提示下一个方块的信息
                //根据nextBlockType进行初始化
                blocksPrompt = new List<Object>()
            {
                new Object(blockManager.nextBlockType),
                new Object(blockManager.nextBlockType),
                new Object(blockManager.nextBlockType),
                new Object(blockManager.nextBlockType),

            };
                //初始化原点的位置
                blocksPrompt[0].position = new Position(10, map.height + 4);
                //存储现在方块具体的类型信息
                blockPromptInfo = blockManager.blockTypeInfo[blockManager.nextBlockType];
                //取出一或四种形态中的一种所有方格的坐标
                Position[] positions = blockPromptInfo[0];
                //从1开始遍历因为0是原点
                for (int i = 0; i < positions.Length; i++)
                {
                    //其他方格的坐标 = 原点坐标 + 方格与原点的相对坐标
                    blocksPrompt[i + 1].position = blocksPrompt[0].position + positions[i];
                }

                //打印提示信息
                for (int i = 0; i < blocksPrompt.Count; i++)
                {
                    blocksPrompt[i].Draw();
                }
                //记录本次显示的类型
                lastDisplayedType = blockManager.nextBlockType;
            }

        }
    }
}
