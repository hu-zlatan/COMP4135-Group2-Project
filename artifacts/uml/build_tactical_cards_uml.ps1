$ErrorActionPreference = 'Stop'
$env:CLI_ANYTHING_DRAWIO_DESKTOP_ROOT = 'F:\unity\Unity-Project\drawio-desktop'
$root = 'F:\unity\Unity-Project\artifacts\uml'
New-Item -ItemType Directory -Force -Path $root | Out-Null

function New-Diagram($path) {
    if (Test-Path $path) { Remove-Item $path -Force }
    $null = cli-anything-drawio --json project new -o $path | Out-String
}

function Add-Shape($project, $type, $label, $x, $y, $w, $h) {
    $json = cli-anything-drawio --json --project $project shape add $type --label $label --x $x --y $y --width $w --height $h | Out-String
    return ((ConvertFrom-Json $json).id)
}

function Connect-Nodes($project, $source, $target, $label = '') {
    if ([string]::IsNullOrWhiteSpace($label)) {
        $null = cli-anything-drawio --json --project $project connect add $source $target | Out-String
    }
    else {
        $null = cli-anything-drawio --json --project $project connect add $source $target --label $label | Out-String
    }
}

function Export-Svg($project, $svg) {
    if (Test-Path $svg) { Remove-Item $svg -Force }
    $null = cli-anything-drawio --project $project export render $svg -f svg --overwrite | Out-String
}

# Gameplay loop
$gameplay = Join-Path $root 'tactical_cards_gameplay_loop.drawio'
New-Diagram $gameplay
$start = Add-Shape $gameplay 'ellipse' 'Start Battle' 60 120 120 60
$turn = Add-Shape $gameplay 'rounded' 'Start Player Turn' 220 120 150 60
$draw = Add-Shape $gameplay 'rectangle' 'Draw 3 Cards' 410 120 130 60
$selectU = Add-Shape $gameplay 'rounded' 'Select Unit' 580 120 120 60
$selectC = Add-Shape $gameplay 'rounded' 'Select Card' 740 120 120 60
$target = Add-Shape $gameplay 'rectangle' 'Highlight Legal Targets' 900 120 180 60
$resolve = Add-Shape $gameplay 'rounded' "Click Tile or Enemy`nResolve Card" 1120 120 170 70
$plays = Add-Shape $gameplay 'diamond' 'Card Plays Left?' 1145 250 130 90
$end = Add-Shape $gameplay 'rounded' 'End Player Turn' 950 320 150 60
$enemy = Add-Shape $gameplay 'rectangle' 'Enemy AI Moves / Attacks' 760 320 170 60
$check = Add-Shape $gameplay 'diamond' 'Victory or Defeat?' 540 320 150 90
$loop = Add-Shape $gameplay 'ellipse' 'Next Player Turn' 320 335 140 60
$finish = Add-Shape $gameplay 'ellipse' 'Battle Ends' 540 470 140 60
Connect-Nodes $gameplay $start $turn
Connect-Nodes $gameplay $turn $draw
Connect-Nodes $gameplay $draw $selectU
Connect-Nodes $gameplay $selectU $selectC
Connect-Nodes $gameplay $selectC $target
Connect-Nodes $gameplay $target $resolve
Connect-Nodes $gameplay $resolve $plays
Connect-Nodes $gameplay $plays $selectC 'Yes'
Connect-Nodes $gameplay $plays $end 'No'
Connect-Nodes $gameplay $end $enemy
Connect-Nodes $gameplay $enemy $check
Connect-Nodes $gameplay $check $loop 'No'
Connect-Nodes $gameplay $loop $turn
Connect-Nodes $gameplay $check $finish 'Yes'
Export-Svg $gameplay (Join-Path $root 'tactical_cards_gameplay_loop.svg')

# Architecture
$arch = Join-Path $root 'tactical_cards_demo_architecture.drawio'
New-Diagram $arch
$gameroot = Add-Shape $arch 'rounded' "GameRoot`nScene bootstrap / wiring" 480 40 190 70
$battleui = Add-Shape $arch 'rounded' "BattleUI`nInput + hand + log" 40 180 180 80
$turnm = Add-Shape $arch 'rectangle' "TurnManager`nTurn flow / enemy summary" 280 180 190 80
$deckm = Add-Shape $arch 'rectangle' "DeckManager`nDraw / hand / discard" 520 180 180 80
$cardres = Add-Shape $arch 'rectangle' "CardResolver`nValidate / resolve cards" 760 180 190 80
$gridm = Add-Shape $arch 'rectangle' "GridManager`nGrid / occupancy / ranges" 240 340 190 80
$tilev = Add-Shape $arch 'rectangle' "TileView`nTile visuals / highlight" 470 340 170 80
$unitc = Add-Shape $arch 'rectangle' "UnitController`nHP / team / position" 690 340 190 80
$enemyai = Add-Shape $arch 'rectangle' "EnemyAI`nNearest target / step" 920 340 170 80
$carddata = Add-Shape $arch 'rectangle' "CardData`nMove / Strike / Guard / Push" 760 500 190 80
Connect-Nodes $arch $gameroot $battleui
Connect-Nodes $arch $gameroot $turnm
Connect-Nodes $arch $gameroot $deckm
Connect-Nodes $arch $gameroot $cardres
Connect-Nodes $arch $gameroot $gridm
Connect-Nodes $arch $turnm $deckm
Connect-Nodes $arch $turnm $enemyai
Connect-Nodes $arch $turnm $gridm
Connect-Nodes $arch $battleui $turnm
Connect-Nodes $arch $battleui $deckm
Connect-Nodes $arch $battleui $cardres
Connect-Nodes $arch $battleui $gridm
Connect-Nodes $arch $cardres $gridm
Connect-Nodes $arch $cardres $unitc
Connect-Nodes $arch $cardres $carddata
Connect-Nodes $arch $gridm $tilev
Connect-Nodes $arch $gridm $unitc
Connect-Nodes $arch $enemyai $unitc
Connect-Nodes $arch $enemyai $gridm
Export-Svg $arch (Join-Path $root 'tactical_cards_demo_architecture.svg')

# Unit state machine
$state = Join-Path $root 'tactical_cards_unit_state_machine.drawio'
New-Diagram $state
$idle = Add-Shape $state 'ellipse' 'Idle' 120 160 110 60
$selected = Add-Shape $state 'rounded' 'Selected' 300 160 130 60
$targeting = Add-Shape $state 'rounded' 'Card Targeting' 500 160 150 60
$acting = Add-Shape $state 'rounded' 'Acting' 730 160 110 60
$guarding = Add-Shape $state 'rounded' 'Guarding' 930 160 130 60
$damaged = Add-Shape $state 'rounded' 'Damaged' 560 300 120 60
$dead = Add-Shape $state 'ellipse' 'Dead' 780 300 100 60
Connect-Nodes $state $idle $selected 'Click unit'
Connect-Nodes $state $selected $targeting 'Choose card'
Connect-Nodes $state $selected $idle 'Clear selection'
Connect-Nodes $state $targeting $acting 'Valid target'
Connect-Nodes $state $targeting $selected 'Cancel / invalid'
Connect-Nodes $state $acting $idle 'Move / Strike / Push resolved'
Connect-Nodes $state $acting $guarding 'Guard resolved'
Connect-Nodes $state $guarding $damaged 'Take hit'
Connect-Nodes $state $idle $damaged 'Take hit'
Connect-Nodes $state $selected $damaged 'Take hit'
Connect-Nodes $state $damaged $idle 'Still alive'
Connect-Nodes $state $damaged $dead 'HP <= 0'
Connect-Nodes $state $guarding $idle 'Turn ends / guard consumed'
Export-Svg $state (Join-Path $root 'tactical_cards_unit_state_machine.svg')

Get-ChildItem $root | Select-Object Name, Length, LastWriteTime
