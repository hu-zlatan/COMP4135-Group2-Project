using UnityEngine;

namespace TacticalCards
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private string displayName = "Unit";
        [SerializeField] private TeamType team = TeamType.Player;
        [SerializeField] private int maxHp = 10;
        [SerializeField] private int attackPower = 2;
        [SerializeField] private int moveRange = 3;
        [SerializeField] private float visualHeightOffset = 0.6f;
        [SerializeField] private UnitVisualShell visualShell;

        public string DisplayName => displayName;
        public TeamType Team => team;
        public int MaxHp => maxHp;
        public int CurrentHp { get; private set; }
        public int AttackPower => attackPower;
        public int MoveRange => moveRange;
        public bool IsGuarding { get; private set; }
        public bool IsAlive => CurrentHp > 0;
        public Vector2Int GridPosition { get; private set; }

        private void Awake()
        {
            CurrentHp = maxHp;
            EnsureVisualShell();
            RefreshPresentation();
        }

        public void SetGridPosition(Vector2Int coord, Vector3 worldPosition)
        {
            GridPosition = coord;
            transform.position = worldPosition + new Vector3(0f, visualHeightOffset, 0f);
        }

        public void SetGuarding(bool isGuarding)
        {
            IsGuarding = isGuarding;
            RefreshPresentation();
        }

        public void ResetForBattle(Vector2Int coord, Vector3 worldPosition)
        {
            gameObject.SetActive(true);
            CurrentHp = maxHp;
            IsGuarding = false;
            SetGridPosition(coord, worldPosition);
            RefreshPresentation();
        }

        public void ConfigureRuntime(string runtimeDisplayName, TeamType runtimeTeam, int runtimeMaxHp, int runtimeAttackPower, int runtimeMoveRange)
        {
            displayName = runtimeDisplayName;
            team = runtimeTeam;
            maxHp = runtimeMaxHp;
            attackPower = runtimeAttackPower;
            moveRange = runtimeMoveRange;
            CurrentHp = Mathf.Min(CurrentHp > 0 ? CurrentHp : runtimeMaxHp, runtimeMaxHp);
            RefreshPresentation();
        }

        public void TakeDamage(int amount)
        {
            var finalDamage = IsGuarding ? Mathf.Max(0, amount - 1) : amount;
            CurrentHp = Mathf.Max(0, CurrentHp - finalDamage);
            IsGuarding = false;
            RefreshPresentation();

            if (CurrentHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        public void Heal(int amount)
        {
            CurrentHp = Mathf.Min(maxHp, CurrentHp + amount);
            RefreshPresentation();
        }

        private void EnsureVisualShell()
        {
            visualShell ??= GetComponent<UnitVisualShell>();
            if (visualShell == null)
            {
                visualShell = gameObject.AddComponent<UnitVisualShell>();
            }
        }

        private void RefreshPresentation()
        {
            EnsureVisualShell();
            visualShell?.Apply(this);
        }
    }
}
