# Tactical Cards Coursework Repository

This repository contains the Unity tactical card game demo, the weekly report sources, the final-report source tree, and the supporting project records used for coursework evidence.

## Project snapshot

`Tactical Cards` is a compact Unity tactics demo built around one battle-ready vertical slice:

- title screen, battle flow, and result screen in the main playable scene
- 6x6 tactical board with two player-controlled units
- card-driven actions including `Move`, `Dash`, `Strike`, `Long Strike`, `Guard`, `Push`, and `Recover`
- deterministic enemy response plus reinforcement pressure
- themed `uGUI` battle HUD with prompts, battle log, disabled-card feedback, and world-space health bars

The project is intentionally small in content, but it is structured to support testing, documentation, and final demonstration.

## Repository layout

```text
.
|-- coursework
|   `-- Unity project used for the playable game
|-- artifacts
|   `-- uml
|       `-- diagram sources and exports used by the reports
`-- doc
    `-- src
        |-- course
        |-- final-report-latex
        |-- project-records
        |-- wr1-latex
        |-- wr2-latex
        `-- wr3-latex
```

## Run the game in Unity

1. Open the Unity project in `coursework/`.
2. Load `Assets/Scenes/SampleScene.unity`.
3. Press Play in the Unity Editor.
4. In the title screen, press `Start Battle`.

## Basic play flow

1. Select a player unit on the board.
2. Select a card from the hand panel.
3. Click a valid target tile or enemy unit.
4. Use `Clear` to reset the current selection if needed.
5. Press `End Turn` to let the enemy act.
6. Use the result screen to replay the battle or return to the title screen.

## Reports and evidence

- Course materials and template references: `doc/src/course/`
- WR1 source: `doc/src/wr1-latex/`
- WR2 source: `doc/src/wr2-latex/`
- WR3 source: `doc/src/wr3-latex/`
- Final report source: `doc/src/final-report-latex/`
- Working records and evidence: `doc/src/project-records/`

## Submission-facing links

- Canonical remote: `https://github.com/hu-zlatan/COMP4135-Group2-Project.git`
- Final website URL: `TBC before final hand-in`

## Process notes

- Review and branching workflow notes are documented under `doc/src/project-records/`.
- The root project records index is `doc/src/project-records/README.md`.
- Open-licensed runtime UI assets are documented in `coursework/Assets/ThirdParty/Kenney/NOTICE.md`.
