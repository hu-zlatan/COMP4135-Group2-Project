# Git / PR 协作规则

**日期：** `2026-04-02`  
**适用仓库：** `hu-zlatan/COMP4135-Group2-Project`  
**当前主工作区：** `F:\unity\Unity-Project-clean-init`

## 当前基线

- 后续协作以当前干净 worktree 为主，不再回到旧的脏工作区直接开发。
- 旧本地 `dev` 分支上的内容，不直接整包覆盖到当前远端基线。
- 旧本地 `dev` 的内容必须先整理，再以 PR 形式逐步并入当前远端分支。
- 目前已经完成过一次“push -> PR -> merge -> 本地 pull”流程，后续统一遵循这条路径。

## 统一工作流

### 1. 开始工作

- 先在当前干净 worktree 上 `pull` 最新远端基线。
- 从当前基线切出一个新的工作分支。
- 一个分支只做一个明确主题，不混多个不相关任务。

### 2. 提交工作

- 本地小步提交可以自由进行。
- 只有整理清楚、适合 review 的内容才开 PR。
- 旧 `dev` 分支里的历史改动，如果要带过来，必须先按主题拆分，再分别走 PR。

### 3. 合并规则

- 不直接在基线分支上提交功能或文档改动。
- 所有改动都通过 PR 合并。
- merge 之后，本地 worktree 立刻同步远端，再开始下一轮工作。

## Review 规则

### 文档类 PR

以下内容视为文档类 PR：

- `doc/**`
- `project-records/**`
- UML、图表、appendix、meeting / validation / testing 记录
- 纯说明性脚本或不会改变游戏行为的仓库整理

合并门槛：

- 你或者团队中任意一人 review 通过即可合并
- 最低要求：`1` 个有效 review

### 代码类 PR

以下内容视为代码类 PR：

- `coursework/Assets/Scripts/**`
- scene / prefab / gameplay wiring
- 会影响运行行为、演示行为、测试结果、玩家交互的任何改动

合并门槛：

- 必须由你 review
- 还必须再有任意另一位成员 review
- 也就是至少 `2` 人 review，其中一个必须是你

## 特殊情况

### 混合型 PR

如果一个 PR 同时包含文档和代码，但代码会影响运行行为：

- 按代码类 PR 处理
- 不按文档类低门槛放行

### 旧 `dev` 内容迁移

旧 `dev` 分支里的内容分三类处理：

- 可以直接复用的文档整理：拆成文档 PR
- 当前基线缺失、但仍有价值的 supporting records：拆成文档 PR
- 会影响当前 demo 行为的实现改动：拆成代码 PR

禁止做法：

- 不允许把旧 `dev` 整体强推覆盖当前远端基线
- 不允许把旧 `dev` 的大量散乱内容一次性塞进一个超大 PR

## 推荐分支命名

- `docs/<topic>`
- `feat/<topic>`
- `fix/<topic>`
- `test/<topic>`
- `chore/<topic>`

## 推荐基线分支策略

- 当前干净 worktree 对应的工作线作为日常协作入口
- 每次 merge 完后，所有人先同步，再开下一条分支
- `review/coursework` 和 `release/game` 继续只做发布/包装用途，不做日常开发

## 一句话规则

- 文档 PR：`1` 个 review 就能合
- 代码 PR：必须你 + 另一人，共 `2` 个 review 才能合
- 旧 `dev`：只能整理后通过 PR 渐进并入

