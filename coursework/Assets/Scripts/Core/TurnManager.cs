using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] private int maxCardPlaysPerTurn = 2;

        private DeckManager deckManager;
        private EnemyAI enemyAI;
        private GridManager gridManager;

        private readonly List<UnitController> playerUnits = new();
        private readonly List<UnitController> enemyUnits = new();

        public bool IsPlayerTurn { get; private set; }
        public int RemainingCardPlays { get; private set; }
        public string LastEnemyActionSummary { get; private set; } = "Battle started.";
        public bool BattleEnded { get; private set; }
        public TeamType? WinningTeam { get; private set; }
        public string BattleOutcomeSummary { get; private set; } = string.Empty;

        public void Initialize(DeckManager targetDeckManager, EnemyAI targetEnemyAI, GridManager targetGridManager)
        {
            deckManager = targetDeckManager;
            enemyAI = targetEnemyAI;
            gridManager = targetGridManager;
            IsPlayerTurn = false;
            RemainingCardPlays = 0;
            BattleEnded = false;
            WinningTeam = null;
            BattleOutcomeSummary = string.Empty;
            LastEnemyActionSummary = "Battle started.";
        }

        public void RegisterUnit(UnitController unit)
        {
            if (unit == null)
            {
                return;
            }

            if (unit.Team == TeamType.Player)
            {
                if (!playerUnits.Contains(unit))
                {
                    playerUnits.Add(unit);
                }
            }
            else if (!enemyUnits.Contains(unit))
            {
                enemyUnits.Add(unit);
            }
        }

        public IReadOnlyList<UnitController> PlayerUnits => playerUnits;
        public IReadOnlyList<UnitController> EnemyUnits => enemyUnits;

        public void StartBattle()
        {
            BattleEnded = false;
            WinningTeam = null;
            BattleOutcomeSummary = string.Empty;
            LastEnemyActionSummary = "Battle started.";

            if (!CheckBattleState())
            {
                StartPlayerTurn();
            }
        }

        public void StartPlayerTurn()
        {
            if (BattleEnded)
            {
                IsPlayerTurn = false;
                RemainingCardPlays = 0;
                return;
            }

            IsPlayerTurn = true;
            RemainingCardPlays = maxCardPlaysPerTurn;
            deckManager?.DiscardHand();
            deckManager?.DrawForTurn();
        }

        public bool TryConsumeCardPlay()
        {
            if (BattleEnded || !IsPlayerTurn || RemainingCardPlays <= 0)
            {
                return false;
            }

            RemainingCardPlays--;
            return true;
        }

        public void EndPlayerTurn()
        {
            if (!IsPlayerTurn || BattleEnded)
            {
                return;
            }

            IsPlayerTurn = false;
            RunEnemyTurn();
            if (!CheckBattleState())
            {
                StartPlayerTurn();
            }
        }

        public bool CheckBattleState()
        {
            playerUnits.RemoveAll(unit => unit == null || !unit.IsAlive);
            enemyUnits.RemoveAll(unit => unit == null || !unit.IsAlive);

            if (BattleEnded)
            {
                return true;
            }

            if (enemyUnits.Count == 0)
            {
                EndBattle(TeamType.Player, "Victory! All enemy units defeated.");
                return true;
            }

            if (playerUnits.Count == 0)
            {
                EndBattle(TeamType.Enemy, "Defeat! All player units were defeated.");
                return true;
            }

            return false;
        }

        private void RunEnemyTurn()
        {
            if (BattleEnded)
            {
                return;
            }

            if (enemyAI == null || gridManager == null)
            {
                LastEnemyActionSummary = "Enemy turn skipped.";
                return;
            }

            var summary = new List<string>();

            foreach (var enemy in enemyUnits)
            {
                if (enemy == null || !enemy.IsAlive)
                {
                    continue;
                }

                var target = enemyAI.FindNearestPlayer(enemy, playerUnits);
                if (target == null)
                {
                    continue;
                }

                var distance = enemyAI.ManhattanDistance(enemy.GridPosition, target.GridPosition);
                if (distance <= 1)
                {
                    var previousHp = target.CurrentHp;
                    target.TakeDamage(enemy.AttackPower);
                    var damageDealt = previousHp - target.CurrentHp;
                    summary.Add($"{enemy.DisplayName} hit {target.DisplayName} for {damageDealt}. {target.DisplayName} HP: {target.CurrentHp}/{target.MaxHp}.");
                    if (CheckBattleState())
                    {
                        break;
                    }

                    continue;
                }

                var step = enemyAI.GetStepToward(enemy, target);
                if (gridManager.TryMoveUnit(enemy, enemy.GridPosition + step))
                {
                    summary.Add($"{enemy.DisplayName} moved to {enemy.GridPosition.x},{enemy.GridPosition.y}.");
                }
            }

            if (BattleEnded)
            {
                return;
            }

            LastEnemyActionSummary = summary.Count > 0 ? string.Join(" ", summary) : "Enemy turn ended with no action.";
        }

        private void EndBattle(TeamType winningTeam, string outcomeSummary)
        {
            BattleEnded = true;
            WinningTeam = winningTeam;
            BattleOutcomeSummary = outcomeSummary;
            LastEnemyActionSummary = outcomeSummary;
            IsPlayerTurn = false;
            RemainingCardPlays = 0;
        }
    }
}
