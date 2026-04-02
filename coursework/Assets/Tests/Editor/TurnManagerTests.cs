using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class TurnManagerTests
    {
        private readonly List<GameObject> createdObjects = new();

        [TearDown]
        public void TearDown()
        {
            for (var i = createdObjects.Count - 1; i >= 0; i--)
            {
                var gameObject = createdObjects[i];
                if (gameObject != null)
                {
                    Object.DestroyImmediate(gameObject);
                }
            }

            createdObjects.Clear();
        }

        [Test]
        public void StartBattle_WhenNoEnemyUnits_EndsInPlayerVictory()
        {
            var turnManager = CreateTurnManager();
            var player = CreateUnit("Hero", TeamType.Player, maxHp: 5, currentHp: 5, attackPower: 2, gridPosition: new Vector2Int(0, 0));

            turnManager.RegisterUnit(player);
            turnManager.StartBattle();

            Assert.That(turnManager.BattleEnded, Is.True);
            Assert.That(turnManager.WinningTeam, Is.EqualTo(TeamType.Player));
            Assert.That(turnManager.BattleOutcomeSummary, Is.EqualTo("Victory! All enemy units defeated."));
            Assert.That(turnManager.IsPlayerTurn, Is.False);
            Assert.That(turnManager.RemainingCardPlays, Is.EqualTo(0));
            Assert.That(turnManager.TryConsumeCardPlay(), Is.False);
        }

        [Test]
        public void StartBattle_WhenNoPlayerUnits_EndsInEnemyVictory()
        {
            var turnManager = CreateTurnManager();
            var enemy = CreateUnit("Enemy", TeamType.Enemy, maxHp: 4, currentHp: 4, attackPower: 2, gridPosition: new Vector2Int(1, 0));

            turnManager.RegisterUnit(enemy);
            turnManager.StartBattle();

            Assert.That(turnManager.BattleEnded, Is.True);
            Assert.That(turnManager.WinningTeam, Is.EqualTo(TeamType.Enemy));
            Assert.That(turnManager.BattleOutcomeSummary, Is.EqualTo("Defeat! All player units were defeated."));
            Assert.That(turnManager.IsPlayerTurn, Is.False);
            Assert.That(turnManager.RemainingCardPlays, Is.EqualTo(0));
        }

        [Test]
        public void EndPlayerTurn_WhenEnemyDefeatsLastPlayer_DoesNotRestartPlayerTurn()
        {
            var turnManager = CreateTurnManager();
            var player = CreateUnit("Hero", TeamType.Player, maxHp: 1, currentHp: 1, attackPower: 1, gridPosition: new Vector2Int(0, 0));
            var enemy = CreateUnit("Enemy", TeamType.Enemy, maxHp: 4, currentHp: 4, attackPower: 2, gridPosition: new Vector2Int(1, 0));

            turnManager.RegisterUnit(player);
            turnManager.RegisterUnit(enemy);
            turnManager.StartBattle();

            Assert.That(turnManager.IsPlayerTurn, Is.True);
            Assert.That(turnManager.RemainingCardPlays, Is.GreaterThan(0));

            turnManager.EndPlayerTurn();

            Assert.That(player.IsAlive, Is.False);
            Assert.That(turnManager.BattleEnded, Is.True);
            Assert.That(turnManager.WinningTeam, Is.EqualTo(TeamType.Enemy));
            Assert.That(turnManager.BattleOutcomeSummary, Is.EqualTo("Defeat! All player units were defeated."));
            Assert.That(turnManager.LastEnemyActionSummary, Is.EqualTo("Defeat! All player units were defeated."));
            Assert.That(turnManager.IsPlayerTurn, Is.False);
            Assert.That(turnManager.RemainingCardPlays, Is.EqualTo(0));
            Assert.That(turnManager.TryConsumeCardPlay(), Is.False);
        }

        private TurnManager CreateTurnManager()
        {
            var gameObject = Track(new GameObject("TurnManager"));
            var turnManager = gameObject.AddComponent<TurnManager>();
            var enemyAi = Track(new GameObject("EnemyAI")).AddComponent<EnemyAI>();
            var gridManager = Track(new GameObject("GridManager")).AddComponent<GridManager>();

            turnManager.Initialize(null, enemyAi, gridManager);
            return turnManager;
        }

        private UnitController CreateUnit(
            string displayName,
            TeamType team,
            int maxHp,
            int currentHp,
            int attackPower,
            Vector2Int gridPosition)
        {
            var gameObject = Track(new GameObject(displayName));
            var unit = gameObject.AddComponent<UnitController>();

            SetPrivateField(unit, "displayName", displayName);
            SetPrivateField(unit, "team", team);
            SetPrivateField(unit, "maxHp", maxHp);
            SetPrivateField(unit, "attackPower", attackPower);
            SetProperty(unit, nameof(UnitController.CurrentHp), currentHp);
            unit.SetGridPosition(gridPosition, Vector3.zero);
            return unit;
        }

        private GameObject Track(GameObject gameObject)
        {
            createdObjects.Add(gameObject);
            return gameObject;
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
    }
}
