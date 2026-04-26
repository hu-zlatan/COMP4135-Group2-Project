# WR3 Sample 框架与得分点提炼

基于课程规范、模板和 sample OCR 结果，这份 sample 的核心价值不在“篇幅长”，而在于它把 `测试计划 -> 执行证据 -> 结果分析 -> 反思改进` 串成了闭环，尤其通过表格和图表把“我们真的做了测试”具象化了。

## 1. 报告框架

模板基线是：

1. `Process Management`
2. `Test Strategy`
3. `Test Plan`
4. `Validation Results`
5. `Summary and Reflections`
6. `Reference`
7. `Meeting Minutes / Appendix`

sample 在模板基础上做了两个加分扩展：

- 把第 1 章写得更像“测试阶段的项目管理”，而不是泛泛介绍开发流程。
- 把第 4 章写成“有数据的验证结果”，不只是口头说“测试做了”。

## 2. 从 sample 提炼出的高价值得分点

### A. 管理与过程分

- 不是只说“我们用了 Agile”，而是先说明为什么考虑过 Waterfall，再解释为什么落到 lightweight Agile。
- 用图展示团队协作流程和角色交互，而不是只写角色名单。
- 明确写 deliverables / version milestones，例如 `v1.0 -> v2.0 -> v3.0`，体现阶段性交付。
- 给出 GitLab tag / branch / merge 过程证据，说明测试并非脱离仓库流程单独进行。
- 提到 Kanban、issue 分配、branch merge review，直接对应课程规范里提到的 repository / Kanban / planning / teamwork。

### B. 测试策略分

- 明确区分 `Verification` 和 `Validation`。
- 不只列测试类型，而是说明为什么这些类型适合当前项目。
- 把 Unity 场景下测试困难点写出来，例如引擎生命周期、组件耦合、实时行为难以纯单测模拟。
- 写 smoke testing 和 regression testing 的触发时机、目的、通过标准。
- 写测试环境时，不只是“Unity 版本”，还写测试框架、覆盖率工具、执行机器环境。

### C. 测试计划分

- sample 不只给模板里的一个示例表，而是把测试计划和 repo / issue / branch 工作流关联起来。
- 测试计划不是抽象任务表，而是下钻到模块级、类级、函数级。
- 附录里出现了按 class/test case 展开的测试表，说明 planning 不是空话。

### D. 验证结果分

- 这是 sample 最亮的部分。
- 它没有只说“覆盖率很高”，而是同时给：
  - 覆盖率图
  - requirements coverage matrix
  - 执行统计
  - defect severity 分布
  - defect cause analysis
  - 改进建议
- 这会让第 4 章从“结果描述”升级成“证据驱动分析”。

### E. 反思与总结分

- 不是简单写“大家分工明确”，而是说明测试阶段角色如何调整。
- 不只是总结做完了什么，还写下阶段计划。
- 用 milestone/deadline 表把未来计划具体化。

## 3. sample 里最值得模仿的图表/表格

下面这些是最值得保留或仿造的内容。

### 3.1 图：开发方法 / 团队协作流程图

sample 特征：

- 有 `Waterfall` 和 `Agile` 的示意图。
- 有 team role / interaction 图，说明 PM、developer、tester、writer 之间如何协作。

为什么加分：

- 让“团队 workflow and collaboration approach”一眼可见。
- 比纯文字更容易体现你们对 process 的理解。

建议你保留：

- 一张测试阶段协作流程图。
- 一张从需求/issue 到 test branch 到 merge 的流程图。

### 3.2 图：Git tag / release evidence

sample 特征：

- 展示了版本 tag 记录，配合 `v1.0 / v2.0 / v3.0` 的阶段性交付。

为什么加分：

- 直接证明不是事后编造流程。
- 能把仓库使用、版本管理、deliverables 连接起来。

建议你保留：

- Git tags / release page 截图，或 milestone/version timeline 图。

### 3.3 图：Code Coverage Metrics

sample 特征：

- 有 `Figure 4.1: Line Coverage by Assembly`。
- 不只讲整体覆盖率，还拆到模块/assembly。

为什么加分：

- 属于“测试结果有量化证据”的典型加分项。
- 比只写一句 “coverage good” 强很多。

建议你保留：

- 一张覆盖率总览图。
- 文中点名 2-3 个高覆盖模块和 1-2 个低覆盖模块。

### 3.4 表：Requirements Coverage Matrix

sample 特征：

- 把 `Req ID -> Description -> Test Case(s) -> Status` 对齐。

为什么加分：

- 这是最典型的“测试不是乱测，而是对需求负责”的证据。
- 同时覆盖 testing、requirements traceability、documentation 三项。

建议你至少做成：

| Req ID | Requirement | Test Case(s) | Status | Notes |
| --- | --- | --- | --- | --- |
| R-1 | ... | ... | Covered / Partial / Not covered | ... |

这是 WR3 里最值得优先做好的表。

### 3.5 表：Defect Statistics / Severity Summary

sample 特征：

- 有缺陷总数、严重级别、测试阶段分布。
- 还补了 defect cause analysis 和改进建议。

为什么加分：

- 说明你们不仅“发现 bug”，还会分类、归因、闭环处理。
- 很符合规范里“how issues will be documented, tracked, and addressed”。

建议你至少做成两张：

- `Defect Summary Table`
- `Defect Cause / Fix / Prevention Table`

可用结构：

| ID | Module | Severity | Symptom | Root Cause | Fix Status |
| --- | --- | --- | --- | --- | --- |

### 3.6 表：Milestones and Deadlines

sample 特征：

- 在 summary/future plan 里仍然用表展示 milestone。

为什么加分：

- 说明反思不是空话，而是落到下一阶段计划。

建议你保留：

- `Milestone / Deadline / Owner / Deliverable`

### 3.7 附录测试用例表

sample 特征：

- 把 class / test / input / expected outcome / result / remark 细化成大量表格。

为什么加分：

- 是最强的“我们真的写了测试”的证据。
- 适合作为 appendix，不会挤爆正文。

但注意：

- 这个部分很耗时，且 sample 明显做得比较重。
- 如果时间有限，不需要照抄 sample 的体量。
- 正文保留总表，附录只放最关键模块的代表性测试即可。

## 4. 可直接照搬的写法逻辑

你可以把 WR3 组织成下面这个“高分但不离谱”的版本：

### 正文必须有

- `1. Process Management`
  - 为什么采用当前开发/测试流程
  - 团队测试期分工
  - 版本里程碑和仓库工作流
- `2. Test Strategy`
  - Verification vs Validation
  - Test types
  - Testing environment
  - Regression / smoke strategy
- `3. Test Plan`
  - 模块级测试计划总表
  - issue / branch / reviewer 流程说明
- `4. Validation Results`
  - 覆盖率结果
  - requirements coverage matrix
  - test execution stats
  - defect summary
  - defect cause + fix + next action
- `5. Summary and Reflections`
  - 分工
  - 本轮完成内容
  - 下一步计划

### 正文优先放的图表

- 1 张团队协作/测试流程图
- 1 张版本/tag/Kanban 证据图
- 1 张覆盖率图
- 2 张核心表
  - requirements coverage matrix
  - defect summary / root cause table
- 1 张 milestone/deadline 表

### 附录放什么最划算

- 代表性 test case tables
- meeting minutes
- test plan 原始 PDF / 截图 / issue list

## 5. 从课程规范倒推的“关键得分项”

结合规范，WR3 最容易拿分的点不是“把测试术语写得很花”，而是下面这些：

- 你们的测试计划是否和需求、模块、repo 流程真正对应。
- 是否能证明团队每个人都参与了 testing-related work。
- 是否有 sensible unit tests，而且能展示运行证据。
- 是否有 issue tracking / bug fixing / regression 的闭环。
- 是否有 documentation evidence，例如 Kanban、meeting minutes、assigned roles、test records。
- 是否有 reflection，而不是只报喜不报忧。

## 6. 最值得你借鉴的亮点

- 把 `测试` 写成了 `工程管理 + 质量保证`，而不是只写“我们测了哪些按钮”。
- 用 `requirements coverage matrix` 连接需求和测试，是 sample 里最强的亮点之一。
- 用 `coverage + defect + fix status` 形成闭环，是第 4 章最强的亮点。
- 把 Git tags / Kanban / branch review 写进 WR3，明显比模板要求更完整。
- 附录给出 test case evidence，让正文更有说服力。

## 7. 如果你时间有限，优先做这 5 件事

1. 做好 `Requirements Coverage Matrix`
2. 放一张覆盖率图或测试统计图
3. 做一张 `Defect Summary / Root Cause / Fix Status` 表
4. 放一张测试流程或仓库流程图
5. 在 summary 里加 `next milestone` 表

这 5 项基本就是 sample 最值得吸收、且性价比最高的部分。
