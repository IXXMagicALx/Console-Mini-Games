using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 蛇身体类型
    /// </summary>
    enum E_SnakeBody_Type
    {
        /// <summary>
        /// 蛇头
        /// </summary>
        Haed,
        /// <summary>
        /// 蛇身
        /// </summary>
        Body,
    }
    /// <summary>
    /// 蛇身体类
    /// </summary>
    internal class SnakeBody : GameObject
    {
        E_SnakeBody_Type bodyType = E_SnakeBody_Type.Haed;
        /// <summary>
        /// 蛇身体构造函数
        /// </summary>
        /// <param name="bodyType">身体类型</param>
        /// <param name="x">打印横坐标</param>
        /// <param name="y">打印纵坐标</param>
        public SnakeBody(E_SnakeBody_Type bodyType,int x,int y)
        {
            this.bodyType = bodyType;
            pos.x = x; 
            pos.y = y;
        }
        /// <summary>
        /// 蛇身体打印的方法
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = (bodyType == E_SnakeBody_Type.Haed ? ConsoleColor.Yellow : ConsoleColor.Green);
            Console.Write(bodyType == E_SnakeBody_Type.Haed ? "◎" : "¤");
        }
    }
}
