using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 墙类
    /// </summary>
    internal class Wall : GameObject
    {
        /// <summary>
        /// 墙构造函数 用于初始化每一个格的坐标
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        public Wall(int x,int y)
        {
            pos = new Position(x, y);
        }
        /// <summary>
        /// 画墙每一个格子的方法
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
        }
    }
}
