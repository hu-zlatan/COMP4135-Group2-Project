using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class TileViewTests
    {
        [Test]
        public void Initialize_SetsCoordinate()
        {
            using var scope = new TestObjectScope();
            var tileObject = scope.Track(GameObject.CreatePrimitive(PrimitiveType.Cube));
            var tile = tileObject.AddComponent<TileView>();

            tile.Initialize(new Vector2Int(2, 4));

            Assert.That(tile.Coord, Is.EqualTo(new Vector2Int(2, 4)));
        }

        [Test]
        public void SetOccupantAndClearOccupant_UpdateOccupancyState()
        {
            using var scope = new TestObjectScope();
            var tileObject = scope.Track(GameObject.CreatePrimitive(PrimitiveType.Cube));
            var tile = tileObject.AddComponent<TileView>();
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0));

            tile.SetOccupant(unit);
            Assert.That(tile.IsOccupied, Is.True);
            Assert.That(tile.Occupant, Is.EqualTo(unit));

            tile.ClearOccupant(unit);
            Assert.That(tile.IsOccupied, Is.False);
            Assert.That(tile.Occupant, Is.Null);
        }

        [Test]
        public void HighlightMethods_CreateRuntimeMaterialAndDoNotThrow()
        {
            using var scope = new TestObjectScope();
            var tileObject = scope.Track(GameObject.CreatePrimitive(PrimitiveType.Cube));
            var tile = tileObject.AddComponent<TileView>();
            tile.Initialize(Vector2Int.zero);

            Assert.DoesNotThrow(() => tile.SetMoveHighlight());
            Assert.DoesNotThrow(() => tile.SetAttackHighlight());
            Assert.DoesNotThrow(() => tile.SetSelected());
            Assert.DoesNotThrow(() => tile.SetBaseVisual());
            Assert.That(tileObject.GetComponent<Renderer>().sharedMaterial, Is.Not.Null);
        }
    }
}
