using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GameBasic
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
             title = "∥ Self Eater Snake ∥";
            subtitle = " ";
             option1 = "-开始游戏";
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
