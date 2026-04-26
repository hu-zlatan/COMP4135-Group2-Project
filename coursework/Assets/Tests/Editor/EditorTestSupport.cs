using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    internal sealed class TestObjectScope : IDisposable
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

    internal static class EditorTestSupport
    {
        public static CardData CreateCard(
            TestObjectScope scope,
            string id,
            string name,
            CardType cardType,
            TargetType targetType,
            int range = 1,
            int power = 1,
            int moveDistance = 1,
            string description = "")
        {
            return scope.Track(CardData.CreateRuntime(id, name, cardType, targetType, range, power, moveDistance, description));
        }

        public static UnitController CreateUnit(
            TestObjectScope scope,
            string displayName,
            TeamType team,
            Vector2Int gridPosition,
            int maxHp = 5,
            int currentHp = 5,
            int attackPower = 2,
            int moveRange = 3)
        {
            var gameObject = scope.Track(new GameObject(displayName));
            var unit = gameObject.AddComponent<UnitController>();

            SetPrivateField(unit, "displayName", displayName);
            SetPrivateField(unit, "team", team);
            SetPrivateField(unit, "maxHp", maxHp);
            SetPrivateField(unit, "attackPower", attackPower);
            SetPrivateField(unit, "moveRange", moveRange);
            SetProperty(unit, nameof(UnitController.CurrentHp), currentHp);
            unit.SetGridPosition(gridPosition, new Vector3(gridPosition.x, 0f, gridPosition.y));
            return unit;
        }

        public static GridManager CreateGridManager(TestObjectScope scope, int width = 6, int height = 6)
        {
            var gameObject = scope.Track(new GameObject("GridManager"));
            var gridManager = gameObject.AddComponent<GridManager>();

            SetPrivateField(gridManager, "width", width);
            SetPrivateField(gridManager, "height", height);
            SetPrivateField(gridManager, "generateOnStart", false);
            gridManager.GenerateGrid();
            return gridManager;
        }

        public static EnemyAI CreateEnemyAI(TestObjectScope scope)
        {
            return scope.Track(new GameObject("EnemyAI")).AddComponent<EnemyAI>();
        }

        public static DeckManager CreateDeckManager(TestObjectScope scope, List<CardData> startingDeck = null, int cardsDrawnPerTurn = 3)
        {
            var gameObject = scope.Track(new GameObject("DeckManager"));
            var deckManager = gameObject.AddComponent<DeckManager>();

            SetPrivateField(deckManager, "cardsDrawnPerTurn", cardsDrawnPerTurn);
            if (startingDeck != null)
            {
                SetPrivateField(deckManager, "startingDeck", new List<CardData>(startingDeck));
            }

            deckManager.ResetDeck();
            return deckManager;
        }

        public static CardResolver CreateCardResolver(TestObjectScope scope, GridManager gridManager)
        {
            var cardResolver = scope.Track(new GameObject("CardResolver")).AddComponent<CardResolver>();
            cardResolver.Initialize(gridManager);
            return cardResolver;
        }

        public static TurnManager CreateTurnManager(TestObjectScope scope, DeckManager deckManager = null, EnemyAI enemyAI = null, GridManager gridManager = null)
        {
            var turnManager = scope.Track(new GameObject("TurnManager")).AddComponent<TurnManager>();
            turnManager.Initialize(deckManager, enemyAI, gridManager);
            return turnManager;
        }

        public static BattleUI CreateBattleUi(
            TestObjectScope scope,
            TurnManager turnManager,
            DeckManager deckManager,
            CardResolver cardResolver,
            GridManager gridManager)
        {
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            battleUi.Initialize(turnManager, deckManager, cardResolver, gridManager);
            return battleUi;
        }

        public static void RegisterAndPlaceUnits(TurnManager turnManager, GridManager gridManager, params UnitController[] units)
        {
            foreach (var unit in units)
            {
                turnManager.RegisterUnit(unit);
                Assert.That(gridManager.TryPlaceUnit(unit, unit.GridPosition), Is.True, $"Failed to place {unit.DisplayName} at {unit.GridPosition}");
            }
        }

        public static void SetPrivateField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(field, Is.Not.Null, $"Missing field: {fieldName}");
            field.SetValue(target, value);
        }

        public static T GetPrivateField<T>(object target, string fieldName)
        {
            var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(field, Is.Not.Null, $"Missing field: {fieldName}");
            return (T)field.GetValue(target);
        }

        public static void SetProperty(object target, string propertyName, object value)
        {
            var property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.That(property, Is.Not.Null, $"Missing property: {propertyName}");
            property.SetValue(target, value);
        }

        public static void InvokePrivateMethod(object target, string methodName, params object[] args)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, $"Missing method: {methodName}");
            method.Invoke(target, args);
        }

        public static T InvokePrivateMethod<T>(object target, string methodName, params object[] args)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, $"Missing method: {methodName}");
            return (T)method.Invoke(target, args);
        }
    }
}
