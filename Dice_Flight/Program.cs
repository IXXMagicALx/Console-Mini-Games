using Microsoft.VisualBasic.FileIO;
using System.Numerics;
using System.Security.Cryptography;

namespace Dice_Flight
{
    #region 场景枚举
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
    #endregion
    #region 地块儿枚举
    /// <summary>
    /// 地块儿枚举
    /// </summary>
    enum E_FieldType
    {
        /// <summary>
        /// 普通
        /// </summary>
        Normal,
        /// <summary>
        /// 陷阱
        /// </summary>
        Trap,
        /// <summary>
        /// 炸弹
        /// </summary>
        Boom,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 虚空枢纽
        /// </summary>
        Portal,
    }
    #endregion
    #region 玩家枚举
    /// <summary>
    /// 玩家枚举
    /// </summary>
    enum E_PlayerType
    {
        /// <summary>
        /// 玩家
        /// </summary>
        Player,
        /// <summary>
        /// 电脑
        /// </summary>
        Bot,
    }
    #endregion
    ///////////////////////////////////////////////
    #region 地块儿结构体
    /// <summary>
    /// 地块儿结构体
    /// </summary>
    struct Field
    {
        //变量
        public E_FieldType fieldType;//地块儿类型
        public Vector2 position;//地块儿位置
        //构造函数(用于方便初始化)
        public Field(int x, int y, E_FieldType FieldType)
        {
            position.x = x;//初始化结构体里的Position变量
            position.y = y;//初始化结构体里的Position变量
            this.fieldType = FieldType;//初始化FieldType变量，即地块儿的不同类型(和传入变量同名所以要加this)
        }
        //函数
        public void Draw()//画格子的方法
        {
            Console.SetCursorPosition(position.x, position.y);//先设置位置
            switch (fieldType)
            {
                case E_FieldType.Normal://普通
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("□");
                    break;
                case E_FieldType.Trap://陷阱
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("¤");
                    break;
                case E_FieldType.Pause://暂停
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("∥");
                    break;
                case E_FieldType.Boom://炸弹
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    break;
                case E_FieldType.Portal://虚空枢纽
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("∞");
                    break;
            }
        }
    }
    #endregion
    #region 位置信息结构体
    /// <summary>
    /// 位置信息结构体
    /// </summary>
    struct Vector2
    {
        //变量
        public int x;
        public int y;
        //构造函数
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    #endregion
    #region 地图结构体
    /// <summary>
    /// 地图结构体
    /// </summary>
    struct Map
    {
        //变量
        public Field[] fields;//格子结构体数组 【Field[]】是类型，【fields】是变量名
        //构造函数
        /// <summary>
        /// 地图结构体的构造函数
        /// </summary>
        /// <param name="num">格子数量</param>
        public Map(int x , int y,int num)
        {
            int indexX = 0;//x 水平移动的次数
            int indexY = 0;//y 向下移动的次数
            int step = 2;//格子每次横向移动的步长是2
            fields = new Field[num];//初始化格子数量
            #region 初始化格子类型
            Random r = new Random();
            for (int i = 0; i < num; i++)
            {
                int chance = r.Next(1, 101);
                if (chance <= 71 || i == 0 || i == num - 1)//普通格子概率是71%
                {
                    fields[i].fieldType = E_FieldType.Normal;
                    //解释：变量类型为Field结构体类型的 数组中的一格 类型是 枚举中的普通格子
                }
                else if (chance > 71 && chance <= 73)//陷阱格子概率是2%
                {
                    fields[i].fieldType = E_FieldType.Trap;
                }
                else if (chance > 73 && chance <= 80)//暂停格子概率是7%
                {
                    fields[i].fieldType = E_FieldType.Pause;
                }
                else if (chance > 80 && chance <= 85)//炸弹格子概率是5%
                {
                    fields[i].fieldType = E_FieldType.Boom;
                }
                else//虚空枢纽格子概率是10%
                {
                    fields[i].fieldType = E_FieldType.Portal;
                }
            #endregion
            #region 设置格子位置
            fields[i].position = new Vector2(x, y);
                //解释：地块儿数组 的位置信息 是 位置信息结构体
                /////////////////////////////////
                /*规律：x先往右加每次加2，加够十次。
                        y往下加两次每次加1。
                       之后x再反向向左减2，减够十次。
                        y往下加两次每次加1。
                        以此类推......*/
                if (10 == indexX)//到Y动了
                {
                    y++;//纵坐标每次变化1
                    indexY++;
                    if (2 == indexY)
                    {
                        step = -step;//x 移动反向
                        indexX = 0;//复位
                        indexY = 0;//复位
                    }
                }
                else
                {
                    x += step;//横坐标每次变化2
                    indexX++;
                } 
            #endregion
            }
        }
        //函数
        public void Draw()//打印地块儿的方法
        {
            for(int i = 0;i < fields.Length; i++)
            {
                //采用遍历数组的方式
                //通过执行地块结构体中的画地块儿函数
                //打印出地图
                fields[i].Draw();
            }
        }
    }
    #endregion
    #region 玩家结构体
    struct Player
    {
        //变量
        public E_PlayerType playerType;
        public int nowIndex;//数组的下标，玩家在哪个格子位置的索引
        public bool isPause;//处于暂停状态标识

        //构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="type">玩家类型</param>
        public Player(int index,E_PlayerType type)
        {
            nowIndex = index;
            playerType = type; 
            isPause = false;//暂停标识
        }
        //函数
        public void Draw(Map map)
        {
            //必须要先得到地图才能得到在地图的哪个格子
            Field field = map.fields[nowIndex];
            //取出所在的格子的信息
            //设置位置
            Console.SetCursorPosition(field.position.x, field.position.y);
            //设置颜色
            //设置图标
            switch (playerType)
            {
                case E_PlayerType.Player:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("★");
                    break;
                case E_PlayerType.Bot:
                    Console.ForegroundColor= ConsoleColor.Magenta;
                    Console.Write("▲");
                    break;
            }
        }
    }
    #endregion
    internal class Program
    {
        #region 窗口初始化函数
        /// <summary>
        /// 窗口初始化
        /// </summary>
        /// <param name="w">控制台宽度</param>
        /// <param name="h">控制台高度</param>
        static void ConsoleSetting(int w, int h)
        {
            Console.SetWindowSize(w, h);//设置窗口大小
            Console.SetBufferSize(w, h + 5);//设置缓冲区大小
            Console.CursorVisible = false;//光标不显示
        }
        #endregion
        #region 开始和结束场景的函数
        /// <summary>
        /// 开始和结束场景的函数
        /// </summary>
        /// <param name="w">控制台宽度</param>
        /// <param name="h">控制台高度</param>
        /// <param name="nowScene">当前场景标识</param>
        /// <param name="nowSelect">当前选择标识</param>
        static void BeginAndEndScene(int w, int h, ref E_SceneType nowScene)
        {
            int nowSelect = 0;//默认选择开始游戏
            while (true)
            {
                //打印标题
                Console.SetCursorPosition(nowScene == E_SceneType.Begin ? w / 2 - 6 : w / 2 - 5, 8);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(nowScene == E_SceneType.Begin ? "Dice_Flight" : "Game Over");
                //打印第一行
                Console.SetCursorPosition(nowScene == E_SceneType.Begin ? w / 2 - 4 : w / 2 - 5, 13);
                Console.ForegroundColor = (nowSelect == 0 ?ConsoleColor.Red : ConsoleColor.White);
                Console.Write(nowScene == E_SceneType.Begin ? "开始游戏" : "回到主界面");
                //打印第二行
                Console.SetCursorPosition(w / 2 - 4, 15);
                Console.ForegroundColor = (nowSelect == 1 ? ConsoleColor.Red : ConsoleColor.White);
                Console.Write("退出游戏");
                //检测按键
                switch (Console.ReadKey(true).Key)//不显示输入
                {
                    case ConsoleKey.W:
                        nowSelect--;
                        if(nowSelect <= 0)
                        {
                            nowSelect = 0;
                        }
                        break;
                    case ConsoleKey.S:
                        nowSelect++;
                        if (nowSelect >= 1)
                        {
                            nowSelect = 1;
                        }
                        break;
                    case ConsoleKey.J:
                        if(nowSelect == 0)
                        {
                            nowScene = (nowScene == E_SceneType.Begin ? E_SceneType.Game : E_SceneType.Begin);//场景标识切换到游戏场景
                            return;//退出函数，回到主逻辑
                        }
                        else
                        {
                            Environment.Exit(0);//关闭控制台
                        }
                        break;
                }
            }
        }
        #endregion
        #region 不变环境搭建函数
        /// <summary>
        /// 不变环境搭建函数
        /// </summary>
        /// <param name="w">控制台宽度</param>
        /// <param name="h">控制台高度</param>
        static void Build(int w, int h)
        {
            #region 围墙
            //场景颜色
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < w; i += 2)
            {
                //第一行
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                //第二行
                Console.SetCursorPosition(i, h - 10);
                Console.Write("■");
                //第三行
                Console.SetCursorPosition(i, h - 5);
                Console.Write("■");
                //第四行
                Console.SetCursorPosition(i, h - 1);
                Console.Write("■");
            }
            for (int i = 0; i < h; i++)
            {
                //左列
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                //右列
                Console.SetCursorPosition(w - 2, i);
                Console.Write("■");
            }
            #endregion
            #region 玩法提示
            //普通格子
            Console.SetCursorPosition(2, h - 9);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("□:普通格子");
            //陷阱
            Console.SetCursorPosition(20, h - 9);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("¤:陷阱，直接回到起点");
            ////////////////////////////////////////////
            //暂停
            Console.SetCursorPosition(2, h - 8);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("∥:暂停,一回合不动");
            //炸弹
            Console.SetCursorPosition(25, h - 8);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("●:炸弹，倒退五格");
            ////////////////////////////////////////////
            //虚空枢纽
            Console.SetCursorPosition(2, h - 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("∞:虚空枢纽,随机往前5或往后5格，暂停，换位置");
            ////////////////////////////////////////////
            //玩家
            Console.SetCursorPosition(2, h - 6);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("★:玩家");
            //电脑
            Console.SetCursorPosition(16, h - 6);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("▲:电脑");
            //重合
            Console.SetCursorPosition(26, h - 6);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("◎:玩家与电脑重合");

            //按任意键开始扔骰子
            Console.SetCursorPosition(2, h - 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("按任意键开始扔骰子");
            #endregion
        }
        #endregion
        #region 清理提示区函数
        static void CleanTips(int h)
        {
            Console.SetCursorPosition(2, h - 4);
            Console.Write("                                              ");
            Console.SetCursorPosition(2, h - 3);
            Console.Write("                                              ");
            Console.SetCursorPosition(2, h - 2);
            Console.Write("                                              ");
        }
        #endregion
        #region 玩家打印函数
        /// <summary>
        /// 玩家打印函数
        /// </summary>
        /// <param name="player">玩家结构体</param>
        /// <param name="bot">电脑结构体</param>
        /// <param name="map">地图信息</param>
        static void DrawPlayer(Player player, Player bot, Map map)
        {
            //重合
            if (player.nowIndex == bot.nowIndex)
            {
                Field field = map.fields[player.nowIndex];
                //取出所在格子的位置信息
                Console.SetCursorPosition(field.position.x, field.position.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("◎");
            }
            //不重合
            else
            {
                player.Draw(map);
                bot.Draw(map);
            }
        }
        #endregion
        #region 扔骰子函数
        /// <summary>
        /// 扔骰子
        /// </summary>
        /// <param name="w">窗口宽</param>
        /// <param name="h">窗口高</param>
        /// <param name="mainPlayer">玩家</param>
        /// <param name="otherPlayer">另一名玩家</param>
        /// <param name="map">地图信息</param>
        /// <returns>默认返回false表示未结束</returns>
        static bool RandomMove(int w , int h , ref Player mainPlayer ,ref Player otherPlayer, Map map)
        {
            //擦之前的提示内容
            CleanTips(h);
            /////////////
            Console.ForegroundColor = mainPlayer.playerType == E_PlayerType.Player ? ConsoleColor.Cyan:ConsoleColor.Magenta;
            Random r = new Random();
            int moveStep = r.Next(1, 7);//移动格数随机数
            Console.SetCursorPosition(2, h - 4);
            Console.Write("{0}扔出点数为:{1}", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑", moveStep );
            
            int randomNum = r.Next(1, 100);//虚空枢纽几种类型判断随机数
            
            if (mainPlayer.isPause)//判断玩家是否处于暂停状态
            {
                Console.SetCursorPosition(2, h - 4);
                Console.Write("处于暂停点,{0}需要暂停一回合",mainPlayer.playerType == E_PlayerType.Player?"你":"电脑");
                //停止暂停
                mainPlayer.isPause = false;
                return false;
            }
            mainPlayer.nowIndex += moveStep;
            if ( mainPlayer.nowIndex >= map.fields.Length - 1 )
            {
                #region 到达终点
                mainPlayer.nowIndex = map.fields.Length - 1;//超过终点则停在终点
                Console.SetCursorPosition(2, h - 4);
                if (mainPlayer.playerType == E_PlayerType.Player)//玩家先到达终点
                {
                    
                    Console.Write("恭喜你，你率先到达了终点");

                }
                else//电脑先到达终点
                {
                    Console.Write("很遗憾，电脑率先到达了终点");
                }
                Console.SetCursorPosition(2, h - 3);
                Console.Write("请按任意键结束游戏");
                return true;
                #endregion
            }
            else//没有到终点，就判断到什么样的格子
            {
                Field field = map.fields [mainPlayer.nowIndex];
                Console.SetCursorPosition(2, h - 4);
                switch (field.fieldType)//根据格子类型判断
                {
                    case E_FieldType.Normal:
                        //普通格子无操作
                        #region 提示信息
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("{0}到达一个安全位置", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                        #endregion
                        break;
                    case E_FieldType.Trap:
                        //陷阱格子，回到起点
                        mainPlayer.nowIndex = 0;
                        #region 提示信息
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("{0}落入了陷阱,回到了起点", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                        #endregion
                        break;
                    case E_FieldType.Pause:
                        //暂停格子，暂停一回合
                        mainPlayer.isPause = true;
                        #region 提示信息
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("{0}到达了暂停区域，下一回合需要等待一回合", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                        #endregion
                        break;
                    case E_FieldType.Boom:
                        //炸弹格子，倒退五格
                        mainPlayer.nowIndex -= 5;
                        if(mainPlayer.nowIndex < 0)//不能比起点小
                        {
                            mainPlayer.nowIndex = 0;
                        }
                        #region 提示信息
                        Console.SetCursorPosition(2, h - 3);
                        Console.Write("Fire the hole!,{0}被狂鼠的炸弹轮胎炸退了五格", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                        Console.SetCursorPosition(2, h - 2);
                        Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                        #endregion
                        break;
                    case E_FieldType.Portal:
                        //虚空枢纽格子，向前or向后or暂停一回合or互换位置
                        if (randomNum <= 20)//往前5格
                        {
                            mainPlayer.nowIndex += 5;
                            if(mainPlayer.nowIndex > map.fields.Length - 1)//不能超过终点
                            {
                                mainPlayer.nowIndex = map.fields.Length - 1;
                            }
                            #region 提示信息
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("辛梅塔帮{0}放了个向前的传送门，前进5格", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                            Console.SetCursorPosition(2, h - 2);
                            Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                            #endregion
                        }

                        else if (randomNum > 20 && randomNum <= 40)//往后5格
                        {
                            mainPlayer.nowIndex -= 5;
                            if (mainPlayer.nowIndex < 0)//不能比起点小
                            {
                                mainPlayer.nowIndex = 0;
                            }
                            #region 提示信息
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("恶灵用次元传送门把{0}抓走了,{1}往回退了五格", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                            Console.SetCursorPosition(2, h - 2);
                            Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                            #endregion
                        }

                        else if (randomNum > 40 && randomNum <= 80)//暂停一回合
                        {
                            mainPlayer.isPause = true;
                            #region 提示信息
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("壹决把{0}放到了不明空间,自己走了,等一回合吧", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                            Console.SetCursorPosition(2, h - 2);
                            Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                            #endregion
                        }

                        else//互换位置
                        {
                            int temp = mainPlayer.nowIndex;
                            mainPlayer.nowIndex = otherPlayer.nowIndex;
                            otherPlayer.nowIndex = temp;
                            #region 提示信息
                            Console.SetCursorPosition(2, h - 3);
                            Console.Write("河神附身{0}，把对面拉下水,自己到了对面的位置", mainPlayer.playerType == E_PlayerType.Player ? "你" : "电脑");
                            Console.SetCursorPosition(2, h - 2);
                            Console.Write("请按任意键,让{0}开始扔骰子", mainPlayer.playerType == E_PlayerType.Player ? "电脑" : "你自己");
                            #endregion
                        }
                        break;
                }
            }
            return false;//默认没有结束，返回false
        }
        #endregion
        #region 玩家移动逻辑
        /// <summary>
        /// 玩家移动逻辑函数(把投骰子，打印地图和打印玩家封装到了一起)
        /// </summary>
        /// <param name="w">控制台宽度</param>
        /// <param name="h">控制台高度</param>
        /// <param name="player">玩家结构体</param>
        /// <param name="bot">电脑结构体</param>
        /// <param name="map">地图信息</param>
        /// <param name="nowScene">场景标识</param>
        /// <returns></returns>
        static bool PlayerMove(int w, int h , ref Player player , ref Player bot , Map map , ref E_SceneType nowScene)
        {
         Console.ReadKey(true);
         bool isGameOver = RandomMove(w , h , ref player , ref bot , map);
        //同时给isGameOver赋值true或false
        //打印地图
        map.Draw();
        //打印玩家
        DrawPlayer(player, bot, map);
        //判断
        if (isGameOver)
        {
         //卡住程序让玩家，按任意键
         Console.ReadKey(true);
         nowScene = E_SceneType.End;//切到结束场景
        }
        return isGameOver;
}
        #endregion
        #region 游戏场景函数
        /// <summary>
        /// 游戏场景函数
        /// </summary>
        /// <param name="w">控制台宽度</param>
        /// <param name="h">控制台高度</param>
        /// <param name="nowScene">场景标识</param>
        static void GameScene(int w , int h , ref E_SceneType nowScene)
        {
            //不变环境搭建函数的使用
            Build(w, h);
            ///////////////////////////////////////////////
            //地图结构体的使用
            Map map = new Map(14,3,80);//给的是起点的横坐标，起点的纵坐标，格数
            map.Draw();
            ///////////////////////////////////////////////
            //玩家结构体的使用
            Player player = new Player(0,E_PlayerType.Player);
            Player bot = new Player(0,E_PlayerType.Bot);
            DrawPlayer(player, bot, map);
            //画玩家的函数，内部包含了，玩家结构体里的player.Draw()
            //////////////////////////////////////////////
            bool isGameOver = false;
            while (true)
            {
                //玩家整个投骰子逻辑
                isGameOver = PlayerMove(w , h , ref player , ref bot , map , ref nowScene);
                if (isGameOver)
                {
                    return;//跳出游戏场景函数
                }
                //电脑整个投骰子逻辑
                isGameOver = PlayerMove(w , h , ref bot , ref player , map , ref nowScene);
                if (isGameOver)
                {
                    return;//跳出游戏场景函数
                }
            }
        }
        #endregion
        static void Main(string [] args)
        {
            #region 主逻辑
            int w = 50, h = 30;
            ConsoleSetting(w, h);//场景初始化函数
            E_SceneType nowScene = E_SceneType.Begin;
            while (true)
            {
                switch (nowScene)
                {
                    case E_SceneType.Begin:
                        Console.Clear();//清空控制台
                        BeginAndEndScene( w,  h, ref nowScene);
                        break;
                    case E_SceneType.Game:
                        Console.Clear();//清空控制台
                        GameScene( w , h ,ref nowScene);
                        break;
                    case E_SceneType.End:
                        Console.Clear();//清空控制台
                        BeginAndEndScene(w, h, ref nowScene);
                        break;
                }
            }
            #endregion
        }
    }
}
