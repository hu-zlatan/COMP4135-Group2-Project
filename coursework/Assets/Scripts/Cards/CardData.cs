using UnityEngine;

namespace TacticalCards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Tactical Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private string cardName;
        [SerializeField] private CardType cardType;
        [SerializeField] private TargetType targetType;
        [SerializeField] private int range = 1;
        [SerializeField] private int power = 1;
        [SerializeField] private int moveDistance = 1;
        [SerializeField] [TextArea] private string description;

        public string Id => id;
        public string CardName => cardName;
        public CardType CardType => cardType;
        public TargetType TargetType => targetType;
        public int Range => range;
        public int Power => power;
        public int MoveDistance => moveDistance;
        public string Description => description;

        public static CardData CreateRuntime(
            string newId,
            string newName,
            CardType newCardType,
            TargetType newTargetType,
            int newRange,
            int newPower,
            int newMoveDistance,
            string newDescription)
        {
            var card = CreateInstance<CardData>();
            card.id = newId;
            card.cardName = newName;
            card.cardType = newCardType;
            card.targetType = newTargetType;
            card.range = newRange;
            card.power = newPower;
            card.moveDistance = newMoveDistance;
            card.description = newDescription;
            return card;
        }
    }
}

