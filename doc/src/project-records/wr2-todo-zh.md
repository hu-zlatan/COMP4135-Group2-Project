# WR2 中文任务清单

这份清单是当前 WR2 的中文工作拆解，同时覆盖 WR3 / final report 需要提前铺好的最小留痕基础。

迁移说明：
这份 TODO 从旧的 WR2 收口工作区迁移到当前 clean `develop` 基线。
其中已勾选状态表示当时 WR2 快照下的收口记录，不等于当前 clean 分支已经包含所有被引用的 WR2 打包文件。

## 优先级说明

- `P0`：阻塞 WR2 提交
- `P1`：强烈建议在提交前完成
- `P2`：如果时间吃紧，可延后到 WR2 后继续

## 状态标签

- `Required now`：当前冻结快照下，WR2 收口必须完成
- `Blocked externally`：依赖本 workspace 之外的仓库、任务板或团队确认
- `Deferred later`：对 WR3 / final 有用，但不属于最小 WR2 收口

## W0 控盘与冻结线

### `P0` 证据控盘

- [x] 在 `doc/src/wr2-latex/` 下建立 WR2 源目录
- [x] 在 `doc/src/project-records/` 下建立项目留痕目录
- [x] 建立 WR2 素材总表
- [x] 设定 WR2 证据冻结线为 `2026-04-02 12:00`
- [ ] [Blocked externally] 从 canonical team repo 收集缺失的外部证据
- [ ] [Blocked externally] 从团队任务板 / kanban 收集缺失的外部证据

### `P1` 团队同步

- [ ] [Required now] 开一次 15 分钟短会，确认 WR2 中哪些表述已有充分证据
- [ ] [Required now] 给剩余缺失证据分配负责人
- [x] 确认哪一个 prototype 快照作为 WR2 最终快照

## W1 WR2 源文件重建

### `P0` 文档骨架

- [x] 用真正的 WR2 LaTeX 结构替换错误的 WR1 风格源文件
- [x] 建立正确章节：
  - [x] Specification and Design Decisions
  - [x] Prototyping Process
  - [x] Implementation Strategy
  - [x] Summary and Reflections
  - [x] Reference
  - [x] Meeting Minutes / Appendix
- [x] 建立稳定的 `media/`、`figures/`、`appendices/` 和 `references.bib`
- [x] 成功编译 WR2 PDF

### `P1` 提交前收口

- [ ] [Required now] 请一位未主写该章节的人做一次最终内容校对
- [ ] [Required now] 确认所有姓名、日期、模块名与团队最终说法一致

## W2 Requirements Validation

### `P0` 范围确认

- [x] 从现有 WR1 中提取核心承诺
- [x] 总结 WR1 到 WR2 的需求验证情况
- [x] 记录哪些功能被保留
- [x] 记录哪些功能被简化
- [x] 记录哪些功能被延后

### `P1` 证据加固

- [ ] [Blocked externally] 如果团队手上有更强的 repo 或 playtest 证据，替换掉偏弱的表述
- [ ] [Required now] 确认 WR2 没有任何段落声称实现了最终 prototype 快照中不存在的功能

## W3 架构与系统图

### `P0` 必要图表

- [x] 补一张与当前代码模块一致的架构图
- [x] 补一张简化的模块 / 类职责图
- [x] 确认图表覆盖：
  - [x] `GameRoot`
  - [x] `TurnManager`
  - [x] `GridManager`
  - [x] `DeckManager`
  - [x] `CardResolver`
  - [x] `UnitController`
  - [x] `EnemyAI`
  - [x] `BattleUI`

### `P1` 交叉确认

- [ ] [Required now] 让主要 Unity 实现者确认这些图仍与当前 scene / code 一致

## W4 Prototype 证据

### `P0` 高保真证据

- [x] 收集当前 battle UI 截图
- [x] 收集当前棋盘截图
- [x] 在 WR2 中插入 prototype 截图
- [x] 写出 prototype 形成过程说明

### `P0` 低保真证据

- [x] 补一份回溯式 low-fidelity wireframe
- [x] 明确标注它是 retrospective，而不是当时保存下来的原始证据

### `P1` 功能状态清晰化

- [x] 总结已实现 / 部分实现 / 延后的 prototype 功能
- [ ] [Deferred later] 如果团队手上有更好的截图集，在最终导出前替换当前图片

## W5 Design Validation

### `P0` 基线验证

- [x] 建立 validation log 模板
- [x] 记录一份 WR2 heuristic walkthrough
- [x] 覆盖至少这些场景：
  - [x] 首次交互与单位选中
  - [x] 打出一张卡并确认目标 / 反馈
  - [x] 结束回合并观察敌方响应
- [x] 提炼至少 3 条真实发现

### `P1` 强化验证

- [ ] [Deferred later] 做一次真实团队 playtest，并追加到 validation log
- [ ] [Deferred later] 如果能拿到真实用户证据，就把仅基于 heuristic 的表述替换掉

## W6 Implementation Strategy

### `P0` 代码结构

- [x] 加入当前脚本目录树
- [x] 解释各文件夹职责
- [x] 加入简化依赖 / 职责图

### `P0` 关键决策

- [x] 记录单场战斗 vertical slice 的决策
- [x] 记录共享 `UnitController` 的决策
- [x] 记录 `enum + CardResolver` 的决策
- [x] 记录确定性 `EnemyAI` 的决策
- [x] 记录当前 UI / 输入实现的选择
- [x] 记录“仍需外部 repo / board 证据”的事实

### `P1` 外部过程证据

- [ ] [Blocked externally] 插入 canonical repo 的真实截图，增强 process 分
- [ ] [Blocked externally] 插入任务板截图，增强 planning / kanban 证据

## W7 Summary、Appendix 与 WR3 基础

### `P0` 报告收尾部分

- [x] 更新实现阶段的分工
- [x] 写完成过程总结
- [x] 写面向 WR3 的 future plan
- [x] 补 appendix 内容，避免 WR2 附录为空

### `P0` 留痕基础

- [x] 加入 `meeting minutes` 模板
- [x] 加入 `decision log` 模板
- [x] 加入 `validation log` 模板
- [x] 加入 `test / bug log` 模板
- [x] 加入一份 recovered implementation checkpoint note
- [x] 加入一份 WR3 prep testing note

### `P1` 尽量替换 recovered 证据

- [ ] [Blocked externally] 如果能拿到真实出席信息，用确认过的 attendance 替换 recovered checkpoint
- [ ] [Blocked externally] 如果团队有原始聊天记录或草稿，补至少一份正式 implementation-stage meeting record

## W8 最终审核与提交

### `P0` 提交检查

- [x] 构建 WR2 PDF
- [x] 运行 BibTeX 并重建引用
- [x] 确认标题是 Weekly Report 2，而不是 Weekly Report 1
- [x] 确认章节名称与 WR2 模板结构一致
- [x] 确认 bibliography 不为空
- [x] 确认 appendix 存在

### `P1` 提交前复核

- [ ] [Required now] 再做一次逐条证据核对
- [ ] [Required now] 确认所有图注和段落措辞已定稿
- [ ] [Required now] 导出并留存最终实际提交的 PDF
- [ ] [Deferred later] 保留 LaTeX 源与 supporting records，方便 WR3 / final report 复用

## 建议分包

### Package A：文档整合

- [ ] [Required now] 对 `wr2-latex` 做最终措辞收口
- [ ] [Required now] 做最终 PDF 导出检查
- [ ] [Required now] 做最终证据一致性检查

### Package B：需求与项目管理

- [ ] [Required now] 确认 WR1 到 WR2 的验证表述
- [ ] [Required now] 确认实现阶段分工表述
- [ ] [Blocked externally] 如果有证据，替换 recovered meeting details

### Package C：架构与实现

- [ ] [Required now] 复核架构图与模块图
- [ ] [Required now] 复核 Implementation Strategy 中的实现表述
- [x] 确认最终采用的 prototype 快照

### Package D：验证与过程证据

- [ ] [Deferred later] 如果可能，再做一次聚焦 playtest
- [ ] [Blocked externally] 收集团队 canonical repo 截图
- [ ] [Blocked externally] 收集团队任务板截图
- [ ] [Deferred later] 把新增发现补进 validation / testing logs

## 快速入口

- WR2 主源：`doc/src/wr2-latex/`
- WR2 PDF：`doc/src/wr2-latex/main.pdf`
- 素材总表：`doc/src/project-records/wr2-asset-register.md`
- 英文 TODO：`doc/src/project-records/wr2-todo.md`
- 中文 TODO：`doc/src/project-records/wr2-todo-zh.md`
