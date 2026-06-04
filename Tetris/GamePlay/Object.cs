using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 方块种类
    /// </summary>
    enum E_ObjectType
    {
        /// <summary>
        /// 墙壁
        /// </summary>
        Wall,
        /// <summary>
        /// 四方格
        /// </summary>
        Cube,
        /// <summary>
        /// 四格直线
        /// </summary>
        Line,
        /// <summary>
        /// 四格T行
        /// </summary>
        Tank,
        /// <summary>
        /// 左Z形
        /// </summary>
        Left_Z,
        /// <summary>
        /// 右Z形
        /// </summary>
        Right_Z,
        /// <summary>
        /// 左L形
        /// </summary>
        Left_L,
        /// <summary>
        /// 右L形
        /// </summary>
        Right_L,
    }
    internal class Object : IDraw
    {
        //方格的类型
        public E_ObjectType objectType;
        //方格坐标信息
        public Position position;
        /// <summary>
        /// 根据类型初始化
        /// </summary>
        /// <param name="objectType">要初始化方块的类型</param>
        public Object(E_ObjectType objectType)
        {
            this.objectType = objectType;
        }
        /// <summary>
        /// 根据类型和坐标初始化
        /// </summary>
        /// <param name="objectType">要初始化方块的类型</param>
        /// <param name="x">初始化方块的横坐标</param>
        /// <param name="y">初始化方块的纵坐标</param>
        public Object(E_ObjectType objectType , int x, int y) : this(objectType)
        {
            this.position = new Position(x, y); 
        }
        /// <summary>
        /// 实现单个小方格的打印
        /// </summary>
        public void Draw()
        {
            //在窗口外不画但是还有，只是不画出来，移到窗口内就画了
            if (position.y < 0)
                return;

            Console.SetCursorPosition(position.x,position.y);
            switch (objectType)
            {
                case E_ObjectType.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_ObjectType.Cube:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_ObjectType.Line:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_ObjectType.Tank:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case E_ObjectType.Left_Z:
                case E_ObjectType.Right_Z:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_ObjectType.Left_L:
                case E_ObjectType.Right_L:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.Write("■");
        }
        /// <summary>
        /// 实现单个小方格的清除打印
        /// </summary>
        public void CleanDraw()
        {
            //在窗口内才需要清
            if (position.y < 0)
                return;
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(" ");

        }
        /// <summary>
        /// 实现类型切换(如普通方格到底部变成墙)
        /// </summary>
        /// <param name="objectType">要变成的类型</param>
        public void ChangeType(E_ObjectType objectType)
        {
            this.objectType = objectType;
        }
    }
}
