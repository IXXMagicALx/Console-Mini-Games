using System;
using System.Collections.Generic;
using System.Text;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 移动方向枚举
    /// </summary>
    enum E_MoveDir
    {
        /// <summary>
        /// 方向向上
        /// </summary>
        Up,
        /// <summary>
        /// 方向向下
        /// </summary>
        Down,
        /// <summary>
        /// 方向向左
        /// </summary>
        Left,
        /// <summary>
        /// 方向向右
        /// </summary>
        Right,
    }
    /// <summary>
    /// 蛇类
    /// </summary>
    internal class Snake : IDraw
    {
        Random r = new Random();
        SnakeBody[] snake;
        E_MoveDir dir = E_MoveDir.Right;
        //目前身体段数
        public int nowNum = 0;
        public Snake(int x,int y)
        {
            //预留200空间，便于存入数。
            //防止超出数组而不方便扩容。
            snake = new SnakeBody[200];
            snake[0] = new SnakeBody(E_SnakeBody_Type.Haed, x, y);
            nowNum = 1;
        }
        /// <summary>
        /// 画蛇的方法
        /// </summary>
        public void Draw()
        {
            for(int i = 0 ; i < nowNum ; i++)
            {
                snake[i].Draw();
            }
        }
        /// <summary>
        /// 蛇移动的方法
        /// </summary>
        public void Move()
        {
            //擦除尾巴
            Console.SetCursorPosition(snake[nowNum - 1].pos.x, snake[nowNum - 1].pos.y);
            Console.Write("  ");

            //在蛇头移动之前 从蛇尾开始 每个后面的移到前一个的位置
            for(int i = nowNum -1 ; i > 0 ; i--)
            {
                snake[i].pos = snake[i - 1].pos;
            }

            switch(dir)
            {
                case E_MoveDir.Up:
                    snake[0].pos.y--;
                    break; 
                case E_MoveDir.Down:
                    snake[0].pos.y++;
                    break;
                case E_MoveDir.Left:
                    snake[0].pos.x-=2;
                    break;
                case E_MoveDir.Right:
                    snake[0].pos.x+=2;
                    break;
            }

        }
        /// <summary>
        /// 蛇转向的方法
        /// </summary>
        public void Turn(E_MoveDir dir)
        {
            //按键与此时方向相同时保持
            //有身体时 不能右转左 上转下
            if(dir == this.dir||nowNum > 1
              &&(this.dir == E_MoveDir.Left && dir == E_MoveDir.Right)
              ||(this.dir == E_MoveDir.Right && dir == E_MoveDir.Left)
              ||(this.dir == E_MoveDir.Up && dir == E_MoveDir.Down)
              ||(this.dir == E_MoveDir.Down && dir == E_MoveDir.Up))
            {
                return;
            }

            //改变成输入的方向
            this.dir = dir;
            
        }
        /// <summary>
        /// 检测结束方法
        /// </summary>
        /// <param name="map">传入地图用于检测是否与边界墙碰撞</param>
        /// <returns>返回真说明游戏结束</returns>
        public bool CheckEnd(Map map)
        {
            for(int i = 0 ; i < map.walls.Length ; i++)
            {
                //撞到墙
                if (snake[0].pos == map.walls[i].pos)
                {
                    return true;
                }

            }
            for(int i = 1 ; i < nowNum ; i++)
            {
                //撞到自己身体
                if (snake[0].pos == snake[i].pos)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检测食物坐标是否与蛇重合
        /// </summary>
        /// <param name="p">食物坐标</param>
        /// <returns>重合返回真</returns>
        public bool CheckFoodSamePosition(Position p)
        {
            for(int i = 0 ; i < nowNum ; i++)
            {
                if (snake[i].pos == p)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 蛇吃食物方法
        /// </summary>
        /// <param name="food"></param>
        public void EatFood(Food food)
        {
            if (snake[0].pos == food.pos)
            {
                //吃到了
                //吃到超级食物，随机长身体(3~5)
                if(food.foodType == E_FoodType.Super)
                {
                    //随机生长3~5
                    int growNum = r.Next(3, 6);
                    for(int i = 0 ; i < growNum ; i++)
                    {
                        Grow();
                    }
                }
                //吃到普通食物
                else
                {
                    Grow();
                }
                //先确定新食物的类型
                food.RandomFoodType();
                //再设置新食物的位置
                food.RandomSetFood(this);
            }
            
        }
        /// <summary>
        /// 长一节身体
        /// </summary>
        public void Grow()
        {
            //前一节身体
            SnakeBody frontBody = snake[nowNum - 1];
            //先长
            snake[nowNum] = new SnakeBody(E_SnakeBody_Type.Body, frontBody.pos.x, frontBody.pos.y);
            //再加
            nowNum++;
        }
    }
}
