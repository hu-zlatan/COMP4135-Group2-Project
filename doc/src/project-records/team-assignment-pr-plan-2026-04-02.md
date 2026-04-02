# 团队分工与 PR 计划

**日期：** `2026-04-02`  
**适用范围：** WR2 收口、Phase 1 执行、GitHub 迁移后的第一轮真实协作  
**目标分支：** `dev`

## 使用原则

- 本表直接对齐当前 WR2 里的 implementation-phase 分工表述，避免报告、TODO、PR 计划互相打架。
- 本表优先服务 WR2 截止前的收口，不把 WR3 / final 的大任务硬塞进本轮。

## 当前团队分工表

| 成员 | 当前主角色 | 本轮主交付 | 辅助责任 | 必须参与 review |
| --- | --- | --- | --- | --- |
| Guangjun HU | WR2 总整合 / requirements validation / appendix owner | 收口 WR2 范围、统一措辞、最终 PDF | 审核需求追踪、确认名字日期与引用 | `docs/collaboration-history-support-pack`、`docs/validation-and-evidence-import` |
| Hongshuo HU | Unity 原型 / architecture / runtime owner | 确认当前 demo snapshot、核对架构图与实现一致 | 如有必要提交最小 demo 准确性修复 | `docs/validation-and-evidence-import`、`docs/wr2-report-closeout` |
| Cheng YE | UML / figures / supporting chapter owner | 图表、附录索引、过程性 supporting docs 整理 | 帮助同步 `review/coursework` 所需文件集 | `docs/wr2-baseline-scope-alignment`、`docs/prototype-snapshot-verification` |
| Zehan CHEN | validation / testing / records owner | validation 记录、外部证据整理、最终证据一致性复核 | 如有条件补一次 focused playtest | `docs/wr2-report-closeout`、`feat/prototype-accuracy-fixes` |

## 各成员 TODO

### Guangjun HU

- [ ] 完成 `01-05` 对应的文档收口：
  - 更新 `wr2-snapshot-freeze-2026-04-02.md`
  - 更新 `wr2-asset-register.md`
  - 更新 `wr2-todo.md`
  - 更新 `wr2-todo-zh.md`
- [ ] 复核 WR1 -> WR2 的 requirements validation 文字，确认没有把“已有 demo”写成“未来工作”。
- [ ] 做 WR2 最终措辞统一：
  - 名字、日期、模块名、章节名
  - 引用 supporting records 的地方是否一致
- [ ] 负责最终提交前的 PDF 导出确认。

### Hongshuo HU

- [ ] 确认当前 Unity scene / code 与 WR2 里的架构图、模块图、Implementation Strategy 一致。
- [ ] 复核这些实现主张是否都能被当前 demo 证明：
  - `Move / Strike / Guard / Push`
  - turn flow
  - enemy response
  - UI feedback
- [ ] 如果 report 和当前 demo 有偏差，提交一个最小准确性修复 PR，只修 WR2 会说错的点。
- [ ] 确认最终截图使用的就是当前被选中的 prototype snapshot。

### Cheng YE

- [ ] 完成 `01-06` 对应的 supporting docs：
  - `process/README.md`
  - `collaboration-history-2026-04-02.md`
  - `canonical-repo-evidence-2026-04-02.md`
  - `task-board-evidence-2026-04-02.md`
  - `implementation-checkpoint-recovered-2026-03-15.md`
- [ ] 复核 UML、架构图、wireframe、appendix 索引是否齐全且文件路径正确。
- [ ] 帮助整理 `review/coursework` 最后需要同步的文件集合。
- [ ] 协助 Guangjun 做章节衔接和 supporting chapter drafting。

### Zehan CHEN

- [ ] 复核现有 `validation` 和 `testing` records 是否与当前 demo 行为一致。
- [ ] 若团队手上有材料，导入外部证据：
  - canonical team repo 截图
  - real board / kanban 截图
  - implementation meeting attendance / raw note
- [ ] 做一次提交前证据复核：
  - 每一条 WR2 关键主张是否能指回图、截图、记录或代码结构
  - appendix 是否能支撑正文说法
- [ ] 如有时间，组织一次 focused playtest，并把结论补到 validation log。

## 建议提交的 PR

> 原则：先并行做基础收口，再做最终整合 PR。所有 PR 默认目标分支都是 `dev`。

| 顺序 | 分支名 | PR 标题建议 | Owner | Reviewer | 类型 | 主要文件 / 范围 | 说明 |
| --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | `docs/wr2-baseline-scope-alignment` | `docs: align WR2 baseline and scope records` | Guangjun HU | Cheng YE | docs | `wr2-snapshot-freeze-2026-04-02.md`、`wr2-asset-register.md`、`wr2-todo.md`、`wr2-todo-zh.md` | 对应 `01-05`，把 WR2 明确锚到“WR1 已完成 + 当前 demo 已存在” |
| 2 | `docs/collaboration-history-support-pack` | `docs: add collaboration history and support evidence notes` | Cheng YE | Guangjun HU | docs | `process/README.md`、`collaboration-history-2026-04-02.md`、repo/board evidence notes、recovered meeting note | 对应 `01-06`，诚实说明 GitLab/微信/Overleaf/GitHub 迁移史 |
| 3 | `docs/prototype-snapshot-verification` | `docs: verify prototype snapshot and architecture alignment` | Hongshuo HU | Cheng YE | docs | UML、截图选择、Implementation Strategy 相关说明 | 如果发现 report 与当前 demo 有偏差，这个 PR 先把“说法”和“快照”对齐 |
| 4 | `feat/prototype-accuracy-fixes` | `feat: apply minimal fixes for WR2 snapshot accuracy` | Hongshuo HU | Zehan CHEN | code | `coursework/Assets/Scripts/**`、必要 scene / prefab 改动 | 只在 demo 行为会让 WR2 说错时才开；没必要不要硬造代码 PR |
| 5 | `docs/validation-and-evidence-import` | `docs: update validation notes and import external evidence` | Zehan CHEN | Hongshuo HU | docs | `validation/**`、`testing/**`、`process/canonical-repo/**`、`process/task-board/**` | 如果拿到真实 repo / board / meeting 证据，就放这里；拿不到就只更新 truthful blocker wording |
| 6 | `docs/wr2-report-closeout` | `docs: close out WR2 report package` | Guangjun HU | Zehan CHEN | docs | `wr2-latex/sections/*.tex`、`appendix.tex`、`wr2-review-sync-checklist-2026-04-02.md`、`main.pdf` | 对应 `01-07`，最终整合 PR，应该最后合并 |

## PR 顺序建议

### 第一波，可并行

- `docs/wr2-baseline-scope-alignment`
- `docs/collaboration-history-support-pack`
- `docs/prototype-snapshot-verification`
- `docs/validation-and-evidence-import`

### 第二波，条件触发

- `feat/prototype-accuracy-fixes`

只在 Hongshuo 复核后确认“当前 demo 与 WR2 说法不一致”时才开。  
如果当前 demo 已经足够支撑 WR2，直接跳过，不要为了“看起来像有代码 PR”而硬开。

### 第三波，最后收口

- `docs/wr2-report-closeout`

这个 PR 必须在前面的文档和 supporting evidence 都落稳之后再开。

## 不要做的事


- 不要把一个人的所有修改都塞进一个超大 PR，至少要按上面的责任边界拆开。
- 不要在没有必要的情况下为了凑 PR 数量去制造空的代码改动。

## 最小可执行方案

如果时间很紧，至少保证这 4 个 PR：

1. `docs/wr2-baseline-scope-alignment`
2. `docs/collaboration-history-support-pack`
3. `docs/validation-and-evidence-import`
4. `docs/wr2-report-closeout`

如果当前 demo 确实有会影响 WR2 准确性的行为差异，再加：

5. `feat/prototype-accuracy-fixes`

## 与当前 GSD Phase 1 的对应关系

- `01-05` -> `docs/wr2-baseline-scope-alignment`
- `01-06` -> `docs/collaboration-history-support-pack`
- `01-07` -> `docs/wr2-report-closeout`
- `docs/prototype-snapshot-verification` 和 `feat/prototype-accuracy-fixes` 是对 `01-07` 的前置保障
- `docs/validation-and-evidence-import` 是 `01-06` 的增强版执行入口

