using System;
using System.Collections.Generic;
using System.Text;
using Self_Eater_Snake.GamePlay;

namespace Self_Eater_Snake.GameBasic
{
    /// <summary>
    /// 结束场景类
    /// </summary>
    internal class EndScene : BeginOrEndBasic
    {
        /// <summary>
        /// 结束场景构造函数
        /// </summary>
        /// <param name="score">最终得分</param>
        public EndScene(int score)
        {
            title = "∥ Game Over ∥";
            subtitle = "本局得分:" + score;
            option1 = "-回到主界面";
        }
        /// <summary>
        /// 结束场景按下J
        /// </summary>
        public override void Press_J_DoSomething()
        {
            if (0 == nowSelect)
            {
                Game.ChangeScene(E_SceneType.Begin);
            }
            else
            {
                Environment.Exit(0);
            }

        }
    }
}
