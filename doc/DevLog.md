# Pawzy Pop 开发日志

---

## 开发进度总览

| 阶段 | 状态 | 完成日期 |
|------|------|----------|
| 阶段一：项目搭建 | ✅ 已完成 | 2025-12-09 |
| 阶段二：核心玩法 | ✅ 已完成 | 2025-12-09 |
| 阶段三：UI系统 | ✅ 已完成 | 2025-12-09 |
| 阶段四：音效/存档 | ✅ 已完成 | 2025-12-09 |
| 阶段五：测试优化 | 🔲 待开始 | - |

---

## 2025-12-09 - 项目初始化

### 完成内容

#### 1. 项目结构创建

创建了完整的 Unity 项目目录结构：

```
Assets/
├── Scripts/
│   ├── Core/           # 核心游戏逻辑
│   ├── Data/           # 数据结构
│   └── UI/             # UI控制器
├── Editor/             # 编辑器扩展工具
├── Prefabs/            # 预制体
├── Resources/
│   ├── Sprites/        # 图片资源
│   ├── TileTypes/      # 元素类型配置
│   └── Levels/         # 关卡JSON配置
└── Scenes/             # 场景文件
```

#### 2. 核心脚本开发

| 脚本 | 路径 | 功能说明 |
|------|------|----------|
| `GameManager.cs` | Scripts/Core/ | 游戏状态机、分数管理、步数管理 |
| `Board.cs` | Scripts/Core/ | 棋盘生成、元素管理、下落填充 |
| `Tile.cs` | Scripts/Core/ | 单个元素的数据、位置、动画 |
| `TileType.cs` | Scripts/Core/ | 元素类型配置（ScriptableObject） |
| `MatchFinder.cs` | Scripts/Core/ | 匹配检测算法（水平+垂直） |
| `MatchProcessor.cs` | Scripts/Core/ | 交换处理、消除逻辑、连锁反应 |
| `InputManager.cs` | Scripts/Core/ | 触摸/鼠标滑动输入处理 |
| `LevelLoader.cs` | Scripts/Core/ | 关卡JSON加载、配置应用 |
| `LevelData.cs` | Scripts/Data/ | 关卡数据结构定义 |
| `GameUI.cs` | Scripts/UI/ | 游戏内HUD、弹窗控制 |
| `MenuUI.cs` | Scripts/UI/ | 主菜单界面控制 |

#### 3. 编辑器工具

| 工具 | 路径 | 功能 |
|------|------|------|
| `TileTypeCreator.cs` | Editor/ | 一键创建6种狗狗元素类型 |
| `GameSetupWindow.cs` | Editor/ | 一键搭建游戏场景、创建Tile预制体 |

#### 4. 关卡配置

创建了5个测试关卡 (`Resources/Levels/`):

| 关卡 | 步数 | 目标分数 | 元素数量 |
|------|------|----------|----------|
| Level 1 | 25 | 800 | 4种 |
| Level 2 | 22 | 1000 | 4种 |
| Level 3 | 20 | 1200 | 5种 |
| Level 4 | 18 | 1500 | 5种 |
| Level 5 | 15 | 2000 | 6种 |

---

### 核心功能实现详情

#### GameManager - 游戏状态管理

```csharp
// 游戏状态枚举
public enum GameState {
    WaitingInput,   // 等待玩家输入
    Processing,     // 处理消除中
    GameOver,       // 游戏失败
    Win             // 游戏胜利
}

// 核心事件
OnScoreChanged      // 分数变化时触发
OnMovesChanged      // 步数变化时触发
OnGameStateChanged  // 状态变化时触发
```

#### Board - 棋盘系统

- 支持可配置的棋盘大小（默认6x6）
- 随机生成元素，避免初始匹配
- 元素下落填充逻辑
- 无解时自动洗牌

#### MatchFinder - 匹配检测

- 水平匹配检测（3+连续相同）
- 垂直匹配检测（3+连续相同）
- 支持检测是否有可能的移动
- 预判交换后是否会产生匹配

#### MatchProcessor - 消除处理

- 交换动画（Smoothstep缓动）
- 无效交换自动回退
- 连锁消除支持
- 自动触发下落和填充

#### InputManager - 输入处理

- 支持鼠标拖拽（编辑器/PC）
- 支持触摸滑动（移动端）
- 滑动方向检测
- 选中元素高亮

---

### 技术决策记录

| 决策 | 选择 | 原因 |
|------|------|------|
| 元素类型配置 | ScriptableObject | 易于编辑器配置，支持热更新 |
| 关卡数据格式 | JSON | 轻量、易读、便于批量生成 |
| 动画实现 | 协程+Lerp | 简单直接，后续可替换为DOTween |
| 匹配算法 | 暴力遍历 | 6x6棋盘性能足够，代码简洁 |

---

### 待解决问题

- [ ] Tile预制体需要在Unity中手动配置SpriteRenderer引用
- [ ] 需要添加正式的狗狗图片素材
- [ ] UI界面需要在Unity中搭建Canvas和布局

---

### 下一步计划

1. **Unity项目配置**
   - 创建Unity 2D项目
   - 导入脚本文件
   - 运行编辑器工具生成资源

2. **UI界面搭建**
   - 主菜单界面
   - 游戏内HUD
   - 胜利/失败弹窗

3. **美术资源**
   - 狗狗头像图片
   - 背景图
   - UI素材

---

## 文件清单

### Scripts/Core/
- `GameManager.cs` - 游戏主控制器
- `Board.cs` - 棋盘管理
- `Tile.cs` - 元素控制
- `TileType.cs` - 元素类型定义
- `MatchFinder.cs` - 匹配检测
- `MatchProcessor.cs` - 消除处理
- `InputManager.cs` - 输入处理
- `LevelLoader.cs` - 关卡加载

### Scripts/Data/
- `LevelData.cs` - 关卡数据结构

### Scripts/UI/
- `GameUI.cs` - 游戏内UI
- `MenuUI.cs` - 菜单UI

### Editor/
- `TileTypeCreator.cs` - 创建元素类型
- `GameSetupWindow.cs` - 场景搭建工具

### Resources/Levels/
- `level_1.json` ~ `level_5.json` - 5个测试关卡

---

## 2025-12-09 - 第二次更新：系统完善

### 完成内容

#### 1. 音频管理系统

新增 `AudioManager.cs` (Scripts/Audio/)

| 功能 | 说明 |
|------|------|
| BGM控制 | 菜单/游戏/胜利 三种背景音乐切换 |
| 音效播放 | 消除、交换、按钮点击、星星等 |
| 连击音效 | 支持不同连击层级播放不同音效 |
| 音量设置 | BGM/SFX 独立音量控制 |
| 静音开关 | BGM/SFX 独立静音 |
| 设置持久化 | 使用 PlayerPrefs 保存音量设置 |

#### 2. UI弹窗系统

| 脚本 | 功能 |
|------|------|
| `PopupBase.cs` | 弹窗基类，提供显示/隐藏动画 |
| `WinPopup.cs` | 胜利弹窗，星星动画、下一关/重玩/返回 |
| `LosePopup.cs` | 失败弹窗，重试/看广告/返回 |
| `PausePopup.cs` | 暂停弹窗，音量设置、继续/重玩/返回 |
| `UIManager.cs` | UI统一管理，响应游戏状态变化 |

**弹窗动画特性：**
- 使用 AnimationCurve 控制缓动
- 支持 CanvasGroup 透明度动画
- 支持内容面板缩放动画
- 使用 unscaledDeltaTime 确保暂停时动画正常

#### 3. 粒子特效系统

新增 `ParticleManager.cs` (Scripts/Effects/)

| 功能 | 说明 |
|------|------|
| 对象池 | 粒子系统对象池，避免频繁创建销毁 |
| 消除特效 | 元素消除时的粒子效果 |
| 连击特效 | 连击时增强的粒子效果 |
| 星星特效 | 胜利时星星飞入效果 |
| 降级方案 | 无粒子预制体时使用简单缩放效果 |

#### 4. 存档系统

新增 `SaveManager.cs` (Scripts/Data/)

| 功能 | 说明 |
|------|------|
| 关卡进度 | 当前关卡、每关星数、最高分 |
| 货币系统 | 金币、钻石的获取和消耗 |
| 道具系统 | 锤子、刷新、额外步数的数量管理 |
| 连续登录 | 自动检测连续登录天数 |
| 数据加密 | 使用 AES 加密存档，防止篡改 |
| 自动保存 | 关键操作后自动保存 |

**存档数据结构：**
```csharp
public class PlayerSaveData {
    int currentLevel;           // 当前解锁关卡
    int[] levelStars;           // 每关星数
    int[] levelHighScores;      // 每关最高分
    int coins, diamonds;        // 货币
    int hammerCount, ...;       // 道具数量
    int consecutiveLoginDays;   // 连续登录天数
    int totalMatchCount;        // 总消除次数
    int totalLevelCompleted;    // 总通关次数
}
```

---

### 新增文件清单

#### Scripts/Audio/
- `AudioManager.cs` - 音频管理器

#### Scripts/UI/
- `PopupBase.cs` - 弹窗基类
- `WinPopup.cs` - 胜利弹窗
- `LosePopup.cs` - 失败弹窗
- `PausePopup.cs` - 暂停弹窗
- `UIManager.cs` - UI管理器

#### Scripts/Effects/
- `ParticleManager.cs` - 粒子特效管理

#### Scripts/Data/
- `SaveManager.cs` - 存档管理器

---

### 当前项目结构

```
Assets/
├── Scripts/
│   ├── Core/           # 核心游戏逻辑 (8个文件)
│   ├── Data/           # 数据结构 (2个文件)
│   ├── UI/             # UI控制器 (6个文件)
│   ├── Audio/          # 音频系统 (1个文件)
│   └── Effects/        # 特效系统 (1个文件)
├── Editor/             # 编辑器工具 (2个文件)
├── Prefabs/            # 预制体
├── Resources/
│   ├── Sprites/        # 图片资源
│   ├── TileTypes/      # 元素类型配置
│   └── Levels/         # 关卡JSON (5个文件)
└── Scenes/             # 场景文件
```

**总计：20个 C# 脚本文件**

---

### 下一步计划

1. **Unity 场景搭建**
   - 创建 Menu 和 Game 场景
   - 搭建 UI Canvas 布局
   - 配置 Prefab 引用

2. **美术资源**
   - 狗狗头像图片（6种）
   - UI 素材（按钮、面板、图标）
   - 背景图

3. **音效资源**
   - BGM（菜单、游戏、胜利）
   - 消除音效（多层级）
   - UI 音效

4. **测试优化**
   - 功能测试
   - 性能优化
   - Bug 修复

---

## 2025-12-09 - 第三次更新：项目规范

### 完成内容

#### 添加项目宪法

新增 `.specify/memory/constitution.md` - 项目宪法文件

**核心原则：**

| 原则 | 说明 |
|------|------|
| I. ChangeLog 优先 | 每次提交前必须更新 DevLog（不可违反） |
| II. 代码质量 | 注释规范、命名规范、避免硬编码 |
| III. 文档同步 | 功能变更必须同步更新文档 |

**提交规范：**
- 使用 `type: subject` 格式
- 支持类型：feat/fix/docs/style/refactor/perf/test/chore

**提交流程：**
1. 完成代码修改
2. 更新 `doc/DevLog.md`（必须）
3. git add → commit → push

---

*最后更新：2025-12-09*
