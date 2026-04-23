using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class GameRootTests
    {
        [Test]
        public void Awake_FindsDependenciesWhenSerializedFieldsAreEmpty()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var turnManager = EditorTestSupport.CreateTurnManager(scope);
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");

            Assert.That(EditorTestSupport.GetPrivateField<GridManager>(gameRoot, "gridManager"), Is.EqualTo(gridManager));
            Assert.That(EditorTestSupport.GetPrivateField<TurnManager>(gameRoot, "turnManager"), Is.EqualTo(turnManager));
            Assert.That(EditorTestSupport.GetPrivateField<DeckManager>(gameRoot, "deckManager"), Is.EqualTo(deckManager));
            Assert.That(EditorTestSupport.GetPrivateField<CardResolver>(gameRoot, "cardResolver"), Is.EqualTo(cardResolver));
            Assert.That(EditorTestSupport.GetPrivateField<EnemyAI>(gameRoot, "enemyAI"), Is.EqualTo(enemyAi));
            Assert.That(EditorTestSupport.GetPrivateField<BattleUI>(gameRoot, "battleUI"), Is.EqualTo(battleUi));
        }

        [Test]
        public void Start_WithFrontEndFlow_ShowsTitleAndDefersBattleStart()
        {
            using var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            EditorTestSupport.SetPrivateField(gridManager, "generateOnStart", false);
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));

            player.transform.position = new Vector3(1f, 0f, 1f);
            enemy.transform.position = new Vector3(2f, 0f, 1f);

            EditorTestSupport.SetPrivateField(gameRoot, "gridManager", gridManager);
            EditorTestSupport.SetPrivateField(gameRoot, "turnManager", turnManager);
            EditorTestSupport.SetPrivateField(gameRoot, "deckManager", deckManager);
            EditorTestSupport.SetPrivateField(gameRoot, "cardResolver", cardResolver);
            EditorTestSupport.SetPrivateField(gameRoot, "enemyAI", enemyAi);
            EditorTestSupport.SetPrivateField(gameRoot, "battleUI", battleUi);
            EditorTestSupport.SetPrivateField(gameRoot, "disableFrontEndFlow", false);
            EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");
            EditorTestSupport.InvokePrivateMethod(gameRoot, "Start");
            scope.Track(EditorTestSupport.GetPrivateField<GameFlowController>(gameRoot, "gameFlowController").gameObject);

            Assert.That(turnManager.IsPlayerTurn, Is.False);
            Assert.That(deckManager.Hand.Count, Is.EqualTo(0));
            Assert.That(EditorTestSupport.GetPrivateField<GameFlowController>(gameRoot, "gameFlowController"), Is.Not.Null);
        }

        [Test]
        public void Start_WithDisableFrontEndFlow_StillBuildsRuntimeHudAndStartsBattle()
        {
            using var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            EditorTestSupport.SetPrivateField(gridManager, "generateOnStart", false);
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));

            EditorTestSupport.SetPrivateField(gameRoot, "gridManager", gridManager);
            EditorTestSupport.SetPrivateField(gameRoot, "turnManager", turnManager);
            EditorTestSupport.SetPrivateField(gameRoot, "deckManager", deckManager);
            EditorTestSupport.SetPrivateField(gameRoot, "cardResolver", cardResolver);
            EditorTestSupport.SetPrivateField(gameRoot, "enemyAI", enemyAi);
            EditorTestSupport.SetPrivateField(gameRoot, "battleUI", battleUi);
            EditorTestSupport.SetPrivateField(gameRoot, "disableFrontEndFlow", true);
            EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");
            EditorTestSupport.InvokePrivateMethod(gameRoot, "Start");
            scope.Track(EditorTestSupport.GetPrivateField<GameFlowController>(gameRoot, "gameFlowController").gameObject);

            Assert.That(turnManager.IsPlayerTurn, Is.True);
            Assert.That(deckManager.Hand.Count, Is.EqualTo(3));
            Assert.That(EditorTestSupport.GetPrivateField<GameFlowController>(gameRoot, "gameFlowController").CurrentState, Is.EqualTo(GameFlowState.Battle));
        }

        [Test]
        public void StartBattleSession_PreparesManagersPlacesUnitsAndStartsBattle()
        {
            using var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            EditorTestSupport.SetPrivateField(gridManager, "generateOnStart", false);
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));

            EditorTestSupport.SetPrivateField(gameRoot, "gridManager", gridManager);
            EditorTestSupport.SetPrivateField(gameRoot, "turnManager", turnManager);
            EditorTestSupport.SetPrivateField(gameRoot, "deckManager", deckManager);
            EditorTestSupport.SetPrivateField(gameRoot, "cardResolver", cardResolver);
            EditorTestSupport.SetPrivateField(gameRoot, "enemyAI", enemyAi);
            EditorTestSupport.SetPrivateField(gameRoot, "battleUI", battleUi);
            EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");
            gameRoot.StartBattleSession();

            Assert.That(turnManager.PlayerUnits.Count, Is.EqualTo(1));
            Assert.That(turnManager.EnemyUnits.Count, Is.EqualTo(2));
            Assert.That(turnManager.IsPlayerTurn, Is.True);
            Assert.That(deckManager.Hand.Count, Is.EqualTo(3));
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.EqualTo(player));
        }

        [Test]
        public void StartBattleSession_UsesSceneWorldPositionsAndKeepsReinforcementsOffOccupiedTiles()
        {
            using var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            EditorTestSupport.SetPrivateField(gridManager, "generateOnStart", false);
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = CreateSceneUnit(scope, "ScenePlayer", TeamType.Player, new Vector3(1f, 0f, 1f));
            var enemy = CreateSceneUnit(scope, "SceneEnemy", TeamType.Enemy, new Vector3(2f, 0f, 1f));

            EditorTestSupport.SetPrivateField(gameRoot, "gridManager", gridManager);
            EditorTestSupport.SetPrivateField(gameRoot, "turnManager", turnManager);
            EditorTestSupport.SetPrivateField(gameRoot, "deckManager", deckManager);
            EditorTestSupport.SetPrivateField(gameRoot, "cardResolver", cardResolver);
            EditorTestSupport.SetPrivateField(gameRoot, "enemyAI", enemyAi);
            EditorTestSupport.SetPrivateField(gameRoot, "battleUI", battleUi);
            EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");
            gameRoot.StartBattleSession();

            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.EqualTo(player));
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.EqualTo(enemy));
            Assert.That(turnManager.EnemyUnits.Count, Is.EqualTo(2));
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant.Team, Is.EqualTo(TeamType.Player));
        }

        private static UnitController CreateSceneUnit(TestObjectScope scope, string displayName, TeamType team, Vector3 worldPosition)
        {
            var gameObject = scope.Track(new GameObject(displayName));
            var unit = gameObject.AddComponent<UnitController>();

            EditorTestSupport.SetPrivateField(unit, "displayName", displayName);
            EditorTestSupport.SetPrivateField(unit, "team", team);
            EditorTestSupport.SetPrivateField(unit, "maxHp", 5);
            EditorTestSupport.SetPrivateField(unit, "attackPower", 2);
            EditorTestSupport.SetPrivateField(unit, "moveRange", 3);
            EditorTestSupport.SetProperty(unit, nameof(UnitController.CurrentHp), 5);
            unit.transform.position = worldPosition;
            return unit;
        }
    }
}
