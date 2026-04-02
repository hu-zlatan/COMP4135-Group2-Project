using UnityEngine;

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
        [SerializeField] private UnitController[] sceneUnits;

        private void Awake()
        {
            gridManager ??= FindFirstObjectByType<GridManager>();
            turnManager ??= FindFirstObjectByType<TurnManager>();
            deckManager ??= FindFirstObjectByType<DeckManager>();
            cardResolver ??= FindFirstObjectByType<CardResolver>();
            enemyAI ??= FindFirstObjectByType<EnemyAI>();
            battleUI ??= FindFirstObjectByType<BattleUI>();

            if (sceneUnits == null || sceneUnits.Length == 0)
            {
                sceneUnits = FindObjectsByType<UnitController>(FindObjectsSortMode.None);
            }
        }

        private void Start()
        {
            gridManager?.GenerateGrid();
            cardResolver?.Initialize(gridManager);
            turnManager?.Initialize(deckManager, enemyAI, gridManager);

            foreach (var unit in sceneUnits)
            {
                if (unit == null)
                {
                    continue;
                }

                turnManager?.RegisterUnit(unit);
                var startCoord = new Vector2Int(Mathf.RoundToInt(unit.transform.position.x), Mathf.RoundToInt(unit.transform.position.z));
                gridManager?.TryPlaceUnit(unit, startCoord);
            }

            battleUI?.Initialize(turnManager, deckManager, cardResolver, gridManager);
            turnManager?.StartBattle();
            battleUI?.Refresh();
        }
    }
}
