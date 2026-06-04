using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 游戏场景类
    /// </summary>
    internal class GameScene : ISceneUpdate
    {
        //记分(消除了多少行，一行+10分)
        public static int score = 0;

        //地图成员
        Map map;
        //方块管理器
        BlockManager blockManager;
        //HUD界面
        HUD hud; 
        //检测输入线程
        Thread checkInputThread;

        /// <summary>
        /// 游戏场景构造函数 初始化游戏场景中的一切
        /// </summary>
        public GameScene()
        {
            map = new Map(this);
            blockManager = new BlockManager();
            hud = new HUD(map , blockManager);
            //添加一个输入监听事件
            InputThread.Instance.inputEvent += CheckInput;
        }
        
        public void Update()
        {
            lock(blockManager)
            {
                //打印地图
                map.Draw();
                //打印移动的方块
                blockManager.Draw();
                //打印提示信息
                hud.Draw();

                if (blockManager.JudgeAutoMove(map))
                    blockManager.AutoMove();
                
            }
            //休眠不在锁里因为，在所里休眠多线程来检测输入就失去了意义
            //通过线程休眠实现缓慢移动
            Thread.Sleep(300);//每移动一次休眠0.3s
        }
        /// <summary>
        /// 
        /// </summary>
        private void CheckInput()
        {
                if (Console.KeyAvailable)//有输入时才会来判断
                {
                    //锁——防止同时使用而出现错误
                    lock(blockManager)
                    {
                        //检测输入来改变形态
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.LeftArrow:
                                if (blockManager.JudgeRotate(E_RotateType.Left, map))
                                    blockManager.Rotate(E_RotateType.Left);
                                break;
                            case ConsoleKey.RightArrow:
                                if (blockManager.JudgeRotate(E_RotateType.Right, map))
                                    blockManager.Rotate(E_RotateType.Right);
                                break;
                            case ConsoleKey.A:
                                if (blockManager.JudgeMoveLR(E_MoveType.Left, map))
                                    blockManager.MoveLR(E_MoveType.Left);
                                break;
                            case ConsoleKey.D:
                                if (blockManager.JudgeMoveLR(E_MoveType.Right, map))
                                    blockManager.MoveLR(E_MoveType.Right);
                                break;
                            case ConsoleKey.S://通过按下S直接调用自动移动方法来加速
                                if (blockManager.JudgeAutoMove(map))
                                    blockManager.AutoMove();
                                break;
                        }
                    }
                }
        }

        public void CloseInput()
        {
            //去掉监听输入的事件相当于关闭线程
            InputThread.Instance.inputEvent -= CheckInput;
        }
    }
}
