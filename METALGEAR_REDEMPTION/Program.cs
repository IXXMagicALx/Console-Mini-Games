namespace METALGEAR_REDEMPTION
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 基础设置
            int width =60, height =30;//申明尺寸
            Console.SetWindowSize(width,height);//设置窗口大小
            Console.SetBufferSize(width, height+5);//设置缓冲区大小
            Console.CursorVisible = false;//隐藏光标
            Random r = new Random();//产生随机数对象
            ConsoleColor titleColor = ConsoleColor.Yellow;//申明标题颜色
            ConsoleColor environmentColor = ConsoleColor.Red;//申明环境颜色 
            string gameOverInfo="";
            #endregion
            int nowScene = 1;//所处场景编号申明
            int nowSelect = 0;//开始和结束选择申明
            while (true)//主循环
            {
                bool isQuitWhile = false;//使能退出while
                switch (nowScene)//分类四个场景
                {
                    case 1:
                        Console.Clear();//切换场景时清空
                        #region 开始场景
                        Console.SetCursorPosition(width/2-6,10);
                        Console.ForegroundColor = titleColor;
                        Console.Write("潜龙谍影:救赎");//打出标题
                        while (true)
                        {
                            //打印开始游戏
                            Console.SetCursorPosition(width / 2 - 4, 16);
                            Console.ForegroundColor = nowSelect == 0 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("开始游戏");
                            //打印前情提要
                            Console.SetCursorPosition(width / 2 - 4, 17);
                            Console.ForegroundColor = nowSelect == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("前情概况");
                            //打印退出游戏
                            Console.SetCursorPosition(width / 2 - 4, 18);
                            Console.ForegroundColor = nowSelect == 2 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("退出游戏");
                            char input1 = Console.ReadKey(true).KeyChar;//检测按键切换选择,且不显示输入内容
                            switch (input1)
                            {
                                case 'W':
                                case 'w':
                                    nowSelect--;
                                    if (nowSelect<0)
                                    {
                                        nowSelect = 0;
                                    }
                                    break;
                                case 'S':
                                case 's':
                                    nowSelect++;
                                    if (nowSelect>2)
                                    {
                                        nowSelect = 2;
                                    }
                                    break;
                                case 'J':
                                case 'j':
                                    if (nowSelect==0)
                                    {
                                        nowScene = 2;
                                        isQuitWhile = true;
                                    }
                                    else if (nowSelect==1)
                                    {
                                        nowScene = 3;
                                        isQuitWhile = true;
                                    }
                                    else if (nowSelect==2)
                                    {
                                        Environment.Exit(0);
                                    }
                                    break;
                            }
                            if (isQuitWhile)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;
                    case 2:
                        Console.Clear();//切换场景时清空
                        #region 游戏场景
                        //环境
                        #region 环境
                        //上方墙
                        Console.ForegroundColor = environmentColor;
                        for (int i = 0 ; i < width ; i+=2)
                        {
                            Console.SetCursorPosition(i , 0);//上方墙
                            Console.Write('■');
                            Console.SetCursorPosition(i , height-8);//中间墙
                            Console.Write('■');
                            Console.SetCursorPosition(i , height-1);//下方墙
                            Console.Write('■');
                        }
                        Console.SetCursorPosition(0,7);
                        Console.Write("■■■■■■■■■■■              ■■■■■■■■■■■");
                        Console.SetCursorPosition(0, 15);
                        Console.Write("■■■■■■■■■■■              ■■■■■■■■■■■");
                        for (int i = 0; i < height; i ++)
                        {
                            Console.SetCursorPosition(0, i);//左侧墙
                            Console.Write('■');
                            Console.SetCursorPosition(width-2, i);//右侧墙
                            Console.Write('■');
                        }
                        Console.SetCursorPosition(0, 7);
                        #endregion
                        #region V Snake相关
                        int VS_X = 2, VS_Y = 11, VS_HP = 185;
                        int VS_ATKmin = 12, VS_ATKmax = 22;
                        ConsoleColor VS_Color = ConsoleColor.Cyan;
                        char VS_Icon = '▲';
                        #endregion
                        #region BOSS相关 
                        int BOSS_X = width/2-2 , BOSS_Y = 3, BOSS_HP = 220;
                        int BOSS_ATKmin = 10 , BOSS_ATKmax = 20;
                        ConsoleColor BOSS_Color = ConsoleColor.DarkRed;
                        char BOSS_Icon = '●';
                        #endregion
                        #region 艾什莉相关
                        int ASL_X = width / 2-2, ASL_Y = 19;
                        ConsoleColor ASL_Color = ConsoleColor.Green;
                        char ASL_Icon = '★';
                        #endregion
                        bool isPeace = true, isFight = false, isTalk = false;
                        while (true)//检测玩家输入死循环
                        {
                            isQuitWhile = false;
                            if(BOSS_HP > 0)
                            {
                                #region BOSS显示相关
                                Console.SetCursorPosition(BOSS_X, BOSS_Y);
                                Console.ForegroundColor = BOSS_Color;
                                Console.Write(BOSS_Icon);
                                #endregion
                            }
                                #region 艾什莉显示相关
                            //一直在，但是不击败BOSS不能成功
                                Console.SetCursorPosition(ASL_X, ASL_Y);
                                Console.ForegroundColor = ASL_Color;
                                Console.Write(ASL_Icon);
                                #endregion
                           
                                #region V Snake显示相关
                            Console.SetCursorPosition(VS_X, VS_Y);
                            Console.ForegroundColor = VS_Color;
                            Console.Write(VS_Icon);
                            #endregion
                            char input2 = Console.ReadKey(true).KeyChar;//游戏场景检测玩家输入
                            if (isPeace)//未战斗状态
                            {
                                if (BOSS_HP > 0)
                                {
                                    #region 清空对话框
                                    Console.SetCursorPosition(2, height - 7);
                                    Console.Write("                                                       ");
                                    Console.SetCursorPosition(2, height - 6);
                                    Console.Write("                                                       ");
                                    Console.SetCursorPosition(2, height - 5);
                                    Console.Write("                                                       ");
                                    #endregion
                                    //////////////////////////////////////////////////////////////////////
                                    #region 场景提示
                                    Console.SetCursorPosition(2, height - 7);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Kaz:BOSS,本次行动的目标是解救总统女儿");
                                    Console.ForegroundColor = ASL_Color;
                                    Console.Write(ASL_Icon);
                                    Console.SetCursorPosition(2, height - 6);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("为了保证总统女儿不受到伤害,请先击败敌方首领");
                                    Console.ForegroundColor = BOSS_Color;
                                    Console.Write(BOSS_Icon);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("再带她走"); 
                                    Console.SetCursorPosition(2, height - 5);
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("否则逃离会变得十分危险，我们也无法与总统交代");
                                    
                                    #endregion
                                }
                                Console.SetCursorPosition(VS_X,VS_Y);//辅助移动
                                Console.Write(" ");
                                switch (input2)
                                {
                                    #region VS移动相关
                                    case 'W':
                                    case 'w':
                                        VS_Y--;
                                        if (VS_Y < 1)
                                        {
                                            VS_Y++;
                                        }
                                        else if (VS_Y==7 && (VS_X < 22 || VS_X > width - 25))//不压中间墙
                                        {
                                            VS_Y++;
                                        }
                                        else if (VS_Y == 15 && (VS_X < 22 || VS_X > width - 25))//不压中间墙
                                        {
                                            VS_Y++;
                                        }
                                        else if(VS_X==BOSS_X&&VS_Y==BOSS_Y&&BOSS_HP>0)//不压BOSS
                                        {
                                            VS_Y++;
                                        }
                                        else if (VS_X == ASL_X && VS_Y == ASL_Y)//不压艾什莉
                                        {
                                            VS_Y++;
                                        }
                                        break;
                                    case 'A':
                                    case 'a':
                                        VS_X--;
                                        if (VS_X < 2)
                                        {
                                            VS_X++;
                                        }
                                        else if (VS_Y==7&&VS_X==21||VS_Y==15&&VS_X==21)//不压中间墙
                                        {
                                            VS_X++;
                                        }
                                        else if (VS_X == BOSS_X+1 && VS_Y == BOSS_Y && BOSS_HP > 0)//不压BOSS
                                        {
                                            VS_X++;
                                        }
                                        else if (VS_X == ASL_X+1 && VS_Y == ASL_Y )//不压艾什莉
                                        {
                                            VS_X++;
                                        }
                                        break;
                                    case 'S':
                                    case 's':
                                        VS_Y++;
                                        if (VS_Y> height - 9)
                                        {
                                            VS_Y--;
                                        }
                                        else if (VS_Y == 7 && (VS_X < 22 || VS_X > width - 25))//不压中间墙
                                        {
                                            VS_Y--;
                                        }
                                        else if (VS_Y == 15 && (VS_X < 22 || VS_X > width - 25))//不压中间墙
                                        {
                                            VS_Y--;
                                        }
                                        else if (VS_X == BOSS_X && VS_Y == BOSS_Y && BOSS_HP > 0)//不压BOSS
                                        {
                                            VS_Y--;
                                        }
                                        else if (VS_X == ASL_X && VS_Y == ASL_Y )//不压艾什莉
                                        {
                                            VS_Y--;
                                        }
                                        break;
                                    case 'D':
                                    case 'd':
                                        VS_X++;
                                        if (VS_X > width-4)
                                        {
                                            VS_X--;
                                        }
                                        else if (VS_Y == 7 && VS_X > width - 25 || VS_Y == 15 && VS_X > width - 25)//不压中间墙
                                        {
                                            VS_X--;
                                        }
                                        else if (VS_X == BOSS_X-1 && VS_Y == BOSS_Y && BOSS_HP > 0)//不压BOSS
                                        {
                                            VS_X--;
                                        }
                                        else if (VS_X == ASL_X-1 && VS_Y == ASL_Y)//不压艾什莉
                                        {
                                            VS_X--;
                                        }
                                        break;
                                #endregion
                                    #region J键互动相关
                                    case 'J':
                                    case 'j':

                                        if ((VS_X == BOSS_X && VS_Y == BOSS_Y - 1 ||  //上
                                             VS_X == BOSS_X && VS_Y == BOSS_Y + 1 ||  //下
                                             VS_X == BOSS_X - 2 && VS_Y == BOSS_Y ||  //左
                                             VS_X == BOSS_X + 2 && VS_Y == BOSS_Y) && BOSS_HP > 0) //右且BOSS活着
                                        {
                                            //清空之前的
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("                                                      ");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("                                                      ");
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("                                                      ");
                                            isPeace = false;
                                            isFight = true;
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("战斗开始，按J继续");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("你当前的血量为" + VS_HP);
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("BOSS当前的血量为" + BOSS_HP);

                                        }
                                        if ((VS_X == ASL_X && VS_Y == ASL_Y - 1 ||  //上
                                             VS_X == ASL_X && VS_Y == ASL_Y + 1 ||  //下
                                             VS_X == ASL_X - 2 && VS_Y == ASL_Y ||  //左
                                             VS_X == ASL_X + 2 && VS_Y == ASL_Y))   //右
                                        {
                                            if (BOSS_HP > 0)
                                            {
                                                Console.SetCursorPosition(2, height - 7);
                                                Console.Write("                                                     ");
                                                Console.SetCursorPosition(2, height - 6);
                                                Console.Write("                                                     ");
                                                Console.SetCursorPosition(2, height - 5);
                                                Console.Write("                                                     ");
                                                Console.SetCursorPosition(2, height - 7);
                                                Console.Write("Kaz:Boss，先解决掉敌方首领。这样才能带目标安全撤离。");
                                            }
                                            else
                                            {
                                                isPeace = false;
                                                isTalk = true;//到对话状态
                                            }
                                        }
                                        break;
                                        
                                    #endregion
                                }
                            }
                            else if (isFight)//战斗状态
                            {
                                if (input2=='J'||input2=='j')//输入J或j
                                {
                                  if (VS_HP<=0)//VS死游戏结束
                                  {
                                        #region 清空对话框
                                        Console.SetCursorPosition(2, height - 7);
                                        Console.Write("                                                     ");
                                        Console.SetCursorPosition(2, height - 6);
                                        Console.Write("                                                     ");
                                        Console.SetCursorPosition(2, height - 5);
                                        Console.Write("                                                     ");
                                        #endregion
                                        ///////////////////////////////////////////////////////////
                                        #region 主角死亡
                                        //死亡提示
                                        Console.SetCursorPosition(2, height - 7);
                                        Console.Write("\aKaz:Snake，你还好吗？回答我，Snake！Snake—————！！");
                                        Console.SetCursorPosition(2, height - 6);
                                        Console.Write("【任务失败】按 J 键返回结算界面");
                                        Console.ReadKey(true);//等待按键否则看不到上方死亡提示
                                        nowScene = 4;//到结束场景
                                        gameOverInfo = "任务失败";
                                        break;//跳出场景2while
                                        #endregion
                                  }
                                  else if (BOSS_HP<=0)//BOSS死亡，擦除BOSS
                                  {
                                        #region BOSS死亡
                                        Console.SetCursorPosition(BOSS_X, BOSS_Y);
                                        Console.Write(" ");
                                        #endregion
                                  }
                                  else//都活着，正常战斗
                                  {
                                        int VS_atk = r.Next(VS_ATKmin, VS_ATKmax);//攻击力12~21
                                        int BOSS_atk = r.Next(BOSS_ATKmin, BOSS_ATKmax);//攻击力10~19
                                        #region 玩家打BOSS
                                        BOSS_HP -= VS_atk;
                                        Console.SetCursorPosition(2, height - 7);
                                        Console.Write("                                                     ");
                                        Console.SetCursorPosition(2, height - 5);
                                        Console.Write("                                                     ");
                                        Console.SetCursorPosition(2, height - 7);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write("你对BOSS造成了{0}点伤害,BOSS还剩{1}点血量",VS_atk, BOSS_HP);
                                        #endregion
                                        if (BOSS_HP>0)//BOSS活着正常战斗
                                        {
                                        #region BOSS打玩家
                                            VS_HP -= BOSS_atk;
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("                                                     ");
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("                                                     ");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.Write("BOSS对你造成了{0}点伤害,你还剩{1}点血量", BOSS_atk, VS_HP);
                                        #endregion       
                                        }
                                        else//BOSS被击败
                                        {
                                            #region 清空对话框
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("                                                     ");
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("                                                     ");
                                            Console.SetCursorPosition(2, height - 5);
                                            Console.Write("                                                     ");
                                            #endregion
                                            ///////////////////////////////////////////////////////////////////////
                                            #region 擦除BOSS
                                            Console.SetCursorPosition(BOSS_X, BOSS_Y);
                                            Console.Write(" ");
                                            #endregion
                                            #region 击败BOSS提示
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("Kaz:BOSS，你成功击败了敌方首领，快去救目标吧");
                                            isFight = false;
                                            isPeace = true;
                                            #endregion
                                        }
                                  }
                                }
                            }
                            else if (isTalk)
                            {
                                int i = 0;
                                if (input2 == 'J' || input2 == 'j')
                                {
                                    #region 清空对话框
                                    Console.SetCursorPosition(2, height - 7);
                                    Console.Write("                                                     ");
                                    Console.SetCursorPosition(2, height - 6);
                                    Console.Write("                                                     ");
                                    Console.SetCursorPosition(2, height - 5);
                                    Console.Write("                                                     ");
                                    #endregion
                                    ///////////////////////////////////////////////////////////////////////
                                    #region 对话部分
                                    while (true)
                                    {
                                        i++;
                                        input2 = Console.ReadKey(true).KeyChar;
                                        if ((input2=='J'||input2 =='j') &&i == 1)
                                        {
                                            Console.SetCursorPosition(2, height - 7);
                                            Console.Write("Venom Snake:“Kept you waiting huh？”");
                                        }
                                        else if ((input2 == 'J' || input2 == 'j') &&i == 2)
                                        {
                                            Console.SetCursorPosition(2, height - 6);
                                            Console.Write("Ashley Graham: “(teary)What took you so long？”");
                                        }
                                        else if ((input2 == 'J' || input2 == 'j') && i == 3)
                                        {
                                            break;
                                        }
                                    }
                                    #endregion
                                    nowScene = 4;//到结束场景
                                    gameOverInfo = "任务成功";
                                    isQuitWhile = true;
                                }
                            }
                            if (isQuitWhile)
                            {
                                break;//退出场景2的循环回到主循环  
                            }   
                        }
                        #endregion
                        break;
                    case 3://前情概况
                        Console.Clear();//切换场景时清空
                        #region 前情概况
                        Console.SetCursorPosition(width / 2 - 4,5);
                        Console.Write("前情概况");
                        Console.SetCursorPosition(5,8);
                        Console.Write("传奇佣兵 Venom Snake 接获绝密任务，紧急空降于被邪教笼罩的欧洲乡村。美国总统之女艾什莉遭邪教组织劫持，全村被寄生生物操控化为狂暴傀儡。Snake 必须突破重重封锁，以卓越的战术与战力击败邪教终极 BOSS，在时间耗尽前安全营救艾什莉撤离。");
                        Console.SetCursorPosition(width / 2 - 6, 20);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("返回主界面");
                         char input3 =Console.ReadKey(true).KeyChar;
                        if (input3 =='J'||input3=='j')//按J或j
                        {
                            nowScene = 1;//回到主界面
                        }
                        
                        #endregion
                        break;
                    case 4:
                        Console.Clear();//切换场景时清空
                        #region 结束场景
                        nowSelect = 0;
                        while (true)
                        {
                            Console.SetCursorPosition(width / 2 - 4, 10);
                            Console.ForegroundColor = titleColor;
                            Console.Write("GameOver");//主标题
                            Console.SetCursorPosition(width / 2 - 4, 12);
                            Console.Write(gameOverInfo);//游戏结果决定副标题
                            Console.SetCursorPosition(width / 2 - 6, 20);
                            Console.ForegroundColor = nowSelect == 0 ? ConsoleColor.Red:ConsoleColor.White;
                            Console.Write("回到开始界面");
                            Console.SetCursorPosition(width / 2 - 4, 21);
                            Console.ForegroundColor = nowSelect == 1 ? ConsoleColor.Red : ConsoleColor.White;
                            Console.Write("退出游戏");
                        char input4 = Console.ReadKey(true).KeyChar;
                        
                            switch (input4)//筛选按键
                            {
                                case'W':
                                case'w':
                                    nowSelect--;
                                    if (nowSelect < 0)
                                    {
                                        nowSelect = 0;
                                    }
                                    break;
                                case'S':
                                case's':
                                    nowSelect++;
                                    if (nowSelect > 1)
                                    {
                                        nowSelect = 1;
                                    }
                                    break;
                                case'J':
                                case'j':
                                    if (nowSelect == 0)
                                    {
                                        nowScene = 1;//回到场景1
                                        isQuitWhile = true;
                                    }
                                    else if (nowSelect == 1)
                                    {
                                        Environment.Exit(0);//退出控制台
                                    }
                                    break;
                            }
                            if (isQuitWhile)
                            {
                                break;
                            }
                        }
                        #endregion
                        break;
                }
            }


        }
    }
}
