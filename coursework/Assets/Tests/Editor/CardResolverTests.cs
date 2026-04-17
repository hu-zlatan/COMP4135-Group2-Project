using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class CardResolverTests
    {
        [Test]
        public void GetValidTiles_Move_ReturnsUnoccupiedTilesWithinRange()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(2, 2));
            var blocker = EditorTestSupport.CreateUnit(scope, "Blocker", TeamType.Player, new Vector2Int(3, 2));
            var turnManager = EditorTestSupport.CreateTurnManager(scope, gridManager: gridManager);

            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, caster, blocker);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 2);

            var validTiles = cardResolver.GetValidTiles(moveCard, caster);

            Assert.That(validTiles, Has.Count.EqualTo(11));
            Assert.That(validTiles.Exists(tile => tile.Coord == blocker.GridPosition), Is.False);
            Assert.That(validTiles.Exists(tile => tile.Coord == new Vector2Int(4, 2)), Is.True);
        }

        [Test]
        public void ResolveTileCard_Move_MovesUnitToTargetTile()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var turnManager = EditorTestSupport.CreateTurnManager(scope, gridManager: gridManager);

            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, caster);
            var moveCard = EditorTestSupport.CreateCard(scope, "move", "Move", CardType.Move, TargetType.Tile, moveDistance: 2);
            var targetTile = gridManager.GetTile(new Vector2Int(2, 1));

            var resolved = cardResolver.ResolveTileCard(moveCard, caster, targetTile);

            Assert.That(resolved, Is.True);
            Assert.That(caster.GridPosition, Is.EqualTo(new Vector2Int(2, 1)));
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.EqualTo(caster));
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.Null);
        }

        [Test]
        public void ResolveTileCard_Guard_SetsUnitGuarding()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var guardCard = EditorTestSupport.CreateCard(scope, "guard", "Guard", CardType.Guard, TargetType.Self, range: 0, power: 0, moveDistance: 0);

            var resolved = cardResolver.ResolveTileCard(guardCard, caster, gridManager.GetTile(new Vector2Int(1, 1)));

            Assert.That(resolved, Is.True);
            Assert.That(caster.IsGuarding, Is.True);
        }

        [Test]
        public void ResolveUnitCard_Strike_DamagesEnemyAndClearsTileWhenTargetDies()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var target = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1), maxHp: 2, currentHp: 2);
            var turnManager = EditorTestSupport.CreateTurnManager(scope, gridManager: gridManager);

            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, caster, target);
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 3);

            var resolved = cardResolver.ResolveUnitCard(strikeCard, caster, target);

            Assert.That(resolved, Is.True);
            Assert.That(target.IsAlive, Is.False);
            Assert.That(target.gameObject.activeSelf, Is.False);
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.Null);
        }

        [Test]
        public void ResolveUnitCard_Push_DamagesAndMovesEnemyBack()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var target = EditorTestSupport.CreateUnit(scope, "Enemy", TeamType.Enemy, new Vector2Int(2, 1), maxHp: 5, currentHp: 5);
            var turnManager = EditorTestSupport.CreateTurnManager(scope, gridManager: gridManager);

            EditorTestSupport.RegisterAndPlaceUnits(turnManager, gridManager, caster, target);
            var pushCard = EditorTestSupport.CreateCard(scope, "push", "Push", CardType.Push, TargetType.EnemyUnit, range: 1, power: 1);

            var resolved = cardResolver.ResolveUnitCard(pushCard, caster, target);

            Assert.That(resolved, Is.True);
            Assert.That(target.CurrentHp, Is.EqualTo(4));
            Assert.That(target.GridPosition, Is.EqualTo(new Vector2Int(3, 1)));
        }

        [Test]
        public void ResolveUnitCard_RejectsFriendlyTarget()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var cardResolver = EditorTestSupport.CreateCardResolver(scope, gridManager);
            var caster = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));
            var ally = EditorTestSupport.CreateUnit(scope, "Ally", TeamType.Player, new Vector2Int(2, 1));
            var strikeCard = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit, range: 1, power: 2);

            var resolved = cardResolver.ResolveUnitCard(strikeCard, caster, ally);

            Assert.That(resolved, Is.False);
        }
    }
}
