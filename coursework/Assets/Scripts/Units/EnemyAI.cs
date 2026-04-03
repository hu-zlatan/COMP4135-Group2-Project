using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class EnemyAI : MonoBehaviour
    {
        public UnitController FindNearestPlayer(UnitController enemy, List<UnitController> playerUnits)
        {
            UnitController bestTarget = null;
            var bestDistance = int.MaxValue;

            foreach (var player in playerUnits)
            {
                if (player == null || !player.IsAlive)
                {
                    continue;
                }

                var distance = ManhattanDistance(enemy.GridPosition, player.GridPosition);
                if (distance < bestDistance)
                {
                    bestDistance = distance;
                    bestTarget = player;
                }
            }

            return bestTarget;
        }

        public Vector2Int GetStepToward(UnitController enemy, UnitController target)
        {
            var delta = target.GridPosition - enemy.GridPosition;

            if (Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
            {
                return new Vector2Int(Mathf.Clamp(delta.x, -1, 1), 0);
            }

            return new Vector2Int(0, Mathf.Clamp(delta.y, -1, 1));
        }

        public int ManhattanDistance(Vector2Int a, Vector2Int b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}
