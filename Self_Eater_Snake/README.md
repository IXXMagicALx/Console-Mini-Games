# C# 控制台小游戏项目

本项目是一个基于 .NET 10.0 的 C# 控制台小游戏集合，包含多个经典小游戏的实现。

## 游戏列表

### 1. 贪吃蛇 (Self-Eater Snake)
经典的贪吃蛇游戏，玩家控制蛇移动，吃食物增长蛇身。

**操作说明：**
- `W` - 向上移动
- `A` - 向左移动
- `S` - 向下移动
- `D` - 向右移动

**游戏特性：**
- 普通食物：吃一个增长一节
- 超级食物：吃一个增长 3~5 节
- 撞墙或撞到自己则游戏结束
- 结束页面会显示最终得分

## 技术栈

- **框架**：.NET 10.0
- **语言**：C# 10+
- **项目类型**：控制台应用程序 (Console Application)
- **特性**：
  - 隐式using启用
  - 可空引用类型启用

## 如何运行

### 方法一：使用 dotnet CLI

```bash
cd Self_Eater_Snake
dotnet run
```

### 方法二：使用 Visual Studio

1. 使用 Visual Studio 打开 `CsharpStudy3_Practice.slnx` 解决方案文件
2. 选择 `Self_Eater_Snake` 项目
3. 按 `F5` 运行或 `Ctrl+F5` 不调试运行

## 项目结构

```
CsharpStudy3_Practice/
├── Self_Eater_Snake.slnx           # 解决方案文件
└── Self_Eater_Snake/               # 贪吃蛇游戏主项目
    ├── Program.cs                  # 程序入口
    ├── GameBasic/                  # 游戏基础框架
    │   ├── Game.cs                 # 游戏主类，管理场景切换
    │   ├── GameScene.cs            # 游戏场景
    │   ├── BeginScene.cs           # 开始场景
    │   ├── EndScene.cs             # 结束场景
    │   ├── ISceneUpdate.cs         # 场景更新接口
    │   └── BeginOrEndBasic.cs      # 开始/结束场景基类
    └── GamePlay/                   # 游戏玩法模块
        ├── Snake.cs                # 蛇类
        ├── SnakeBody.cs             # 蛇身节点
        ├── Food.cs                  # 食物类
        ├── Wall.cs                  # 墙壁类
        ├── Map.cs                   # 地图类
        ├── GameObject.cs            # 游戏对象基类
        ├── Position.cs              # 位置类
        └── IDraw.cs                 # 绘制接口
```

## 游戏架构

项目采用场景驱动的架构设计：

```
┌─────────┐    开始    ┌─────────┐
│ 开始场景 │ ────────> │ 游戏场景 │
└─────────┘           └─────────┘
                          │
                          │ 结束
                          ▼
                    ┌─────────┐
                    │ 结束场景 │
                    └─────────┘
```

- **GameBasic**：游戏框架层，包含场景管理和状态切换
- **GamePlay**：游戏逻辑层，包含蛇、食物、地图等游戏元素的具体实现
