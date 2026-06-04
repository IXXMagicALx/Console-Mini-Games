using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 监听输入
    /// </summary>
    internal class InputThread
    {
        //申明一个线程
        Thread checkInputThread;

        //输入检测事件
        public Action inputEvent;

        //单例模型
        private static InputThread instance = new InputThread();

        public static InputThread Instance
        {
            get
            {
                return instance;
            }
        }

        private InputThread()
        {
            checkInputThread = new Thread(CheckInputThread);
            //将该线程设置为后台线程
            checkInputThread.IsBackground = true;
            //启动线程
            checkInputThread.Start();
        }
        /// <summary>
        /// 检测输入线程内容
        /// </summary>
        private void CheckInputThread()
        {
            while(true)
            {
                //如果事件为空则不执行
                inputEvent?.Invoke();
            }
        }
    }
}
