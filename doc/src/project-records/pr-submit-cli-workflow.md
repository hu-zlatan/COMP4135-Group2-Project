# PR Submit CLI Workflow

This workflow is for teams that do not want to create PRs manually in the GitHub web UI.

## One-time setup

1. Copy `scripts/pr-profiles.sample.json` to `scripts/pr-profiles.json`.
2. Fill each member's `gh_user`, `git_name`, and `git_email`.
3. Each member logs in once with their own GitHub account:
   - `gh auth login -h github.com --web`

Notes:

- `scripts/pr-profiles.json` is gitignored and must not be committed.
- SSH key alone does not change `gh` API identity. PR author is whichever account is logged in via `gh auth`.

## One-command submit

Run from repository root on your feature/docs branch:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/pr-submit.ps1 `
  -Profile guangjun `
  -CommitMessage "docs: align WR2 baseline and scope records" `
  -PrTitle "docs: align WR2 baseline and scope records" `
  -BaseBranch develop `
  -PrType docs
```

## Review policy enforcement

- Docs PR:
  - allow maintainer self-review flow
- Code PR:
  - must provide at least two reviewers:

```powershell
powershell -ExecutionPolicy Bypass -File scripts/pr-submit.ps1 `
  -Profile hongshuo `
  -CommitMessage "feat: improve card target feedback" `
  -PrTitle "feat: improve card target feedback" `
  -BaseBranch develop `
  -PrType code `
  -Reviewers reviewer1,reviewer2
```

If `-PrType auto` is used, the script treats PRs as:

- `docs` only when all changed files are under `doc/` or `artifacts/uml/`
- `code` otherwise
