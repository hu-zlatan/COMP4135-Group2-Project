using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace TacticalCards.Tests.Editor
{
    public class GridManagerTests
    {
        [Test]
        public void GenerateGrid_CreatesExpectedTiles()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope, width: 4, height: 3);

            Assert.That(gridManager.GetTile(new Vector2Int(0, 0)), Is.Not.Null);
            Assert.That(gridManager.GetTile(new Vector2Int(3, 2)), Is.Not.Null);
            Assert.That(gridManager.GetTile(new Vector2Int(4, 0)), Is.Null);
            Assert.That(gridManager.Width, Is.EqualTo(4));
            Assert.That(gridManager.Height, Is.EqualTo(3));
        }

        [Test]
        public void GenerateGrid_BuildsBoardPresentationAndMarkers()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope, width: 4, height: 3);

            var presentationRoot = gridManager.transform.Find("GeneratedTiles/BoardPresentation");

            Assert.That(presentationRoot, Is.Not.Null);
            Assert.That(presentationRoot.Find("BoardBase"), Is.Not.Null);
            Assert.That(presentationRoot.Find("FrameNorth"), Is.Not.Null);
            Assert.That(presentationRoot.Find("PlayerBeacon_Orb"), Is.Not.Null);
            Assert.That(presentationRoot.Find("EnemyBeacon_Orb"), Is.Not.Null);
        }

        [Test]
        public void TryPlaceUnit_PlacesUnitAndUpdatesOccupancy()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));

            var placed = gridManager.TryPlaceUnit(unit, new Vector2Int(1, 1));

            Assert.That(placed, Is.True);
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.EqualTo(unit));
            Assert.That(unit.GridPosition, Is.EqualTo(new Vector2Int(1, 1)));
        }

        [Test]
        public void TryMoveUnit_MovesUnitAndClearsPreviousTile()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(1, 1));

            gridManager.TryPlaceUnit(unit, new Vector2Int(1, 1));
            var moved = gridManager.TryMoveUnit(unit, new Vector2Int(2, 1));

            Assert.That(moved, Is.True);
            Assert.That(gridManager.GetTile(new Vector2Int(1, 1)).Occupant, Is.Null);
            Assert.That(gridManager.GetTile(new Vector2Int(2, 1)).Occupant, Is.EqualTo(unit));
        }

        [Test]
        public void TryPushUnit_RejectsOutOfBoundsDestination()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope);
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(0, 0));

            gridManager.TryPlaceUnit(unit, new Vector2Int(0, 0));
            var pushed = gridManager.TryPushUnit(unit, Vector2Int.left);

            Assert.That(pushed, Is.False);
            Assert.That(unit.GridPosition, Is.EqualTo(new Vector2Int(0, 0)));
        }

        [Test]
        public void GetNeighbors_ReturnsOrthogonalInBoundsCoordinates()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope, width: 3, height: 3);

            var neighbors = gridManager.GetNeighbors(new Vector2Int(0, 1));

            Assert.That(neighbors, Is.EquivalentTo(new[]
            {
                new Vector2Int(0, 2),
                new Vector2Int(0, 0),
                new Vector2Int(1, 1),
            }));
        }

        [Test]
        public void GetTilesInRange_ReturnsBreadthFirstManhattanRange()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope, width: 5, height: 5);
            var unit = EditorTestSupport.CreateUnit(scope, "Hero", TeamType.Player, new Vector2Int(2, 2));

            gridManager.TryPlaceUnit(unit, unit.GridPosition);
            var tiles = gridManager.GetTilesInRange(unit.GridPosition, range: 2, includeOccupied: false);

            Assert.That(tiles.Count, Is.EqualTo(12));
            Assert.That(tiles.Exists(tile => tile.Coord == new Vector2Int(4, 2)), Is.True);
            Assert.That(tiles.Exists(tile => tile.Coord == new Vector2Int(2, 2)), Is.False);
        }

        [Test]
        public void HighlightMoveRange_TracksHighlightedTilesAndClearHighlights_ResetsState()
        {
            using var scope = new TestObjectScope();
            var gridManager = EditorTestSupport.CreateGridManager(scope, width: 5, height: 5);

            gridManager.HighlightMoveRange(new Vector2Int(2, 2), 1);
            var highlighted = EditorTestSupport.GetPrivateField<List<TileView>>(gridManager, "highlightedTiles");
            Assert.That(highlighted.Count, Is.EqualTo(4));

            gridManager.ClearHighlights();
            Assert.That(highlighted.Count, Is.EqualTo(0));
        }
    }
}
