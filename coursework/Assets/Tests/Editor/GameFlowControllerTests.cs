using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class GameFlowControllerTests
    {
        [Test]
        public void Initialize_SetsTitleState()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: true);

            Assert.That(controller.CurrentState, Is.EqualTo(GameFlowState.Title));
            Assert.That(controller.LastResult, Is.Null);
        }

        [Test]
        public void Initialize_ConfiguresBattlePanelToPassThroughWorldClicks()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: true);

            var battlePanel = EditorTestSupport.GetPrivateField<GameObject>(controller, "battlePanel");

            Assert.That(battlePanel, Is.Not.Null);
            Assert.That(battlePanel.GetComponent<UnityEngine.UI.Image>().raycastTarget, Is.False);
        }

        [Test]
        public void Initialize_BuildsThemedTitlePresentation()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: true);

            var titlePanel = EditorTestSupport.GetPrivateField<GameObject>(controller, "titlePanel");

            Assert.That(titlePanel.transform.Find("TitleBanner"), Is.Not.Null);
            Assert.That(titlePanel.transform.Find("TitleContent"), Is.Not.Null);
            Assert.That(titlePanel.transform.Find("TitleContent/StartButton"), Is.Not.Null);
            Assert.That(titlePanel.transform.Find("TitleContent/BrandIcon").GetComponent<UnityEngine.UI.Image>().sprite, Is.Not.Null);
            Assert.That(titlePanel.transform.Find("TitleCorners/TopLeft"), Is.Not.Null);
            Assert.That(EditorTestSupport.GetPrivateField<UnityEngine.UI.Text>(controller, "titleSummaryText"), Is.Not.Null);
        }

        [Test]
        public void StartGame_WithBattleReadyUnits_TransitionsToBattle()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: true);

            controller.StartGame();

            Assert.That(controller.CurrentState, Is.EqualTo(GameFlowState.Battle));
            Assert.That(controller.LastResult, Is.Null);
        }

        [Test]
        public void StartGame_WithoutEnemyUnits_ShowsImmediateResult()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: false);

            controller.StartGame();

            Assert.That(controller.CurrentState, Is.EqualTo(GameFlowState.Result));
            Assert.That(controller.LastResult, Is.Not.Null);
            Assert.That(controller.LastResult.Title, Is.EqualTo("Victory"));
        }

        [Test]
        public void ReturnToMenu_AfterResult_GoesBackToTitle()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: false);
            controller.StartGame();

            controller.ReturnToMenu();

            Assert.That(controller.CurrentState, Is.EqualTo(GameFlowState.Title));
            Assert.That(controller.LastResult, Is.Null);
        }

        [Test]
        public void StartGame_ImmediateResult_BuildsResultBadges()
        {
            using var scope = CreateContext(out var controller, out _, withEnemy: false);

            controller.StartGame();

            var resultPanel = EditorTestSupport.GetPrivateField<GameObject>(controller, "resultPanel");
            Assert.That(resultPanel.transform.Find("ResultCorners/TopLeft"), Is.Not.Null);
            Assert.That(resultPanel.transform.Find("ResultContent/ReplayBadge"), Is.Not.Null);
            Assert.That(resultPanel.transform.Find("ResultContent/OutcomeBadge"), Is.Not.Null);
        }

        private static TestObjectScope CreateContext(out GameFlowController controller, out GameRoot gameRoot, bool withEnemy)
        {
            var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            EditorTestSupport.SetPrivateField(gridManager, "generateOnStart", false);
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));

            if (withEnemy)
            {
                var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));
                EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });
            }
            else
            {
                EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player });
            }

            EditorTestSupport.SetPrivateField(gameRoot, "gridManager", gridManager);
            EditorTestSupport.SetPrivateField(gameRoot, "turnManager", turnManager);
            EditorTestSupport.SetPrivateField(gameRoot, "deckManager", deckManager);
            EditorTestSupport.SetPrivateField(gameRoot, "cardResolver", cardResolver);
            EditorTestSupport.SetPrivateField(gameRoot, "enemyAI", enemyAi);
            EditorTestSupport.SetPrivateField(gameRoot, "battleUI", battleUi);
            EditorTestSupport.SetPrivateField(gameRoot, "disableFrontEndFlow", true);
            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");

            controller = scope.Track(new GameObject("GameFlowController")).AddComponent<GameFlowController>();
            controller.Initialize(gameRoot, battleUi, turnManager);
            return scope;
        }
    }
}
