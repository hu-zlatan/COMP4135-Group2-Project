using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class EnemyAITests
    {
        [Test]
        public void ManhattanDistance_ReturnsAbsoluteGridDistance()
        {
            using var scope = new TestObjectScope();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);

            var distance = enemyAi.ManhattanDistance(new Vector2Int(1, 1), new Vector2Int(4, 3));

            Assert.That(distance, Is.EqualTo(5));
        }

        [Test]
        public void FindNearestPlayer_SkipsDeadUnitsAndReturnsClosestAliveTarget()
        {
            using var scope = new TestObjectScope();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(4, 4));
            var deadPlayer = EditorTestSupport.CreateUnit(scope, "Dead", TeamType.Player, new Vector2Int(3, 4), maxHp: 1, currentHp: 1);
            var nearPlayer = EditorTestSupport.CreateUnit(scope, "Near", TeamType.Player, new Vector2Int(4, 2));
            var farPlayer = EditorTestSupport.CreateUnit(scope, "Far", TeamType.Player, new Vector2Int(0, 0));

            deadPlayer.TakeDamage(1);
            var result = enemyAi.FindNearestPlayer(enemy, new System.Collections.Generic.List<UnitController> { deadPlayer, farPlayer, nearPlayer });

            Assert.That(result, Is.EqualTo(nearPlayer));
        }

        [Test]
        public void GetStepToward_PrefersHorizontalStepWhenAxesTie()
        {
            using var scope = new TestObjectScope();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(1, 1));
            var target = EditorTestSupport.CreateUnit(scope, "Target", TeamType.Player, new Vector2Int(3, 3));

            var step = enemyAi.GetStepToward(enemy, target);

            Assert.That(step, Is.EqualTo(Vector2Int.right));
        }

        [Test]
        public void GetStepToward_UsesVerticalStepWhenVerticalDeltaIsLarger()
        {
            using var scope = new TestObjectScope();
            var enemyAi = EditorTestSupport.CreateEnemyAI(scope);
            var enemy = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1));
            var target = EditorTestSupport.CreateUnit(scope, "Target", TeamType.Player, new Vector2Int(3, 4));

            var step = enemyAi.GetStepToward(enemy, target);

            Assert.That(step, Is.EqualTo(Vector2Int.up));
        }
    }
}
