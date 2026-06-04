using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    internal class BlockInfo
    {
        //列表存储方块形态
        //列表里的坐标数组存储每种形态中除原点外所有方格所在的坐标
        public List<Position[]> blockPatterns;
        /// <summary>
        /// 获取形态的数量来判断旋转该怎么变化
        /// </summary>
        public int Count { get => blockPatterns.Count; }


        /// <summary>
        /// 索引器，提供给外部方便根据索引快速获取方块样式每个格子的位置信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Position[] this[int index]
        {
            get
            {
                if (index < 0)
                    return blockPatterns[0];
                else if (index >= blockPatterns.Count)
                    return blockPatterns[blockPatterns.Count - 1];
                else
                    return blockPatterns[index];
            }
        }

        /// <summary>
        /// 初始化各种方块的样式
        /// </summary>
        /// <param name="objectType">方块样式</param>
        public BlockInfo(E_ObjectType objectType)
        {
            blockPatterns = new List<Position[]>();

            switch (objectType)
            {
                case E_ObjectType.Cube:
                    blockPatterns.Add(new Position[] { 
                               new Position(2, 0), 
                               new Position(0, 1), 
                               new Position(2, 1) });
                    break;
                case E_ObjectType.Line:
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(0, 1), 
                               new Position(0, 2) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-4, 0), 
                               new Position(-2, 0), 
                               new Position(2, 0) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -2), 
                               new Position(0, -1), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, 0), 
                               new Position(2, 0), 
                               new Position(4, 0) });
                    break;
                case E_ObjectType.Tank:
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, 0), 
                               new Position(2, 0), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(-2, 0), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(-2, 0), 
                               new Position(2, 0) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(2, 0), 
                               new Position(0, 1) });
                    break;
                case E_ObjectType.Left_Z:
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(-2, 0), 
                               new Position(-2, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, -1), 
                               new Position(0, -1), 
                               new Position(2, 0) });
                    blockPatterns.Add(new Position[] { 
                               new Position(2, -1), 
                               new Position(2, 0), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, 0), 
                               new Position(0, 1), 
                               new Position(2, 1) });
                    break;
                case E_ObjectType.Right_Z:
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(2, 0), 
                               new Position(2, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(2, 0), 
                               new Position(-2, 1), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, -1), 
                               new Position(-2, 0), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(2, -1), 
                               new Position(-2, 0) });
                    break;
                case E_ObjectType.Left_L:
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, -1), 
                               new Position(0, -1), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(2, 1), 
                               new Position(-2, 0), 
                               new Position(2, 0) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(0, 1), 
                               new Position(2, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, 0), 
                               new Position(2, 0), 
                               new Position(-2, 1) });
                    break;
                case E_ObjectType.Right_L:
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(2, -1), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, 0), 
                               new Position(2, 0), 
                               new Position(2, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(0, -1), 
                               new Position(-2, 1), 
                               new Position(0, 1) });
                    blockPatterns.Add(new Position[] { 
                               new Position(-2, -1), 
                               new Position(-2, 0), 
                               new Position(2, 0) });
                    break;
            }
        }

        

        
    }
}
