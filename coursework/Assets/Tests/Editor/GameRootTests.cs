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
        public void Start_InitializesManagersPlacesUnitsAndStartsBattle()
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
            EditorTestSupport.SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            EditorTestSupport.InvokePrivateMethod(gameRoot, "Awake");
            EditorTestSupport.InvokePrivateMethod(gameRoot, "Start");

            Assert.That(turnManager.PlayerUnits.Count, Is.EqualTo(1));
            Assert.That(turnManager.EnemyUnits.Count, Is.EqualTo(1));
            Assert.That(turnManager.IsPlayerTurn, Is.True);
            Assert.That(deckManager.Hand.Count, Is.EqualTo(3));
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.EqualTo(player));
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.EqualTo(enemy));
        }
    }
}
