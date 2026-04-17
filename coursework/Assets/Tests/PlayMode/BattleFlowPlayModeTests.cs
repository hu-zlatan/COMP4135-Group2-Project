using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.PlayMode
{
    public class BattleFlowPlayModeTests
    {
        [Test]
        public void GameRoot_Start_InitializesBattleReadyState()
        {
            using var scope = new TestObjectScope();
            var gridManager = scope.Track(new GameObject("GridManager")).AddComponent<GridManager>();
            SetPrivateField(gridManager, "generateOnStart", false);

            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            var deckManager = CreateDeckManager(scope);
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            var enemyAi = scope.Track(new GameObject("EnemyAI")).AddComponent<EnemyAI>();
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var gameRoot = scope.Track(new GameObject("GameRoot")).AddComponent<GameRoot>();
            var player = CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var enemy = CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));

            player.transform.position = new Vector3(1f, 0f, 1f);
            enemy.transform.position = new Vector3(2f, 0f, 1f);

            SetPrivateField(gameRoot, "gridManager", gridManager);
            SetPrivateField(gameRoot, "turnManager", turnManager);
            SetPrivateField(gameRoot, "deckManager", deckManager);
            SetPrivateField(gameRoot, "cardResolver", cardResolver);
            SetPrivateField(gameRoot, "enemyAI", enemyAi);
            SetPrivateField(gameRoot, "battleUI", battleUi);
            SetPrivateField(gameRoot, "sceneUnits", new[] { player, enemy });

            InvokePrivateMethod(gameRoot, "Awake");
            InvokePrivateMethod(gameRoot, "Start");

            Assert.That(turnManager.PlayerUnits.Count, Is.EqualTo(1));
            Assert.That(turnManager.EnemyUnits.Count, Is.EqualTo(1));
            Assert.That(turnManager.IsPlayerTurn, Is.True);
            Assert.That(deckManager.Hand.Count, Is.EqualTo(3));
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.EqualTo(player));
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.EqualTo(enemy));
        }

        private static DeckManager CreateDeckManager(TestObjectScope scope)
        {
            var deckManager = scope.Track(new GameObject("DeckManager")).AddComponent<DeckManager>();
            SetPrivateField(deckManager, "cardsDrawnPerTurn", 3);
            deckManager.ResetDeck();
            return deckManager;
        }

        private static UnitController CreateUnit(TestObjectScope scope, string displayName, TeamType team, Vector2Int gridPosition)
        {
            var gameObject = scope.Track(new GameObject(displayName));
            var unit = gameObject.AddComponent<UnitController>();

            SetPrivateField(unit, "displayName", displayName);
            SetPrivateField(unit, "team", team);
            SetPrivateField(unit, "maxHp", 5);
            SetPrivateField(unit, "attackPower", 2);
            SetPrivateField(unit, "moveRange", 3);
            SetProperty(unit, nameof(UnitController.CurrentHp), 5);
            unit.SetGridPosition(gridPosition, Vector3.zero);
            return unit;
        }

        private static void SetPrivateField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(field, Is.Not.Null, $"Missing field: {fieldName}");
            field.SetValue(target, value);
        }

        private static void SetProperty(object target, string propertyName, object value)
        {
            var property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.That(property, Is.Not.Null, $"Missing property: {propertyName}");
            property.SetValue(target, value);
        }

        private static void InvokePrivateMethod(object target, string methodName)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, $"Missing method: {methodName}");
            method.Invoke(target, null);
        }

        private sealed class TestObjectScope : IDisposable
        {
            private readonly List<UnityEngine.Object> created = new();

            public T Track<T>(T obj) where T : UnityEngine.Object
            {
                created.Add(obj);
                return obj;
            }

            public void Dispose()
            {
                for (var i = created.Count - 1; i >= 0; i--)
                {
                    var obj = created[i];
                    if (obj != null)
                    {
                        UnityEngine.Object.DestroyImmediate(obj);
                    }
                }

                created.Clear();
            }
        }
    }
}
