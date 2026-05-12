using System;
using System.Collections.Generic;
using System.Text;
using Self_Eater_Snake.GameBasic;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 食物类型
    /// </summary>
    enum E_FoodType
    {
        /// <summary>
        /// 普通食物
        /// </summary>
        Normal,
        /// <summary>
        /// 超级食物
        /// </summary>
        Super,
    }
    /// <summary>
    /// 食物类
    /// </summary>
    internal class Food : GameObject
    {
        Random r = new Random();
        public E_FoodType foodType = E_FoodType.Normal;
        /// <summary>
        /// 食物构造函数
        /// </summary>
        /// <param name="snake">传入蛇，与蛇坐标进行比较避免重合</param>
        public Food(Snake snake)
        {
            //在每次设置食物之前
            //确定食物类型
            RandomFoodType();
            //设置食物
            RandomSetFood(snake);
        }
        /// <summary>
        /// 画食物
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            Console.ForegroundColor = (foodType == E_FoodType.Normal ? ConsoleColor.Cyan : ConsoleColor.Blue);
            Console.Write(foodType == E_FoodType.Normal ? "▲" : "★");
        }
        /// <summary>
        /// 确定食物类型
        /// </summary>
        public void RandomFoodType()
        {
            
            int foodTypePercent = r.Next(1, 101);
            //70%概率为普通食物
            if(foodTypePercent <= 70)
            {
                foodType = E_FoodType.Normal;
            }
            //30%概率为超级食物
            else
            {
                foodType = E_FoodType.Super;
            }
        }
        /// <summary>
        /// 在地图上放置食物
        /// </summary>
        /// <param name="snake">传入蛇，与蛇坐标进行比较避免重合</param>
        public void RandomSetFood(Snake snake)
        {

            int x = r.Next(2, Game.width/2 - 1) * 2;
            int y = r.Next(1, Game.height - 4);
            pos = new Position(x, y);
            //判断是否和蛇的位置相同
            //如果位置相同则接着递归直到不同
            if(snake.CheckFoodSamePosition(pos))
            {
                RandomSetFood(snake);
            }
        }
    }
}
