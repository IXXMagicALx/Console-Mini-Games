using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    struct Position
    {
        public int x, y;

        public Position( int x , int y )
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 对坐标判断相等
        /// </summary>
        /// <param name="p1">坐标1</param>
        /// <param name="p2">坐标2</param>
        /// <returns>判断结果</returns>
        public static bool operator == (Position p1 , Position p2)
        {
            if( p1.x == p2.x && p1.y == p2.y )
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 对坐标判断不等
        /// </summary>
        /// <param name="p1">坐标1</param>
        /// <param name="p2">坐标2</param>
        /// <returns>判断结果</returns>
        public static bool operator !=( Position p1, Position p2 )
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 坐标相加
        /// </summary>
        /// <param name="p1">坐标1</param>
        /// <param name="p2">坐标2</param>
        /// <returns>相加结果</returns>
        public static Position operator +( Position p1 , Position p2 )
        {
            Position add = new Position(p1.x + p2.x , p1.y + p2.y);
            return add;
        }
    }
}
