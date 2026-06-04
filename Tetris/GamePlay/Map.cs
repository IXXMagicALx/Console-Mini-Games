using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Tetris
{
    internal class Map:IDraw
    {
        public int width;
        public int height;

        GameScene nowGameScene;
        //每行可放的动态墙壁个数
        public int lineCapacity;
        //记录每行有多少个小方块
        private int[] lineCount;

        //固定墙壁
        List<Object> walls = new List<Object>();
        //动态墙壁
        public List<Object> dynamicWalls = new List<Object>();

        public Map( GameScene gameScene)
        {
            this.nowGameScene = gameScene;

            width = Game.width - 2;
            height = Game.height - 8;

            lineCapacity = 0;
            //0 ~ Game.height - 9
            lineCount = new int[height];
            //初始化地图两个横向边界
            for (int i = 0 ; i < Game.width; i += 2)
            {
                walls.Add(new Object(E_ObjectType.Wall , i , height));
                lineCapacity++;
                walls.Add(new Object(E_ObjectType.Wall , i , Game.height - 1));
            }
            //减去两端的两个,完成对一行方格容量的初始化
            lineCapacity -= 2;

            //初始化地图两个竖直边界
            for(int i = 0 ; i < Game.height ; i++)
            {
                walls.Add(new Object(E_ObjectType.Wall, 0 , i));
                walls.Add(new Object(E_ObjectType.Wall, width , i));
            }

            //打印固定墙壁,在构造函数中打印,只打印一次减少性能浪费
            foreach (Object item in walls)
            {
                item.Draw();
            }
        }
        /// <summary>
        /// 打印动态墙壁
        /// </summary>
        public void Draw()
        {
            //打印动态墙壁
            foreach(Object item in dynamicWalls)
            {
                item.Draw();
            }
        }
        /// <summary>
        /// 清理动态墙壁
        /// </summary>
        public void CleanDraw()
        {
            foreach(Object item in dynamicWalls)
            {
                item.CleanDraw();
            }

        }
        /// <summary>
        /// 增加动态墙壁
        /// </summary>
        /// <param name="walls">存要改变的方块的列表</param>
        public void AddWalls(List<Object> walls)
        {
            for(int i = 0 ; i < walls.Count ; i++)
            {
                //存入动态墙壁中
                dynamicWalls.Add(walls[i]);

                //如果到顶了
                if (walls[i].position.y <= 0)
                {
                    //关闭线程
                    nowGameScene.CloseInput();
                    //切换场景到结束场景
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }

                //添加动态墙壁后记录在哪一行
                //height为Game.height - 8
                //walls[i].position.y最大为Game.height - 9
                //为了使最底下的一行的索引为0，还要-1
                lineCount[height - walls[i].position.y - 1]++;
            }

            //清理动态墙壁
            CleanDraw();
            //判断是否要移除
            CheckClean();
            //打印动态墙壁
            Draw();
        }
        /// <summary>
        /// 判断是否要移除
        /// </summary>
        public void CheckClean()
        {
            //待移除列表
            List<Object> delList = new List<Object>();

            //遍历记录每行有多少个小方块的数组
            for (int i = 0 ; i < lineCount.Length ; i++)
            {
                //如果这行已经满了
                if (lineCount[i] == lineCapacity)
                {
                    ScoreUpdate();
                    //遍历所有动态墙壁以找到在这行的动态墙壁
                    for (int j = 0 ; j < dynamicWalls.Count ; j++)
                    {
                        //找到满了的行
                        if((height - dynamicWalls[j].position.y - 1) == i)
                        {
                            //将处在满了的那行的小方块存入待移除列表中之后统一移除
                            delList.Add(dynamicWalls[j]);
                        }

                        //满了的那行之上的所有行下移
                        else if((height - dynamicWalls[j].position.y - 1) > i)
                        {
                            //上面的每行纵坐标都+1
                            dynamicWalls[j].position.y++;
                        }

                    }

                    //遍历移除待移除列表中的方块
                    foreach(Object item in delList)
                    {
                        dynamicWalls.Remove(item);
                    }
                    //注意 j的初值不是0是i，也就是从删除行开始往上
                    //记录方块数量的数组往下迁移
                    for(int j = i ; j < lineCount.Length - 1 ; j++)
                    {
                        lineCount[j] = lineCount[j + 1]; 
                    }
                    //将最顶的计数置空
                    lineCount[lineCount.Length - 1] = 0;

                    //通过函数的重载来判断是否还有其他行也需要清空
                    CheckClean();
                    break;
                }

            }
        }

        /// <summary>
        /// 得分更新
        /// </summary>
        public void ScoreUpdate()
        {
            //清除旧得分
            Console.SetCursorPosition(36, height + 2);
            Console.Write("    ");

            GameScene.score += 10;

            //打印新得分
            Console.SetCursorPosition(37, height + 2);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(GameScene.score);
        }
    }
}
