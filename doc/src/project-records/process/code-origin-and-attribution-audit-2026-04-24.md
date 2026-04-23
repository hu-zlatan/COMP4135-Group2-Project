# Code Origin And Attribution Audit

- Date: 2026-04-24
- Purpose: identify the current code and asset origin boundaries that must be described accurately in the final report and final submission package

## 1. Group-authored runtime code

The following folders represent the main group-authored gameplay and UI implementation in the current workspace:

- `coursework/Assets/Scripts/Common/`
- `coursework/Assets/Scripts/Core/`
- `coursework/Assets/Scripts/Cards/`
- `coursework/Assets/Scripts/Grid/`
- `coursework/Assets/Scripts/Units/`
- `coursework/Assets/Scripts/UI/`
- `coursework/Assets/Scripts/Presentation/`

The associated test code under `coursework/Assets/Tests/` is also group-authored coursework code.

## 2. Third-party runtime assets

The current imported third-party runtime art evidence in the workspace is:

- `coursework/Assets/ThirdParty/Kenney/NOTICE.md`
- `coursework/Assets/Resources/UITheme/`

These assets were introduced during the visual integration pass and are already separated from the team's own scripts.

## 3. External engine and package dependencies

The Unity package manifest shows the project depends on standard Unity packages plus the MCP integration package used during tooling work:

- `coursework/Packages/manifest.json`
- `coursework/Packages/packages-lock.json`

These package dependencies should be described as environment dependencies, not as team-authored coursework deliverables.

## 4. Current compliance strengths

- Third-party UI art has a dedicated notice file.
- Runtime code, tests, records, and report sources are separated cleanly by folder.
- The final report can explicitly distinguish:
  - gameplay code written by the team;
  - test code written by the team;
  - third-party art assets;
  - Unity engine and package dependencies.

## 5. Current compliance gap

The CW1 specification states that code written by the team should be clearly marked as such and that adapted code should identify the original source and the nature of the adaptation. The current runtime `.cs` files do not yet carry per-file attribution headers.

## 6. Recommended resolution path

Use one of the following approaches before final submission:

1. Add short header comments to the main runtime and test files describing authorship and whether the file is original coursework code.
2. If the team prefers not to add headers to every file, create a dedicated code-origin appendix table that explicitly marks:
   - group-authored files;
   - third-party assets;
   - Unity package dependencies;
   - any adapted code, if such adaptation exists.

The second option is lower-risk for late-stage cleanup, but it must still satisfy the wording of the CW1 brief. The team should choose one explicit method rather than relying on assumption.

## 7. Safe claim boundary for the final report

The final report can already state truthfully that:

- the gameplay and test implementation under `Assets/Scripts/` and `Assets/Tests/` is team-authored coursework code unless otherwise stated;
- the themed UI art imported from Kenney is third-party material under the recorded notice;
- Unity packages are external dependencies;
- code attribution still needs a final explicit presentation layer in the submission package.
