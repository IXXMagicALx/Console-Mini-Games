using System;
using System.Collections.Generic;
using System.Text;
using Self_Eater_Snake.GameBasic;

namespace Self_Eater_Snake.GamePlay
{
    /// <summary>
    /// 地图类
    /// </summary>
    internal class Map : IDraw
    {
        public Wall[] walls;
        /// <summary>
        /// 地图构造函数 用来初始化墙壁数组
        /// </summary>
        public Map()
        {
            int index = 0;
            walls = new Wall[Game.width + (Game.height - 3) * 2];
            //上
            for(int i = 0 ; i < Game.width ; i += 2)
            {
                walls[index] = new Wall(i, 0);
                index++;
            }
            //下
            for (int i = 0 ; i < Game.width ; i += 2)
            {
                walls[index] = new Wall(i , Game.height-2);
                index++;
            }
            //左
            for(int i = 1 ; i < Game.height - 2 ; i++ )
            {
                walls[index] = new Wall(0, i);
                index++;
            }
            //右
            for (int i = 1 ; i < Game.height - 2 ; i++)
            {
                walls[index] = new Wall(Game.width - 2, i);
                index++;
            }
        }
        /// <summary>
        /// 绘制墙壁
        /// </summary>
        public void Draw()
        {
            for(int i  = 0 ; i < walls.Length ; i++)
            {
                walls[i].Draw();
            }
        }
    }
}
