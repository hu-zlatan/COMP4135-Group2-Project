# Tactical Cards Software Package

- Package date: 2026-04-26
- Archive target: `artifacts/submission/TacticalCards-software-2026-04-26.zip`
- Purpose: provide the software hand-in required by the CW1 specification

## Included content

- `source/README.md`
- `source/coursework/Assets/`
- `source/coursework/Packages/`
- `source/coursework/ProjectSettings/`
- `source/coursework/Assets/ThirdParty/`
- `windows-build/` copied from `F:/unity/build/`

## Excluded Unity directories

The archive intentionally excludes generated or machine-local folders that are not part of the source-code hierarchy:

- `coursework/Library/`
- `coursework/Temp/`
- `coursework/Logs/`
- `coursework/UserSettings/`

## Run options

### Option 1: Run the packaged Windows build

1. Open `windows-build/`.
2. Run `coursework.exe`.

### Option 2: Open the project in Unity

1. Open the `source/coursework/` project in Unity 6000.3.11f1.
2. Load `Assets/Scenes/SampleScene.unity`.
3. Press Play, then use `Start Battle` on the title screen.

## Notes

- The archive includes both source and a runnable build because the specification encourages a package that is executable where feasible.
- The report, website, video, and presentation remain separate deliverables and are not bundled into this software archive.
