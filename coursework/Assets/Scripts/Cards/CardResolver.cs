using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class CardResolver : MonoBehaviour
    {
        private GridManager gridManager;

        public void Initialize(GridManager targetGridManager)
        {
            gridManager = targetGridManager;
        }

        public List<TileView> GetValidTiles(CardData card, UnitController caster)
        {
            if (gridManager == null || card == null || caster == null)
            {
                return new List<TileView>();
            }

            return card.CardType switch
            {
                CardType.Move => gridManager.GetTilesInRange(caster.GridPosition, card.MoveDistance, includeOccupied: false),
                CardType.Strike => gridManager.GetTilesInRange(caster.GridPosition, card.Range, includeOccupied: true),
                CardType.Push => gridManager.GetTilesInRange(caster.GridPosition, card.Range, includeOccupied: true),
                _ => new List<TileView>(),
            };
        }

        public bool ResolveTileCard(CardData card, UnitController caster, TileView targetTile)
        {
            if (card == null || caster == null)
            {
                return false;
            }

            return card.CardType switch
            {
                CardType.Move when gridManager != null && targetTile != null => gridManager.TryMoveUnit(caster, targetTile.Coord),
                CardType.Guard => ResolveGuard(caster),
                _ => false,
            };
        }

        public bool ResolveUnitCard(CardData card, UnitController caster, UnitController targetUnit)
        {
            if (gridManager == null || card == null || caster == null || targetUnit == null)
            {
                return false;
            }

            if (!targetUnit.IsAlive || targetUnit.Team == caster.Team)
            {
                return false;
            }

            return card.CardType switch
            {
                CardType.Strike => ResolveStrike(card, targetUnit),
                CardType.Push => ResolvePush(card, caster, targetUnit),
                _ => false,
            };
        }

        private bool ResolveStrike(CardData card, UnitController targetUnit)
        {
            targetUnit.TakeDamage(card.Power);
            ClearTargetOccupancyIfDead(targetUnit);
            return true;
        }

        private bool ResolveGuard(UnitController caster)
        {
            caster.SetGuarding(true);
            return true;
        }

        private bool ResolvePush(CardData card, UnitController caster, UnitController targetUnit)
        {
            targetUnit.TakeDamage(card.Power);
            if (!targetUnit.IsAlive)
            {
                ClearTargetOccupancyIfDead(targetUnit);
                return true;
            }

            var direction = targetUnit.GridPosition - caster.GridPosition;
            direction = new Vector2Int(Mathf.Clamp(direction.x, -1, 1), Mathf.Clamp(direction.y, -1, 1));
            if (direction == Vector2Int.zero)
            {
                return true;
            }

            gridManager.TryPushUnit(targetUnit, direction);
            return true;
        }

        private void ClearTargetOccupancyIfDead(UnitController targetUnit)
        {
            if (!targetUnit.IsAlive)
            {
                gridManager?.ClearOccupancy(targetUnit);
            }
        }
    }
}

