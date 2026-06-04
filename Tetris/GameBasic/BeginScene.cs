using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 开始场景类
    /// </summary>
    internal class BeginScene : BeginOrEndBasic
    {
        /// <summary>
        /// 开始场景构造函数
        /// </summary>
        public BeginScene()
        {
             title = "俄罗斯方块";
            subtitle = " ";
             option1 = "开始游戏";
        }
        /// <summary>
        /// 开始场景按下J
        /// </summary>
        public override void Press_J_DoSomething()
        {
           if( 0 == nowSelect)
           {
                Game.ChangeScene(E_SceneType.Game);
           }
           else
           {
                Environment.Exit(0);
           }
            
        }
    }
}
