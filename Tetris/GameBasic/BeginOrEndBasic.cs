using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 开始场景和结束场景的父类
    /// </summary>
    abstract internal class BeginOrEndBasic : ISceneUpdate
    {
        protected int nowSelect = 0;
        protected string title = "标题";
        protected string subtitle = "副标题";
        protected string option1 = "选项一";
        /// <summary>
        /// 按下J的交互
        /// </summary>
        public abstract void Press_J_DoSomething();
        /// <summary>
        /// 帧更新方法
        /// </summary>
        public void Update()
        {
            //开始场景和结束场景的逻辑
            Console.ForegroundColor = ConsoleColor.White;
            //打印标题
            Console.SetCursorPosition(Game.width/2 - title.Length,15);
            Console.Write(title);
            //打印副标题
            Console.SetCursorPosition(Game.width / 2 - subtitle.Length + 2,16);
            Console.Write(subtitle);
            //打印第一个选项
            Console.SetCursorPosition(Game.width / 2 - option1.Length, 18);
            Console.ForegroundColor = (nowSelect == 0 ? ConsoleColor.Red : ConsoleColor.White);
            Console.Write(option1);
            //打印第二个选项
            Console.SetCursorPosition(Game.width / 2 - 4, 19);
            Console.ForegroundColor = (nowSelect == 1 ? ConsoleColor.Red : ConsoleColor.White);
            Console.Write("退出游戏");
            //检测输入
            switch(Console.ReadKey(true).Key)
            {
                case ConsoleKey.W:
                    nowSelect--;
                    if(nowSelect <= 0)
                    {
                        nowSelect = 0;
                    }
                    break;
                case ConsoleKey.S:
                    nowSelect++;
                    if(nowSelect >= 1)
                    {
                        nowSelect = 1;
                    }
                    break;
                case ConsoleKey.J:
                    Press_J_DoSomething();
                    break;
            }
        }
    }
}
