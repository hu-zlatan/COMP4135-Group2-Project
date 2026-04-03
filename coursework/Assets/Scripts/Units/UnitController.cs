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
        }

        public void SetGridPosition(Vector2Int coord, Vector3 worldPosition)
        {
            GridPosition = coord;
            transform.position = worldPosition + new Vector3(0f, visualHeightOffset, 0f);
        }

        public void SetGuarding(bool isGuarding)
        {
            IsGuarding = isGuarding;
        }

        public void TakeDamage(int amount)
        {
            var finalDamage = IsGuarding ? Mathf.Max(0, amount - 1) : amount;
            CurrentHp = Mathf.Max(0, CurrentHp - finalDamage);
            IsGuarding = false;

            if (CurrentHp <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        public void Heal(int amount)
        {
            CurrentHp = Mathf.Min(maxHp, CurrentHp + amount);
        }
    }
}

