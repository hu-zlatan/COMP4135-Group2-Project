using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private TileView tilePrefab;
        [SerializeField] private Transform tileRoot;
        [SerializeField] private int width = 6;
        [SerializeField] private int height = 6;
        [SerializeField] private float tileSpacing = 1f;
        [SerializeField] private bool generateOnStart = true;

        private readonly Dictionary<Vector2Int, TileView> tiles = new();
        private readonly List<TileView> highlightedTiles = new();

        public int Width => width;
        public int Height => height;

        private void Start()
        {
            if (generateOnStart && tiles.Count == 0)
            {
                GenerateGrid();
            }
        }

        public void GenerateGrid()
        {
            ClearGrid();
            EnsureTileRoot();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var coord = new Vector2Int(x, y);
                    var world = CoordToWorld(coord);
                    var tile = CreateTile(world);
                    tile.name = $"Tile_{x}_{y}";
                    tile.Initialize(coord);
                    tiles.Add(coord, tile);
                }
            }
        }

        public Vector3 CoordToWorld(Vector2Int coord)
        {
            return new Vector3(coord.x * tileSpacing, 0f, coord.y * tileSpacing);
        }

        public bool IsInBounds(Vector2Int coord)
        {
            return coord.x >= 0 && coord.x < width && coord.y >= 0 && coord.y < height;
        }

        public TileView GetTile(Vector2Int coord)
        {
            tiles.TryGetValue(coord, out var tile);
            return tile;
        }

        public bool TryPlaceUnit(UnitController unit, Vector2Int coord)
        {
            var tile = GetTile(coord);
            if (tile == null || tile.IsOccupied)
            {
                return false;
            }

            tile.SetOccupant(unit);
            unit.SetGridPosition(coord, CoordToWorld(coord));
            return true;
        }

        public bool TryMoveUnit(UnitController unit, Vector2Int targetCoord)
        {
            var targetTile = GetTile(targetCoord);
            if (targetTile == null || targetTile.IsOccupied)
            {
                return false;
            }

            var currentTile = GetTile(unit.GridPosition);
            currentTile?.ClearOccupant(unit);
            targetTile.SetOccupant(unit);
            unit.SetGridPosition(targetCoord, CoordToWorld(targetCoord));
            return true;
        }

        public bool TryPushUnit(UnitController unit, Vector2Int direction)
        {
            var destination = unit.GridPosition + direction;
            if (!IsInBounds(destination))
            {
                return false;
            }

            return TryMoveUnit(unit, destination);
        }

        public void ClearOccupancy(UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            var tile = GetTile(unit.GridPosition);
            tile?.ClearOccupant(unit);
        }

        public List<Vector2Int> GetNeighbors(Vector2Int coord)
        {
            var neighbors = new List<Vector2Int>(4);
            var candidates = new[]
            {
                coord + Vector2Int.up,
                coord + Vector2Int.down,
                coord + Vector2Int.left,
                coord + Vector2Int.right,
            };

            foreach (var candidate in candidates)
            {
                if (IsInBounds(candidate))
                {
                    neighbors.Add(candidate);
                }
            }

            return neighbors;
        }

        public List<TileView> GetTilesInRange(Vector2Int origin, int range, bool includeOccupied)
        {
            var results = new List<TileView>();
            var distances = new Dictionary<Vector2Int, int> { [origin] = 0 };
            var frontier = new Queue<Vector2Int>();
            frontier.Enqueue(origin);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                var currentDistance = distances[current];

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (distances.ContainsKey(neighbor))
                    {
                        continue;
                    }

                    var nextDistance = currentDistance + 1;
                    if (nextDistance > range)
                    {
                        continue;
                    }

                    distances[neighbor] = nextDistance;
                    frontier.Enqueue(neighbor);

                    var tile = GetTile(neighbor);
                    if (tile == null)
                    {
                        continue;
                    }

                    if (includeOccupied || !tile.IsOccupied)
                    {
                        results.Add(tile);
                    }
                }
            }

            return results;
        }

        public void HighlightMoveRange(Vector2Int origin, int range)
        {
            ClearHighlights();

            foreach (var tile in GetTilesInRange(origin, range, includeOccupied: false))
            {
                tile.SetMoveHighlight();
                highlightedTiles.Add(tile);
            }
        }

        public void HighlightAttackRange(Vector2Int origin, int range)
        {
            ClearHighlights();

            foreach (var tile in GetTilesInRange(origin, range, includeOccupied: true))
            {
                tile.SetAttackHighlight();
                highlightedTiles.Add(tile);
            }
        }

        public void ClearHighlights()
        {
            foreach (var tile in highlightedTiles)
            {
                tile.SetBaseVisual();
            }

            highlightedTiles.Clear();
        }

        private void EnsureTileRoot()
        {
            if (tileRoot != null)
            {
                return;
            }

            var root = new GameObject("GeneratedTiles");
            root.transform.SetParent(transform, false);
            tileRoot = root.transform;
        }

        private TileView CreateTile(Vector3 worldPosition)
        {
            if (tilePrefab != null)
            {
                return Instantiate(tilePrefab, worldPosition, Quaternion.identity, tileRoot);
            }

            var tileObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tileObject.transform.SetParent(tileRoot, false);
            tileObject.transform.position = worldPosition;
            tileObject.transform.localScale = new Vector3(0.9f, 0.1f, 0.9f);
            var tile = tileObject.AddComponent<TileView>();
            return tile;
        }

        private void ClearGrid()
        {
            foreach (var tile in tiles.Values)
            {
                if (tile != null)
                {
                    Destroy(tile.gameObject);
                }
            }

            tiles.Clear();
            highlightedTiles.Clear();
        }
    }
}
