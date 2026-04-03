# Phase 协作与依赖图

**日期：** `2026-04-02`  
**基线：** 当前 `dev`，WR2 以当前 prototype snapshot 为文档基线  
**目的：** 回顾全部 phase，明确哪些任务可以并行分给不同成员，哪些任务存在硬依赖，哪些需要等待

## 先说明当前 commit

- 最新团队分工与 PR 计划已经提交：`f61c4a5`
- Phase 1 课程优先重排已经提交：`dbf2b36`

## 判断规则

- `可并行`：可以由不同成员在不同分支上同时推进，不互相等结果
- `软依赖`：可以提前准备，但不能正式收口或合并
- `硬依赖`：前置没完成，后置就不应该开始或不可能正确完成
- `外部阻塞`：依赖 canonical repo、task board、聊天记录、真实 attendance 等 workspace 外证据

## 全局协作原则

- 文档线和代码线分开跑，不让所有人都卡在 WR2 文档上。
- 同一时间只允许一个“最终整合 PR”，其他人都交较小的 feature/docs/support PR。
- 凡是会影响 WR2 说法准确性的代码改动，优先级高于纯润色。
- 凡是只增强分数、不影响正确性的 supporting evidence，都不能拖住主线。
- `review/coursework` 和 `release/game` 默认都不做日常开发，只在包装阶段同步。

## 各 Phase 总览

| Phase | 主目标                               | 现在能否开始            | 适合并行的包                                                              | 主要硬依赖                      | 主要外部阻塞                    |
| ----- | --------------------------------- | ----------------- | ------------------------------------------------------------------- | -------------------------- | ------------------------- |
| 1     | WR2 收口与 snapshot 冻结               | 正在进行              | 文档基线、supporting docs、snapshot 复核、evidence 导入                        | `01-07` 依赖 `01-05 + 01-06` | repo / board / meeting 证据 |
| 2     | prototype 稳定性与可演示性                | 可做准备，谨慎落地         | UI 可读性、回合反馈、敌方回合反馈、最小准确性修复                                          | 需要 Phase 1 明确 WR2 基线后再大改   | 无硬外部阻塞                    |
| 3     | validation / test backbone        | 可先准备模板与 checklist | checklist、bug log、records、轻量自动化测试                                   | 需要相对稳定的 prototype          | 无硬外部阻塞                    |
| 4     | WR3 测试执行与风险清理                     | 不能现在正式执行          | 测试执行、bug 修复、retest、WR3 证据整合                                         | 强依赖 Phase 3 基础齐全           | 无硬外部阻塞                    |
| 5     | final report 与仓库包装                | 可提前准备一部分          | report packaging、review sync、release sync、appendix/source hierarchy | 需要 Phase 4 的已验证结果          | 可能仍受外部证据影响                |
| 6     | presentation / video / submission | 只能做准备，不宜最终录制      | slides、script、shot list、submission checklist                        | 依赖最终稳定 build 和 Phase 5 包装  | 无硬外部阻塞                    |

## Phase 1

### 当前任务块

- `01-05`
  - WR2 baseline / scope records
  - 核心是把 WR1 和当前 demo 的关系说清楚
- `01-06`
  - collaboration history / process support pack
  - 核心是诚实说明 GitLab、微信、Overleaf、GitHub 迁移
- `01-07`
  - WR2 正文、appendix、review sync checklist、PDF 重编译

### 可以并行分出去

- `Package P1-A`
  - owner：Guangjun
  - 内容：`01-05`
  - 文件：`wr2-snapshot-freeze-2026-04-02.md`、`wr2-asset-register.md`、`wr2-todo.md`、`wr2-todo-zh.md`
  - 分支建议：`docs/wr2-baseline-scope-alignment`

- `Package P1-B`
  - owner：Cheng
  - 内容：`01-06`
  - 文件：`process/README.md`、`collaboration-history-2026-04-02.md`、repo/board notes、recovered meeting note
  - 分支建议：`docs/collaboration-history-support-pack`

- `Package P1-C`
  - owner：Hongshuo
  - 内容：prototype snapshot 复核
  - 文件：架构图、Implementation Strategy 对应说明、必要时截图选择说明
  - 分支建议：`docs/prototype-snapshot-verification`

- `Package P1-D`
  - owner：Zehan
  - 内容：validation 与外部 evidence 导入
  - 文件：`validation/**`、`testing/**`、`process/canonical-repo/**`、`process/task-board/**`
  - 分支建议：`docs/validation-and-evidence-import`

### 依赖关系

- `01-05` 和 `01-06` 可并行
- `prototype snapshot verification` 可并行，但结果会影响 `01-07`
- `validation-and-evidence-import` 可并行，但它的增量结果会影响 `01-07`
- `01-07` 是整合任务，必须等待：
  - `01-05` 完成
  - `01-06` 完成
  - `prototype snapshot verification` 给出结论

### 需要阻塞等待的点

- 如果 Hongshuo 发现“当前 demo 与 WR2 说法不一致”，则 `01-07` 需要等一个最小代码修复 PR
- 如果 repo / board 截图还没拿到，不阻塞 `01-07` 开始写，但会阻塞“把 supporting evidence 写成 stronger claim”
- 最终 PDF 导出必须等：
  - 正文措辞定稿
  - appendix 引用稳定
  - 是否采用外部证据的策略已决定

### 最不该阻塞主线的点

- canonical repo 截图
- task board 截图
- recovered meeting note 的更强替换证据

这些都只影响 supporting strength，不该拖死 WR2 主体。

## Phase 2

### 推荐拆法

- `Package P2-A: UI readability`
  - 选中单位、当前回合、卡牌区、状态提示
  - 建议 owner：Hongshuo
  - 分支建议：`feat/ui-readability-and-turn-feedback`

- `Package P2-B: action feedback`
  - `Move / Strike / Guard / Push` 的合法目标与结果反馈
  - 建议 owner：Hongshuo 或 Cheng 配合
  - 分支建议：`feat/card-target-feedback`

- `Package P2-C: enemy response and end-state clarity`
  - 敌方回合可见性、战斗结束反馈
  - 建议 owner：Hongshuo
  - 分支建议：`feat/enemy-turn-and-endstate-feedback`

- `Package P2-D: phase-2 validation prep`
  - 更新 walkthrough/checklist，准备验证脚本
  - 建议 owner：Zehan
  - 分支建议：`docs/phase2-validation-prep`

### 依赖关系

- `P2-A`、`P2-B`、`P2-C` 可以并行，但前提是写入范围尽量分开
- `P2-D` 可以并行准备，但最终 checklist 需要以最新 demo 为准

### 需要阻塞等待的点

- 如果多个任务都改 `BattleUI`、同一个 scene、同一批 prefab，就需要先切写入边界，否则会互相覆盖
- Phase 2 的“验收通过”必须等：
  - UI 可读性收口
  - 核心四张卡反馈稳定
  - 敌方回合/结束态可演示

### 软依赖

- 最好等 Phase 1 的 WR2 snapshot 已冻结，再合并大一点的体验改动
- 但可以先在各自分支上做，不必全部原地等着

## Phase 3

### 推荐拆法

- `Package P3-A: core-loop checklist`
  - 把核心流程变成可重复执行 checklist
  - owner：Zehan
  - 分支建议：`test/core-loop-checklist`

- `Package P3-B: records backbone`
  - meeting / decision / validation / testing log 的正式续写规则
  - owner：Guangjun 或 Zehan
  - 分支建议：`docs/testing-records-backbone`

- `Package P3-C: lightweight automated tests`
  - 优先做规则层，不碰大规模 Unity 场景测试
  - 目标可以是 grid range、card resolution、turn order 小逻辑
  - owner：Hongshuo
  - 分支建议：`test/rule-layer-smoke-tests`

### 依赖关系

- `P3-A` 和 `P3-B` 可并行
- `P3-C` 可以并行探索，但正式落地前最好确认 Phase 2 的相关逻辑不再大改

### 需要阻塞等待的点

- checklist 不能在 prototype 还频繁变时就定死
- 自动化测试如果依赖频繁变动的接口，最好等代码接口稍稳后再写

## Phase 4

### 推荐拆法

- `Package P4-A: test execution`
  - 执行 Phase 3 checklist 和更多测试案例
  - owner：Zehan
  - 分支建议：`test/wr3-execution-results`

- `Package P4-B: critical bug fixing`
  - 按 bug 写入范围拆多个修复分支
  - owner：Hongshuo
  - 分支建议：`fix/<bug-topic>`

- `Package P4-C: retest and evidence packaging`
  - 修完后重测并整理 WR3 数据
  - owner：Guangjun + Zehan
  - 分支建议：`docs/wr3-retest-and-evidence`

### 依赖关系

- `P4-A` 先产出 bug 列表
- `P4-B` 基于 bug 列表并行修
- `P4-C` 必须等关键修复完成后再做最终 retest

### 需要阻塞等待的点

- WR3 的最终测试结果必须等关键 bug 修复完成
- 如果同一模块连续出现回归，retest 需要等待该模块稳定

## Phase 5

### 推荐拆法

- `Package P5-A: final report package`
  - final report 主文、appendix、source hierarchy
  - owner：Guangjun
  - 分支建议：`docs/final-report-package`

- `Package P5-B: review/coursework sync`
  - 把老师要看的材料同步到 `review/coursework`
  - owner：Cheng
  - 分支建议：`chore/review-coursework-sync`

- `Package P5-C: release/game sync`
  - 维护轻量游戏分支
  - owner：Hongshuo
  - 分支建议：`chore/release-game-sync`

### 依赖关系

- `P5-B` 和 `P5-C` 可以并行
- `P5-A` 可以先写结构，但最终收口必须等 Phase 4 的验证结果

### 需要阻塞等待的点

- final report 不能早于 WR3 结果定稿
- `review/coursework` 和 `release/game` 的最终同步，要等最终 build / report 快照稳定

## Phase 6

### 推荐拆法

- `Package P6-A: presentation deck and speaking script`
  - owner：Guangjun + Cheng
  - 分支建议：`docs/presentation-pack`

- `Package P6-B: video shot list and recording prep`
  - owner：Hongshuo + Zehan
  - 分支建议：`docs/video-shot-plan`

- `Package P6-C: final submission checklist`
  - owner：Zehan
  - 分支建议：`docs/submission-checklist`

### 依赖关系

- slides 和 shot list 可以并行准备
- 真正录视频必须等最终稳定 build
- 最终 submission checklist 必须等所有材料名称、版本、分支都定下来

### 需要阻塞等待的点

- 演示视频录制必须等待“已验证最终快照”
- 最终提交打包必须等：
  - final report
  - final build
  - presentation/video
  - branch packaging

## 跨 Phase 的真实阻塞点

### 硬阻塞

- WR2 最终收口，等待 Phase 1 的 `01-05 + 01-06 + snapshot verification`
- Phase 2 正式验收，等待 WR2 snapshot 明确后再判断“偏差修复”边界
- Phase 4 正式执行，等待 Phase 3 checklist / records backbone 成型
- Phase 5 最终打包，等待 Phase 4 测试结果
- Phase 6 最终录制与提交，等待 Phase 5 的最终 package

### 软阻塞

- 外部 repo / board 证据
- 更强的 playtest 证据
- 更强的 meeting attendance 证据

这些可以增强分数，但不应该让主线所有人停工。

## 最推荐的并行策略

### 现在到 WR2 提交前

- Guangjun：跑 `01-05`
- Cheng：跑 `01-06`
- Hongshuo：做 snapshot / architecture / implementation claim 复核
- Zehan：整理 validation 和外部 evidence import

然后：

- 只有在 Hongshuo 确认确实存在“WR2 会说错”的代码偏差时，才开最小代码修复 PR
- 最后由 Guangjun 做 `01-07` 最终整合

### WR2 交后立刻

- Hongshuo：直接进 Phase 2 代码改进
- Zehan：直接进 Phase 3 checklist / bug log / validation backbone
- Guangjun：开始准备 Phase 5 的 final report 结构框架
- Cheng：持续维护图、截图、appendix 索引与 review branch file set

## 一句话结论

真正不该阻塞团队的是 supporting evidence；真正需要等待的是“当前 demo 会不会让报告说错”以及“最终整合 PR 是否已经拿到前置结果”。

