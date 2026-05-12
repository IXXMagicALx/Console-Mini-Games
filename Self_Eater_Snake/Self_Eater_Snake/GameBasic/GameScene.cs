using Self_Eater_Snake.GamePlay;
using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GameBasic
{
    /// <summary>
    /// 游戏场景类
    /// </summary>
    internal class GameScene : ISceneUpdate
    {
        Map map;
        Snake snake;
        Food food;
        int timeLine = 0;
        public static int score = 0;
        /// <summary>
        /// 游戏场景构造函数 初始化游戏场景中的一切
        /// </summary>
        public GameScene()
        {
            map = new Map();
            snake = new Snake(10, 10);
            food = new Food(snake);
        }
        
        public void Update()
        {
            if(0 == timeLine%8000)
            {
                //画地图
                map.Draw();
                //画食物
                food.Draw();
                //移动
                snake.Move();
                //画蛇
                snake.Draw();
                if(snake.CheckEnd(map))
                {
                    //记分
                    score = snake.nowNum - 1;
                    //结束逻辑
                    Game.ChangeScene(E_SceneType.End);
                }
                snake.EatFood(food);
                timeLine = 0;
            }

            timeLine++;

            if(Console.KeyAvailable)//检测输入但不中断代码
            {
                switch(Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.Turn(E_MoveDir.Up);
                        break;
                    case ConsoleKey.A:
                        snake.Turn(E_MoveDir.Left);
                        break;
                    case ConsoleKey.S:
                        snake.Turn(E_MoveDir.Down);
                        break;
                    case ConsoleKey.D:
                        snake.Turn(E_MoveDir.Right);
                        break;
                }
            }
        }
    }
}
