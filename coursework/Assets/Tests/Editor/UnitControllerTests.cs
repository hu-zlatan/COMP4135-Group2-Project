using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class UnitControllerTests
    {
        [Test]
        public void CreateUnit_InitializesCurrentHpFromConfiguredValue()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0), maxHp: 8, currentHp: 6);

            Assert.That(unit.MaxHp, Is.EqualTo(8));
            Assert.That(unit.CurrentHp, Is.EqualTo(6));
            Assert.That(unit.IsAlive, Is.True);
        }

        [Test]
        public void TakeDamage_ReducesHitPoints()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0), maxHp: 8, currentHp: 8);

            unit.TakeDamage(3);

            Assert.That(unit.CurrentHp, Is.EqualTo(5));
        }

        [Test]
        public void Guarding_ReducesNextHitAndThenClearsGuard()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0), maxHp: 8, currentHp: 8);
            unit.SetGuarding(true);

            unit.TakeDamage(3);

            Assert.That(unit.CurrentHp, Is.EqualTo(6));
            Assert.That(unit.IsGuarding, Is.False);
        }

        [Test]
        public void Heal_DoesNotExceedMaxHp()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0), maxHp: 8, currentHp: 4);

            unit.Heal(10);

            Assert.That(unit.CurrentHp, Is.EqualTo(8));
        }

        [Test]
        public void TakeDamage_ToZero_DisablesGameObject()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0), maxHp: 2, currentHp: 2);

            unit.TakeDamage(2);

            Assert.That(unit.IsAlive, Is.False);
            Assert.That(unit.gameObject.activeSelf, Is.False);
        }

        [Test]
        public void SetGridPosition_UpdatesGridAndWorldPosition()
        {
            using var scope = new TestObjectScope();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0));

            unit.SetGridPosition(new Vector2Int(2, 3), new Vector3(2f, 0f, 3f));

            Assert.That(unit.GridPosition, Is.EqualTo(new Vector2Int(2, 3)));
            Assert.That(unit.transform.position, Is.EqualTo(new Vector3(2f, 0.6f, 3f)));
        }
    }
}
