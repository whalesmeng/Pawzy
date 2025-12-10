<!--
Sync Impact Report
==================
- Version change: 0.0.0 → 1.0.0
- Added principles:
  - I. ChangeLog 优先
  - II. 代码质量
  - III. 文档同步
- Added sections:
  - 提交规范
  - 开发流程
- Templates status: ✅ No updates required (initial version)
- Follow-up TODOs: None
-->

# Pawzy Pop 项目宪法

## Core Principles

### I. ChangeLog 优先（NON-NEGOTIABLE）

**每次 Git 提交前，必须先更新 ChangeLog。**

- 在 `doc/DevLog.md` 中记录本次变更内容
- 记录格式：日期 + 变更摘要 + 详细内容
- 提交信息（commit message）需与 ChangeLog 内容一致
- 禁止空提交或无意义的提交信息

**理由**：确保项目历史可追溯，方便团队协作和问题排查。

### II. 代码质量

- 所有 C# 脚本必须有清晰的注释
- 公共方法必须有 XML 文档注释
- 遵循 Unity 最佳实践和命名规范
- 避免硬编码，使用配置文件或 ScriptableObject

**理由**：保持代码可维护性，降低后续开发成本。

### III. 文档同步

- 功能变更必须同步更新相关文档
- 新增系统必须在 `doc/DevLog.md` 中记录
- API 变更需更新 `UNITY_SETUP.md`

**理由**：文档是项目的重要组成部分，必须保持与代码同步。

## 提交规范

### Commit Message 格式

```
<type>: <subject>

<body>
```

**Type 类型**：
| 类型 | 说明 |
|------|------|
| feat | 新功能 |
| fix | Bug 修复 |
| docs | 文档更新 |
| style | 代码格式（不影响功能） |
| refactor | 重构 |
| perf | 性能优化 |
| test | 测试相关 |
| chore | 构建/工具变更 |

### 提交流程

1. 完成代码修改
2. **更新 `doc/DevLog.md`**（必须）
3. `git add -A`
4. `git commit -m "type: 描述"`
5. `git push`

## 开发流程

### 功能开发

1. 在 DevLog 中记录计划
2. 创建功能分支（可选）
3. 实现功能
4. 更新 DevLog
5. 提交代码

### Bug 修复

1. 在 DevLog 中记录问题
2. 修复 Bug
3. 更新 DevLog（记录解决方案）
4. 提交代码

## Governance

- 本宪法是项目的最高准则
- 所有提交必须遵守 ChangeLog 优先原则
- 修改宪法需要在 DevLog 中记录理由
- 版本号遵循语义化版本规范（MAJOR.MINOR.PATCH）

**Version**: 1.0.0 | **Ratified**: 2025-12-09 | **Last Amended**: 2025-12-09
