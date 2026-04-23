using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class BattleUITests
    {
        [Test]
        public void SelectCard_WithoutSelectedUnit_PreservesCardAndExplainsNextStep()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);

            battleUi.SelectCard(strikeCard);

            Assert.That(battleUi.SelectedCard, Is.EqualTo(strikeCard));
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Strike selected. Next: select a player unit."));
        }

        [Test]
        public void Update_WithoutCamera_ReturnsEarly()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);

            EditorTestSupport.InvokePrivateMethod(battleUi, "Update");

            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Select a player unit."));
        }

        [Test]
        public void HandlePlayerUnitSelected_WithSelectedCard_PromptsTargetSelectionAndLogsNextStep()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            battleUi.SelectCard(strikeCard);

            EditorTestSupport.InvokePrivateMethod(battleUi, "HandlePlayerUnitSelected", player);
            var actionLog = GetActionLog(battleUi);

            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Hero selected. Strike ready. Next: choose an enemy target."));
            Assert.That(actionLog[^1], Is.EqualTo("Hero selected at 1,1. Next: choose an enemy target."));
        }

        [Test]
        public void ClearSelectedCard_WithSelectedUnit_ReturnsChooseCardPrompt()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(strikeCard);

            battleUi.ClearSelectedCard();

            Assert.That(battleUi.SelectedCard, Is.Null);
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Hero selected. Next: choose a card."));
        }

        [Test]
        public void TryPlayCardOnTile_InvalidTarget_ReturnsFalseAndKeepsCardSelected()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out var gridManager);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 1);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(moveCard);

            var resolved = battleUi.TryPlayCardOnTile(player, gridManager.GetTile(new Vector2Int(4, 4)));

            Assert.That(resolved, Is.False);
            Assert.That(battleUi.SelectedCard, Is.EqualTo(moveCard));
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Invalid tile target."));
        }

        [Test]
        public void TryPlayCardOnTile_WithoutRemainingPlays_ShowsQuotaMessage()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out var player, out _, out var gridManager);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 1);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(moveCard);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.RemainingCardPlays), 0);

            var resolved = battleUi.TryPlayCardOnTile(player, gridManager.GetTile(new Vector2Int(1, 2)));

            Assert.That(resolved, Is.False);
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("No card plays remaining."));
        }

        [Test]
        public void TryPlayCardOnUnit_FriendlyTarget_ShowsSpecificInstruction()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            var ally = EditorTestSupport.CreateUnit(scope, "Ally", TeamType.Player, new Vector2Int(1, 2));
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(strikeCard);

            var resolved = battleUi.TryPlayCardOnUnit(player, ally);

            Assert.That(resolved, Is.False);
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Select an enemy unit."));
        }

        [Test]
        public void TryPlayCardOnUnit_WithoutRemainingPlays_ShowsQuotaMessage()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out var player, out var enemy, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(strikeCard);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.RemainingCardPlays), 0);

            var resolved = battleUi.TryPlayCardOnUnit(player, enemy);

            Assert.That(resolved, Is.False);
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("No card plays remaining."));
        }

        [Test]
        public void BuildSnapshot_WithSelectionAndNewCards_ReportsHudState()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var dashCard = EditorTestSupport.CreateCard(scope, "dash", "Dash", CardType.Dash, TargetType.Tile, moveDistance: 3);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(dashCard);

            var snapshot = battleUi.BuildSnapshot();

            Assert.That(snapshot.SelectedUnit.DisplayName, Is.EqualTo("Hero"));
            Assert.That(snapshot.SelectedCard.Title, Is.EqualTo("Dash"));
            Assert.That(snapshot.StatusMessage, Does.Contain("Dash ready"));
        }

        [Test]
        public void BuildSnapshot_WithoutEnergy_DisablesHandAndPromptsEndTurn()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out _, out _, out _);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.RemainingCardPlays), 0);

            var snapshot = battleUi.BuildSnapshot();

            Assert.That(snapshot.CanPlayCards, Is.False);
            Assert.That(snapshot.PromptTitle, Is.EqualTo("No Energy Left"));
            Assert.That(snapshot.PromptDetail, Does.Contain("End the turn"));
            Assert.That(snapshot.Hand[0].IsPlayable, Is.False);
            Assert.That(snapshot.Hand[0].DisabledReason, Is.EqualTo("No Energy"));
        }

        [Test]
        public void AttachRuntimeUi_BackdropDoesNotBlockWorldClicks()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var parent = scope.Track(new GameObject("HudRoot")).transform;

            battleUi.AttachRuntimeUi(parent);

            var backdrop = parent.Find("BattleHudPanel/Backdrop");
            Assert.That(backdrop, Is.Not.Null);
            Assert.That(backdrop.GetComponent<UnityEngine.UI.Image>().raycastTarget, Is.False);
            Assert.That(parent.Find("BattleHudPanel/TopBar").GetComponent<UnityEngine.UI.Image>().raycastTarget, Is.False);
            Assert.That(parent.Find("BattleHudPanel/LeftPanel").GetComponent<UnityEngine.UI.Image>().raycastTarget, Is.False);
            Assert.That(parent.Find("BattleHudPanel/BottomPanel").GetComponent<UnityEngine.UI.Image>().raycastTarget, Is.False);
        }

        [Test]
        public void AttachRuntimeUi_AppliesThemeSpritesToPanelsAndCards()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var parent = scope.Track(new GameObject("HudRoot")).transform;

            battleUi.AttachRuntimeUi(parent);
            battleUi.SetVisible(true);

            Assert.That(parent.Find("BattleHudPanel/TopBar"), Is.Not.Null);
            Assert.That(parent.Find("BattleHudPanel/LeftPanel"), Is.Not.Null);
            Assert.That(parent.Find("BattleHudPanel/BottomPanel"), Is.Not.Null);
            Assert.That(UiThemeResources.GetSprite(UiThemeResources.Paths.PanelAccent), Is.Not.Null);
            Assert.That(UiThemeResources.GetCardIcon(CardType.Strike), Is.Not.Null);
        }

        [Test]
        public void AttachRuntimeUi_BuildsPromptEnergyAndWorldHudLayers()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var parent = scope.Track(new GameObject("HudRoot")).transform;

            battleUi.AttachRuntimeUi(parent);
            battleUi.SetVisible(true);

            Assert.That(parent.Find("BattleHudPanel/TopBar/EnergyLabel"), Is.Not.Null);
            Assert.That(parent.Find("BattleHudPanel/TopBar/PromptTitle"), Is.Not.Null);
            Assert.That(parent.Find("BattleHudPanel/WorldHudLayer"), Is.Not.Null);
        }

        [Test]
        public void AttachRuntimeUi_StacksTopBarRowsAndKeepsHandCompact()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var parent = scope.Track(new GameObject("HudRoot")).transform;

            battleUi.AttachRuntimeUi(parent);
            battleUi.SetVisible(true);
            battleUi.Refresh();

            var turnLabel = parent.Find("BattleHudPanel/TopBar/TurnLabel").GetComponent<RectTransform>();
            var selectionLabel = parent.Find("BattleHudPanel/TopBar/SelectionLabel").GetComponent<RectTransform>();
            var promptTitle = parent.Find("BattleHudPanel/TopBar/PromptTitle").GetComponent<RectTransform>();
            var promptDetail = parent.Find("BattleHudPanel/TopBar/PromptDetail").GetComponent<RectTransform>();
            var bottomPanel = parent.Find("BattleHudPanel/BottomPanel").GetComponent<RectTransform>();
            var firstCard = parent.Find("BattleHudPanel/BottomPanel/CardArea/CardButton").GetComponent<RectTransform>();

            Assert.That(selectionLabel.offsetMax.y, Is.LessThanOrEqualTo(turnLabel.offsetMin.y));
            Assert.That(promptTitle.offsetMax.y, Is.LessThanOrEqualTo(selectionLabel.offsetMin.y));
            Assert.That(promptDetail.offsetMax.y, Is.LessThanOrEqualTo(promptTitle.offsetMin.y));
            Assert.That(bottomPanel.offsetMax.y, Is.LessThanOrEqualTo(136f));
            Assert.That(firstCard.sizeDelta.y, Is.LessThanOrEqualTo(78f));
        }

        [Test]
        public void SetVisible_AndRefresh_RendersWorldHealthBarsForUnits()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var parent = scope.Track(new GameObject("HudRoot")).transform;

            battleUi.AttachRuntimeUi(parent);
            battleUi.SetVisible(true);
            battleUi.Refresh();

            var worldHudLayer = parent.Find("BattleHudPanel/WorldHudLayer");
            Assert.That(worldHudLayer, Is.Not.Null);
            Assert.That(worldHudLayer.childCount, Is.GreaterThanOrEqualTo(2));
        }

        [Test]
        public void SelectCard_HealOnSelectedUnit_AutoResolvesAndRestoresHealth()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var healCard = EditorTestSupport.CreateCard(scope, "heal", "Recover", CardType.Heal, TargetType.Self, range: 0, power: 2, moveDistance: 0);
            EditorTestSupport.SetProperty(player, nameof(UnitController.CurrentHp), 2);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);

            battleUi.SelectCard(healCard);

            Assert.That(player.CurrentHp, Is.EqualTo(4));
            Assert.That(battleUi.SelectedCard, Is.Null);
            Assert.That(GetActionLog(battleUi)[^1], Does.Contain("used Recover"));
        }

        [Test]
        public void TryPlayCardOnUnit_ValidStrike_ConsumesPlayAndUpdatesActionLog()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out var player, out var enemy, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(strikeCard);

            var resolved = battleUi.TryPlayCardOnUnit(player, enemy);
            var actionLog = GetActionLog(battleUi);

            Assert.That(resolved, Is.True);
            Assert.That(enemy.CurrentHp, Is.EqualTo(3));
            Assert.That(turnManager.RemainingCardPlays, Is.EqualTo(1));
            Assert.That(actionLog[^1], Does.Contain("used Strike on Enemy"));
        }

        [Test]
        public void TryPlayCardOnUnit_WhenFinalEnemyDies_AppliesBattleOutcome()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out var enemy, out _);
            var strikeCard = EditorTestSupport.CreateCard(scope, "finisher", "Finisher", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            EditorTestSupport.SetProperty(enemy, nameof(UnitController.CurrentHp), 2);
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            battleUi.SelectCard(strikeCard);

            var resolved = battleUi.TryPlayCardOnUnit(player, enemy);
            var actionLog = GetActionLog(battleUi);

            Assert.That(resolved, Is.True);
            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Victory! All enemy units defeated."));
            Assert.That(actionLog[^1], Is.EqualTo("Victory! All enemy units defeated."));
        }

        [Test]
        public void SelectCard_AfterBattleEnded_ShowsBattleOutcome()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var turnManager = EditorTestSupport.CreateTurnManager(scope, deckManager, enemyAi, gridManager);
            var player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, player);
            turnManager.StartBattle();

            var battleUi = EditorTestSupport.CreateBattleUi(scope, turnManager, deckManager, cardResolver, gridManager);
            var card = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile);
            battleUi.SelectCard(card);

            Assert.That(GetStatusMessage(battleUi), Is.EqualTo("Victory! All enemy units defeated."));
            Assert.That(battleUi.SelectedCard, Is.Null);
        }

        [Test]
        public void GetInvalidUnitTargetMessage_HandlesMissingCardFriendlyDeadAndRangeCases()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out var enemy, out _);
            var ally = EditorTestSupport.CreateUnit(scope, "Ally", TeamType.Player, new Vector2Int(1, 2));
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);

            var noCard = EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", enemy);
            Assert.That(noCard, Is.EqualTo("No card selected."));

            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);
            battleUi.SelectCard(strikeCard);

            var friendly = EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", ally);
            Assert.That(friendly, Is.EqualTo("Select an enemy unit."));

            enemy.TakeDamage(enemy.CurrentHp);
            var defeated = EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", enemy);
            Assert.That(defeated, Is.EqualTo("Enemy is already defeated."));

            var otherEnemy = EditorTestSupport.CreateUnit(scope, "FarEnemy", TeamType.Enemy, new Vector2Int(5, 5));
            var outOfRange = EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", otherEnemy);
            Assert.That(outOfRange, Is.EqualTo("Target out of range. Strike range is 1."));
        }

        [Test]
        public void HelperBranches_ReturnExpectedFallbackValues()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out var player, out var enemy, out var gridManager);
            var pushCard = EditorTestSupport.CreateCard(scope, "push", "Push", CardType.Push, TargetType.EnemyUnit, range: 1, power: 1);
            var guardCard = EditorTestSupport.CreateCard(scope, "guard", "Guard", CardType.Guard, TargetType.Self, range: 0, power: 0, moveDistance: 0);
            var customCard = EditorTestSupport.CreateCard(scope, "custom", "Custom", (CardType)99, (TargetType)99, description: "Custom text");

            Assert.That(EditorTestSupport.InvokePrivateMethod<bool>(battleUi, "CanPlayTileCard", player, gridManager.GetTile(new Vector2Int(1, 2))), Is.False);

            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            EditorTestSupport.SetProperty(battleUi, nameof(BattleUI.SelectedCard), guardCard);
            Assert.That(EditorTestSupport.InvokePrivateMethod<bool>(battleUi, "CanPlayTileCard", player, gridManager.GetTile(player.GridPosition)), Is.True);

            battleUi.SelectCard(pushCard);
            var farEnemy = EditorTestSupport.CreateUnit(scope, "FarEnemy", TeamType.Enemy, new Vector2Int(5, 5));
            Assert.That(EditorTestSupport.InvokePrivateMethod<bool>(battleUi, "CanPlayUnitCard", player, farEnemy), Is.False);

            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", null);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", enemy), Is.EqualTo("Select a player unit first."));
            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", (object)null), Is.EqualTo("Invalid unit target."));

            battleUi.SelectCard(pushCard);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetInvalidUnitTargetMessage", farEnemy), Is.EqualTo("Target out of range. Push range is 1."));

            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardDescription", pushCard), Is.EqualTo("Deal 1 damage and push 1."));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardDescription", customCard), Is.EqualTo("Custom text"));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCompactCardDescription", pushCard), Is.EqualTo("Hit and push 1"));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCompactCardDescription", customCard), Is.EqualTo("Custom text"));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardStats", pushCard), Is.EqualTo("Range 1  Power 1"));

            Assert.That(EditorTestSupport.InvokePrivateMethod<Color>(battleUi, "GetCardTint", guardCard, false).b, Is.GreaterThan(0.9f));
            Assert.That(EditorTestSupport.InvokePrivateMethod<Color>(battleUi, "GetCardTint", customCard, false).r, Is.EqualTo(0.88f).Within(0.001f));

            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetNextStepMessage"), Is.EqualTo("Next: choose an enemy target."));
            EditorTestSupport.SetProperty(battleUi, nameof(BattleUI.SelectedCard), guardCard);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetNextStepMessage"), Is.EqualTo("Next: choose your unit tile."));
            battleUi.SelectCard(null);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetNextStepMessage"), Is.EqualTo("Next: choose a card."));

            EditorTestSupport.SetPrivateField(battleUi, "turnManager", null);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetEndTurnStatusMessage"), Is.EqualTo("Enemy phase: Enemy turn resolved. New player turn started."));
            EditorTestSupport.SetPrivateField(battleUi, "turnManager", turnManager);

            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetIdleStatusMessage"), Is.EqualTo("Hero selected. Next: choose a card."));
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.BattleEnded), true);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.BattleOutcomeSummary), "Victory! All enemy units defeated.");
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetSelectionStatusMessage"), Is.EqualTo("Victory! All enemy units defeated."));
        }

        [Test]
        public void GetTurnLabel_ReportsNoBattleEnemyTurnAndBattleOutcomeStates()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out _, out _, out _);

            EditorTestSupport.SetPrivateField(battleUi, "turnManager", null);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetTurnLabel"), Is.EqualTo("No Battle"));

            EditorTestSupport.SetPrivateField(battleUi, "turnManager", turnManager);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.IsPlayerTurn), false);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetTurnLabel"), Is.EqualTo("Enemy Turn"));

            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.BattleEnded), true);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.WinningTeam), TeamType.Player);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetTurnLabel"), Is.EqualTo("Victory"));

            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.WinningTeam), TeamType.Enemy);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetTurnLabel"), Is.EqualTo("Defeat"));
        }

        [Test]
        public void GetEndTurnStatusMessage_PrefixesEnemyPhaseSummaryAndFallsBackToOutcome()
        {
            using var scope = CreateBattleContext(out var battleUi, out var turnManager, out _, out _, out _);

            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.LastEnemyActionSummary), "Enemy moved to 1,2.");
            Assert.That(
                EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetEndTurnStatusMessage"),
                Is.EqualTo("Enemy phase: Enemy moved to 1,2."));

            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.BattleEnded), true);
            EditorTestSupport.SetProperty(turnManager, nameof(TurnManager.BattleOutcomeSummary), "Victory! All enemy units defeated.");
            Assert.That(
                EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetEndTurnStatusMessage"),
                Is.EqualTo("Victory! All enemy units defeated."));
        }

        [Test]
        public void GetSelectionStatusMessage_CoversIdleAndTileTargetPrompts()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out var player, out _, out _);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 2);

            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetSelectionStatusMessage"), Is.EqualTo("Select a player unit."));

            battleUi.SelectCard(moveCard);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetSelectionStatusMessage"), Is.EqualTo("Move selected. Next: select a player unit."));

            EditorTestSupport.SetPrivateField(battleUi, "selectedUnit", player);
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetSelectionStatusMessage"), Is.EqualTo("Hero selected. Move ready. Next: choose a tile target."));
        }

        [Test]
        public void AddLog_TrimsHistoryToLatestTwelveEntries()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);

            for (var i = 1; i <= 14; i++)
            {
                EditorTestSupport.InvokePrivateMethod(battleUi, "AddLog", $"entry-{i}");
            }

            var actionLog = GetActionLog(battleUi);
            Assert.That(actionLog, Has.Count.EqualTo(12));
            Assert.That(actionLog[0], Is.EqualTo("entry-3"));
            Assert.That(actionLog[^1], Is.EqualTo("entry-14"));

            EditorTestSupport.InvokePrivateMethod(battleUi, "AddLog", " ");
            Assert.That(GetActionLog(battleUi)[^1], Is.EqualTo("entry-14"));
        }

        [Test]
        public void CardPresentationHelpers_ReturnDescriptionsStatsAndTint()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 2);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 3);
            var guardCard = EditorTestSupport.CreateCard(scope, "guard", "Guard", CardType.Guard, TargetType.Self, range: 0, power: 0, moveDistance: 0);

            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardDescription", moveCard), Is.EqualTo("Move up to 2 tiles."));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardDescription", strikeCard), Is.EqualTo("Deal 3 damage."));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCompactCardDescription", guardCard), Is.EqualTo("Block next hit"));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardStats", moveCard), Is.EqualTo("Move 2"));
            Assert.That(EditorTestSupport.InvokePrivateMethod<string>(battleUi, "GetCardStats", guardCard), Is.EqualTo("Self"));

            var tint = EditorTestSupport.InvokePrivateMethod<Color>(battleUi, "GetCardTint", strikeCard, true);
            Assert.That(tint.r, Is.GreaterThan(0.95f));
            Assert.That(tint.a, Is.EqualTo(1f));
        }

        [Test]
        public void LayoutHelpers_ReturnUsableValues()
        {
            using var scope = CreateBattleContext(out var battleUi, out _, out _, out _, out _);
            Assert.That(EditorTestSupport.InvokePrivateMethod<Rect>(battleUi, "GetTopBarRect").width, Is.GreaterThanOrEqualTo(300f));
            Assert.That(EditorTestSupport.InvokePrivateMethod<Rect>(battleUi, "GetLogPanelRect").height, Is.GreaterThanOrEqualTo(180f));
            Assert.That(EditorTestSupport.InvokePrivateMethod<Rect>(battleUi, "GetHandPanelRect").height, Is.GreaterThan(0f));
        }

        private static string GetStatusMessage(BattleUI battleUi)
        {
            return EditorTestSupport.GetPrivateField<string>(battleUi, "statusMessage");
        }

        private static List<string> GetActionLog(BattleUI battleUi)
        {
            return EditorTestSupport.GetPrivateField<List<string>>(battleUi, "actionLog");
        }

        private static TestObjectScope CreateBattleContext(
            out BattleUI battleUi,
            out TurnManager turnManager,
            out UnitController player,
            out UnitController enemy,
            out GridManager gridManager)
        {
            var scope = new TestObjectScope();
            gridManager = EditorTestSupport.CreateGridManager(scope);
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            turnManager = EditorTestSupport.CreateTurnManager(scope, deckManager, enemyAi, gridManager);
            player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1), maxHp: 5, currentHp: 5);
            enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1), maxHp: 5, currentHp: 5);

            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, player, enemy);
            turnManager.StartBattle();
            battleUi = EditorTestSupport.CreateBattleUi(scope, turnManager, deckManager, cardResolver, gridManager);
            return scope;
        }
    }
}
