# Pawzy Pop - Unity 项目设置指南

## 快速开始

### 1. 创建 Unity 项目

1. 打开 Unity Hub
2. 创建新项目，选择 **2D (URP)** 模板
3. 项目名称：`PawzyPop`
4. 将本仓库的 `Assets` 文件夹内容复制到 Unity 项目的 `Assets` 文件夹

### 2. 安装依赖

打开 Package Manager (Window > Package Manager)，安装：
- TextMeshPro（通常已预装）
- 2D Sprite（通常已预装）

### 3. 初始化游戏资源

在 Unity 编辑器中：

1. **创建 Tile 预制体**
   - 菜单：`PawzyPop > Create Tile Prefab`

2. **创建狗狗元素类型**
   - 菜单：`PawzyPop > Create Default Tile Types`

3. **设置游戏场景**
   - 新建场景或打开空场景
   - 菜单：`PawzyPop > Setup Game Scene`

### 4. 配置 Board

1. 选择 Hierarchy 中的 `Board` 对象
2. 在 Inspector 中设置：
   - **Tile Prefab**: 拖入 `Assets/Prefabs/Tile.prefab`
   - **Tiles Parent**: 拖入 `Board` 下的 `Tiles` 子对象
   - **Tile Types**: 拖入 `Assets/Resources/TileTypes` 下的所有 TileType 资源

### 5. 配置 InputManager

1. 选择 `InputManager` 对象
2. 设置 **Tile Layer**：
   - 创建新 Layer 叫 `Tile`
   - 将 Tile Prefab 的 Layer 设为 `Tile`
   - InputManager 的 Tile Layer 选择 `Tile`

### 6. 运行测试

按 Play 按钮，应该能看到：
- 6x6 的彩色棋盘
- 可以滑动交换元素
- 3连消除并得分

---

## 项目结构

```
Assets/
├── Scripts/
│   ├── Core/           # 核心游戏逻辑
│   ├── Data/           # 数据结构
│   └── UI/             # UI 控制器
├── Editor/             # 编辑器工具
├── Prefabs/            # 预制体
├── Resources/
│   ├── Sprites/        # 图片资源
│   ├── TileTypes/      # 元素类型配置
│   └── Levels/         # 关卡 JSON
└── Scenes/             # 场景文件
```

---

## 核心脚本说明

| 脚本 | 功能 |
|------|------|
| `GameManager` | 游戏状态、分数、步数管理 |
| `Board` | 棋盘生成、元素管理 |
| `Tile` | 单个元素的数据和动画 |
| `TileType` | 元素类型配置（ScriptableObject） |
| `MatchFinder` | 匹配检测算法 |
| `MatchProcessor` | 交换、消除、连锁处理 |
| `InputManager` | 触摸/鼠标输入处理 |
| `LevelLoader` | 关卡 JSON 加载 |

---

## 下一步开发

- [ ] 添加正式的狗狗图片素材
- [ ] 完善 UI 界面
- [ ] 添加音效
- [ ] 实现特殊道具
- [ ] 添加更多关卡
