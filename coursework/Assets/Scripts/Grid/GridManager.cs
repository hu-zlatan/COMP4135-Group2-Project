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
        [SerializeField] private float boardMargin = 0.72f;
        [SerializeField] private float boardThickness = 0.30f;
        [SerializeField] private float frameThickness = 0.24f;
        [SerializeField] private float frameHeight = 0.36f;

        private readonly Dictionary<Vector2Int, TileView> tiles = new();
        private readonly List<TileView> highlightedTiles = new();
        private Transform boardPresentationRoot;

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

            BuildBoardPresentation();
        }

        public Vector3 CoordToWorld(Vector2Int coord)
        {
            return new Vector3(coord.x * tileSpacing, 0f, coord.y * tileSpacing);
        }

        public Vector2Int WorldToCoord(Vector3 worldPosition)
        {
            if (Mathf.Approximately(tileSpacing, 0f))
            {
                return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
            }

            return new Vector2Int(
                Mathf.RoundToInt(worldPosition.x / tileSpacing),
                Mathf.RoundToInt(worldPosition.z / tileSpacing));
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
            tileObject.transform.localScale = new Vector3(0.92f, 0.12f, 0.92f);
            var tile = tileObject.AddComponent<TileView>();
            return tile;
        }

        private void ClearGrid()
        {
            if (boardPresentationRoot != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(boardPresentationRoot.gameObject);
                }
                else
                {
                    DestroyImmediate(boardPresentationRoot.gameObject);
                }

                boardPresentationRoot = null;
            }

            foreach (var tile in tiles.Values)
            {
                if (tile != null)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(tile.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(tile.gameObject);
                    }
                }
            }

            tiles.Clear();
            highlightedTiles.Clear();
        }

        private void BuildBoardPresentation()
        {
            if (tileRoot == null)
            {
                return;
            }

            if (boardPresentationRoot != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(boardPresentationRoot.gameObject);
                }
                else
                {
                    DestroyImmediate(boardPresentationRoot.gameObject);
                }
            }

            boardPresentationRoot = new GameObject("BoardPresentation").transform;
            boardPresentationRoot.SetParent(tileRoot, false);

            var boardCenter = new Vector3(((width - 1) * tileSpacing) * 0.5f, 0f, ((height - 1) * tileSpacing) * 0.5f);
            var boardSize = new Vector3(((width - 1) * tileSpacing) + 1f + boardMargin, boardThickness, ((height - 1) * tileSpacing) + 1f + boardMargin);

            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "BoardBase",
                boardCenter + new Vector3(0f, -0.22f, 0f),
                boardSize,
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("BoardBase", WorldThemeResources.BoardBase));

            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "BoardInset",
                boardCenter + new Vector3(0f, -0.06f, 0f),
                new Vector3(boardSize.x - 0.18f, 0.08f, boardSize.z - 0.18f),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("BoardInset", WorldThemeResources.BoardTrim));

            var halfWidth = boardSize.x * 0.5f;
            var halfDepth = boardSize.z * 0.5f;
            var frameY = 0.12f;
            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "FrameNorth",
                boardCenter + new Vector3(0f, frameY, halfDepth - (frameThickness * 0.5f)),
                new Vector3(boardSize.x, frameHeight, frameThickness),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("Frame", WorldThemeResources.BoardFrame));
            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "FrameSouth",
                boardCenter + new Vector3(0f, frameY, -halfDepth + (frameThickness * 0.5f)),
                new Vector3(boardSize.x, frameHeight, frameThickness),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("Frame", WorldThemeResources.BoardFrame));
            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "FrameWest",
                boardCenter + new Vector3(-halfWidth + (frameThickness * 0.5f), frameY, 0f),
                new Vector3(frameThickness, frameHeight, boardSize.z - (frameThickness * 2f)),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("Frame", WorldThemeResources.BoardFrame));
            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                "FrameEast",
                boardCenter + new Vector3(halfWidth - (frameThickness * 0.5f), frameY, 0f),
                new Vector3(frameThickness, frameHeight, boardSize.z - (frameThickness * 2f)),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("Frame", WorldThemeResources.BoardFrame));

            CreateCornerPost("NorthWest", boardCenter + new Vector3(-halfWidth + 0.18f, 0.18f, halfDepth - 0.18f));
            CreateCornerPost("NorthEast", boardCenter + new Vector3(halfWidth - 0.18f, 0.18f, halfDepth - 0.18f));
            CreateCornerPost("SouthWest", boardCenter + new Vector3(-halfWidth + 0.18f, 0.18f, -halfDepth + 0.18f));
            CreateCornerPost("SouthEast", boardCenter + new Vector3(halfWidth - 0.18f, 0.18f, -halfDepth + 0.18f));

            CreateBeacon(
                "PlayerBeacon",
                boardCenter + new Vector3(-0.9f, 0f, -halfDepth - 0.22f),
                WorldThemeResources.PlayerMarker,
                WorldThemeResources.PlayerSecondaryMarker);
            CreateBeacon(
                "EnemyBeacon",
                boardCenter + new Vector3(0.9f, 0f, halfDepth + 0.22f),
                WorldThemeResources.EnemyMarker,
                WorldThemeResources.EnemySecondaryMarker);
        }

        private void CreateCornerPost(string suffix, Vector3 position)
        {
            CreatePresentationPrimitive(
                PrimitiveType.Cylinder,
                boardPresentationRoot,
                $"CornerPost_{suffix}",
                position,
                new Vector3(0.12f, 0.18f, 0.12f),
                Vector3.zero,
                WorldThemeResources.GetSharedMaterial("CornerPost", WorldThemeResources.BoardTrim));
        }

        private void CreateBeacon(string namePrefix, Vector3 position, Color primary, Color accent)
        {
            CreatePresentationPrimitive(
                PrimitiveType.Cylinder,
                boardPresentationRoot,
                $"{namePrefix}_Base",
                position + new Vector3(0f, -0.10f, 0f),
                new Vector3(0.18f, 0.08f, 0.18f),
                Vector3.zero,
                WorldThemeResources.CreateMaterial($"{namePrefix}_BaseMat", accent));
            CreatePresentationPrimitive(
                PrimitiveType.Cube,
                boardPresentationRoot,
                $"{namePrefix}_Stem",
                position + new Vector3(0f, 0.12f, 0f),
                new Vector3(0.08f, 0.24f, 0.08f),
                Vector3.zero,
                WorldThemeResources.CreateMaterial($"{namePrefix}_StemMat", accent));
            CreatePresentationPrimitive(
                PrimitiveType.Sphere,
                boardPresentationRoot,
                $"{namePrefix}_Orb",
                position + new Vector3(0f, 0.34f, 0f),
                new Vector3(0.18f, 0.18f, 0.18f),
                Vector3.zero,
                WorldThemeResources.CreateMaterial($"{namePrefix}_OrbMat", primary));
        }

        private static void CreatePresentationPrimitive(
            PrimitiveType primitiveType,
            Transform parent,
            string objectName,
            Vector3 localPosition,
            Vector3 localScale,
            Vector3 localEulerAngles,
            Material material)
        {
            var decoration = GameObject.CreatePrimitive(primitiveType);
            decoration.name = objectName;
            decoration.transform.SetParent(parent, false);
            decoration.transform.localPosition = localPosition;
            decoration.transform.localScale = localScale;
            decoration.transform.localEulerAngles = localEulerAngles;

            var collider = decoration.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
                if (Application.isPlaying)
                {
                    Destroy(collider);
                }
                else
                {
                    DestroyImmediate(collider);
                }
            }

            if (decoration.TryGetComponent<Renderer>(out var renderer))
            {
                renderer.sharedMaterial = material;
            }
        }
    }
}
