using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 游戏帧更新接口
    /// </summary>
    internal interface ISceneUpdate
    {
        /// <summary>
        /// 更新方法
        /// </summary>
        public void Update();
    }
}
