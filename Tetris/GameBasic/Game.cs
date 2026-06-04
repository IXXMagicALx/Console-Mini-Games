using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// 场景枚举
    /// </summary>
    enum E_SceneType
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Begin,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
    }
    /// <summary>
    /// 游戏类
    /// </summary>
    internal class Game
    {
        //控制台尺寸 常量
        public const int width = 50;
        public const int height = 35;
        //接口
        public static ISceneUpdate nowScene; 
        /// <summary>
        /// 在构造函数中初始化控制台
        /// </summary>
        public Game()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height + 5);
            ChangeScene(E_SceneType.Begin);
        }
        /// <summary>
        /// 游戏开始函数
        /// </summary>
        public void Start()
        {
            //游戏主循环，主要负责游戏场景逻辑的更新
            while (true)
            {
                if(nowScene != null)
                {
                   nowScene.Update();
                }
            }    
        }
        /// <summary>
        /// 切换场景方法
        /// </summary>
        /// <param name="sceneType">场景类型</param>
        public static void ChangeScene(E_SceneType sceneType)
        {
            //切换场景前清空窗口
            Console.Clear();
            
            switch (sceneType)
            {
                case E_SceneType.Begin:
                    nowScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene(GameScene.score);
                    break;
            }

        }
    }
}