# Canonical Repo Evidence

- Record ID: PROCESS-REPO-2026-04-02
- Date: 2026-04-02
- Status: blocked externally after discovery
- Expected owner: repo maintainer
- Expected source system: canonical team GitHub or GitLab repository
- Import folder: `process/canonical-repo/`

## What this workspace can confirm

- Current local repository branch: `dev`
- Local review branch exists: `review/coursework`
- Local release branch exists: `release/game`
- Local backup branch exists: `backup/raw-pre-cleanup-2026-04-01`
- `git remote -v` returned no configured remotes in this workspace on 2026-04-02
- `gh repo list` for the authenticated GitHub account returned accessible repositories, but none matched the Unity coursework repo name from this workspace

## Discovery artifacts captured in-repo

- `process/discovery/local-remote-check-2026-04-02.txt`
- `process/discovery/github-visible-repos-2026-04-02.json`

These files strengthen the blocker note by proving what was checked from this workspace before escalation to the team.

## Missing teacher-facing artifacts

- Screenshot or export showing the canonical remote repository identity
- Screenshot or export showing branch usage on the canonical remote
- PR / review / merge history if the report or final package claims that workflow
- Canonical remote commit history that can be inspected outside this local workspace

## Why this is blocked here

This workspace contains the cleaned local repo state and branch model, but it does not currently contain a configured remote or imported screenshots from the actual team remote. Because of that, local evidence can only prove the intended branch structure, not the full remote collaboration history.

## Next collection action

1. Identify the real team remote URL or hosting system.
2. Capture the repository identity, branch list, and any real PR / review history the team wants to claim.
3. Save those captures into `process/canonical-repo/` using dated filenames.
4. Update this note and `wr2-asset-register.md` from blocked to ready if the evidence is imported.
