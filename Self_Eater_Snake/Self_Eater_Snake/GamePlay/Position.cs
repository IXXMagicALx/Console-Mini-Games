using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 位置结构体
    /// </summary>
    struct Position
    {
        public int x;
        public int y;
        /// <summary>
        /// 位置构造函数
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        public Position(int x, int y)
        {
            this.x = x; 
            this.y = y;
        }
        //判断位置是否相等
        //相等运算符重载
        public static bool operator == (Position p1, Position p2)
        {
            if(p1.x == p2.x && p1.y == p2.y)
            {
                return true;
            }
            return false;
        }
        public static bool operator != (Position p1, Position p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
            {
                return false;
            }
            return true;
        }
    }
}
