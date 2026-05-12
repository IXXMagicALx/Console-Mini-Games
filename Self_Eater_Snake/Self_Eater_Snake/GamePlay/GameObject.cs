using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 游戏对象类
    /// </summary>
    abstract internal class GameObject
    {
        public Position pos;
        public abstract void Draw();
    }
}
