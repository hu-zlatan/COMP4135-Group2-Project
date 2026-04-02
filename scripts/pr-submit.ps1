param(
  [Parameter(Mandatory = $true)]
  [string]$Profile,

  [string]$BaseBranch = "develop",
  [string]$CommitMessage,
  [string]$PrTitle,
  [string]$PrBody,

  [ValidateSet("auto", "docs", "code")]
  [string]$PrType = "auto",

  [string[]]$Reviewers = @(),

  [switch]$Ready
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Require-Command {
  param([string]$Name)
  if (-not (Get-Command $Name -ErrorAction SilentlyContinue)) {
    throw "Required command not found: $Name"
  }
}

function Get-CurrentBranch {
  return (git branch --show-current).Trim()
}

function Get-ProfileValue {
  param(
    [pscustomobject]$Profiles,
    [string]$ProfileName
  )
  foreach ($prop in $Profiles.PSObject.Properties) {
    if ($prop.Name -eq $ProfileName) {
      return $prop.Value
    }
  }
  return $null
}

function Get-CurrentGhUser {
  try {
    $user = (gh api user --jq .login 2>$null).Trim()
    if ([string]::IsNullOrWhiteSpace($user)) {
      return $null
    }
    return $user
  } catch {
    return $null
  }
}

function Classify-PrType {
  param(
    [string[]]$ChangedFiles
  )

  if ($ChangedFiles.Count -eq 0) {
    throw "No changed files detected for this branch."
  }

  foreach ($path in $ChangedFiles) {
    $isDocsLike = $path.StartsWith("doc/") -or $path.StartsWith("artifacts/uml/")
    if (-not $isDocsLike) {
      return "code"
    }
  }

  return "docs"
}

function Ensure-CleanWorkingTree {
  $status = git status --short
  if (-not [string]::IsNullOrWhiteSpace($status)) {
    throw "Working tree is not clean. Commit or stash changes before running this script."
  }
}

Require-Command git
Require-Command gh

if (-not (Test-Path ".git")) {
  throw "Run this script from a git repository root."
}

$profilesPath = Join-Path (Get-Location) "scripts/pr-profiles.json"
if (-not (Test-Path $profilesPath)) {
  throw "Missing scripts/pr-profiles.json. Copy scripts/pr-profiles.sample.json to scripts/pr-profiles.json and fill your team mapping first."
}

$profiles = Get-Content $profilesPath -Encoding UTF8 | ConvertFrom-Json
$profileConfig = Get-ProfileValue -Profiles $profiles -ProfileName $Profile
if ($null -eq $profileConfig) {
  throw "Profile '$Profile' not found in scripts/pr-profiles.json."
}

$branch = Get-CurrentBranch
if ($branch -eq $BaseBranch -or $branch -eq "main" -or $branch -eq "dev") {
  throw "Refusing to submit PR from protected/default branch '$branch'. Switch to your feature/docs branch first."
}

if (-not [string]::IsNullOrWhiteSpace($profileConfig.git_name)) {
  git config user.name $profileConfig.git_name
}
if (-not [string]::IsNullOrWhiteSpace($profileConfig.git_email)) {
  git config user.email $profileConfig.git_email
}

if (-not [string]::IsNullOrWhiteSpace($profileConfig.gh_user)) {
  $currentGhUser = Get-CurrentGhUser
  if ($currentGhUser -ne $profileConfig.gh_user) {
    try {
      gh auth switch -u $profileConfig.gh_user | Out-Null
    } catch {
      throw "Current gh account '$currentGhUser' does not match expected '$($profileConfig.gh_user)'. Run 'gh auth login -h github.com --web' with that account first."
    }
  }
}

if ([string]::IsNullOrWhiteSpace($CommitMessage)) {
  $CommitMessage = Read-Host "Commit message"
}
if ([string]::IsNullOrWhiteSpace($PrTitle)) {
  $PrTitle = Read-Host "PR title"
}

git add -A

$pending = git diff --cached --name-only
if ([string]::IsNullOrWhiteSpace($pending)) {
  throw "No staged changes after git add -A."
}

git commit -m $CommitMessage
git push -u origin $branch

$changed = @(git diff --name-only "origin/$BaseBranch...HEAD")
if ($PrType -eq "auto") {
  $PrType = Classify-PrType -ChangedFiles $changed
}

if ($PrType -eq "code" -and $Reviewers.Count -lt 2) {
  throw "Code PR requires at least 2 reviewers. Pass them with -Reviewers user1,user2."
}

if ([string]::IsNullOrWhiteSpace($PrBody)) {
  $PrBody = @"
## Summary
- submitted by profile: $Profile
- pr type: $PrType
- branch: $branch

## Review policy
- docs PR: maintainer self-review allowed
- code PR: requires at least 2 reviewers
"@
}

$existingPrJson = gh pr list --state open --head $branch --base $BaseBranch --json number,url
$existingPr = $existingPrJson | ConvertFrom-Json
if ($existingPr.Count -gt 0) {
  Write-Host "Open PR already exists: $($existingPr[0].url)"
  exit 0
}

$createArgs = @(
  "pr", "create",
  "--base", $BaseBranch,
  "--head", $branch,
  "--title", $PrTitle,
  "--body", $PrBody
)
if (-not $Ready) {
  $createArgs += "--draft"
}

$prUrl = gh @createArgs
Write-Host $prUrl

if ($Reviewers.Count -gt 0) {
  $prNumber = ($prUrl.Trim().Split("/")[-1])
  gh pr edit $prNumber --add-reviewer ($Reviewers -join ",") | Out-Null
}
