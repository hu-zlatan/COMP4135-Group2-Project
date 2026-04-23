using UnityEngine;
using System.Collections.Generic;

namespace TacticalCards
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private GridManager gridManager;
        [SerializeField] private TurnManager turnManager;
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private CardResolver cardResolver;
        [SerializeField] private EnemyAI enemyAI;
        [SerializeField] private BattleUI battleUI;
        [SerializeField] private bool disableFrontEndFlow;
        [SerializeField] private UnitController[] sceneUnits;

        private GameFlowController gameFlowController;
        private readonly List<UnitController> runtimeReinforcements = new();
        private readonly List<UnitSpawnSnapshot> initialSnapshots = new();

        private void Awake()
        {
            gridManager ??= FindFirstObjectByType<GridManager>();
            turnManager ??= FindFirstObjectByType<TurnManager>();
            deckManager ??= FindFirstObjectByType<DeckManager>();
            cardResolver ??= FindFirstObjectByType<CardResolver>();
            enemyAI ??= FindFirstObjectByType<EnemyAI>();
            battleUI ??= FindFirstObjectByType<BattleUI>();
            gameFlowController ??= FindFirstObjectByType<GameFlowController>();

            if (sceneUnits == null || sceneUnits.Length == 0)
            {
                sceneUnits = FindObjectsByType<UnitController>(FindObjectsSortMode.None);
            }

            CacheInitialSnapshots();
        }

        private void Start()
        {
            if (!disableFrontEndFlow)
            {
                EnsureFlowController();
                gameFlowController.Initialize(this, battleUI, turnManager);
                return;
            }

            StartBattleSession();
        }

        public void PrepareBattle()
        {
            ClearRuntimeReinforcements();
            gridManager?.GenerateGrid();
            cardResolver?.Initialize(gridManager);
            deckManager?.ResetDeck();
            turnManager?.Initialize(deckManager, enemyAI, gridManager);
            RestoreSceneUnits();
            RegisterSnapshotUnits();
            SpawnReinforcementIfNeeded();
            RegisterRuntimeReinforcements();
            battleUI?.Initialize(turnManager, deckManager, cardResolver, gridManager);
            battleUI?.Refresh();
        }

        public void StartBattleSession()
        {
            PrepareBattle();
            turnManager?.StartBattle();
            battleUI?.Refresh();
        }

        private void EnsureFlowController()
        {
            if (gameFlowController != null)
            {
                return;
            }

            gameFlowController = new GameObject("GameFlowController").AddComponent<GameFlowController>();
        }

        private void CacheInitialSnapshots()
        {
            if (initialSnapshots.Count > 0 || sceneUnits == null)
            {
                return;
            }

            foreach (var unit in sceneUnits)
            {
                if (unit == null)
                {
                    continue;
                }

                initialSnapshots.Add(new UnitSpawnSnapshot(unit, ResolveInitialCoord(unit)));
            }
        }

        private void RestoreSceneUnits()
        {
            foreach (var snapshot in initialSnapshots)
            {
                if (snapshot.Unit == null)
                {
                    continue;
                }

                snapshot.Unit.ResetForBattle(snapshot.GridPosition, gridManager.CoordToWorld(snapshot.GridPosition));
            }
        }

        private Vector2Int ResolveInitialCoord(UnitController unit)
        {
            if (unit == null)
            {
                return Vector2Int.zero;
            }

            if (gridManager != null)
            {
                return gridManager.WorldToCoord(unit.transform.position);
            }

            return new Vector2Int(
                Mathf.RoundToInt(unit.transform.position.x),
                Mathf.RoundToInt(unit.transform.position.z));
        }

        private void RegisterSnapshotUnits()
        {
            foreach (var snapshot in initialSnapshots)
            {
                TryRegisterUnit(snapshot.Unit, snapshot.GridPosition);
            }
        }

        private void RegisterRuntimeReinforcements()
        {
            foreach (var reinforcement in runtimeReinforcements)
            {
                if (reinforcement == null)
                {
                    continue;
                }

                TryRegisterUnit(reinforcement, reinforcement.GridPosition);
            }
        }

        private bool TryRegisterUnit(UnitController unit, Vector2Int coord)
        {
            if (unit == null)
            {
                return false;
            }

            if (gridManager != null && !gridManager.TryPlaceUnit(unit, coord))
            {
                return false;
            }

            turnManager?.RegisterUnit(unit);
            return true;
        }

        private void SpawnReinforcementIfNeeded()
        {
            var enemyCount = 0;
            UnitSpawnSnapshot enemyTemplate = null;
            foreach (var snapshot in initialSnapshots)
            {
                if (snapshot.Unit != null && snapshot.Unit.Team == TeamType.Enemy)
                {
                    enemyCount++;
                    enemyTemplate ??= snapshot;
                }
            }

            if (enemyCount >= 2 || enemyTemplate == null)
            {
                return;
            }

            var reinforcementCoord = FindReinforcementCoord(enemyTemplate.GridPosition);
            var reinforcementObject = Instantiate(enemyTemplate.Unit.gameObject);
            reinforcementObject.name = $"{enemyTemplate.Unit.DisplayName} Reinforcement";
            var reinforcementUnit = reinforcementObject.GetComponent<UnitController>();
            reinforcementUnit.ConfigureRuntime($"{enemyTemplate.Unit.DisplayName} Reinforcement", TeamType.Enemy, Mathf.Max(2, enemyTemplate.Unit.MaxHp - 1), enemyTemplate.Unit.AttackPower, enemyTemplate.Unit.MoveRange);
            reinforcementUnit.ResetForBattle(reinforcementCoord, gridManager.CoordToWorld(reinforcementCoord));
            runtimeReinforcements.Add(reinforcementUnit);
        }

        private Vector2Int FindReinforcementCoord(Vector2Int origin)
        {
            var candidates = new[]
            {
                origin + Vector2Int.left,
                origin + Vector2Int.right,
                origin + Vector2Int.up,
                origin + Vector2Int.down,
                origin + new Vector2Int(-1, -1),
                origin + new Vector2Int(1, 1),
            };

            foreach (var candidate in candidates)
            {
                if (gridManager.IsInBounds(candidate) && gridManager.GetTile(candidate) != null && !gridManager.GetTile(candidate).IsOccupied)
                {
                    return candidate;
                }
            }

            return new Vector2Int(Mathf.Max(0, origin.x - 1), origin.y);
        }

        private void ClearRuntimeReinforcements()
        {
            for (var i = runtimeReinforcements.Count - 1; i >= 0; i--)
            {
                var unit = runtimeReinforcements[i];
                if (unit != null)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(unit.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(unit.gameObject);
                    }
                }
            }

            runtimeReinforcements.Clear();
        }

        private sealed class UnitSpawnSnapshot
        {
            public UnitSpawnSnapshot(UnitController unit, Vector2Int gridPosition)
            {
                Unit = unit;
                GridPosition = gridPosition;
            }

            public UnitController Unit { get; }
            public Vector2Int GridPosition { get; }
        }
    }
}
