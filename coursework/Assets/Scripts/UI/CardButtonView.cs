using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public class CardButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;

        private CardData boundCard;
        private BattleUI battleUI;

        public void Bind(CardData card, BattleUI owner)
        {
            boundCard = card;
            battleUI = owner;

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            battleUI?.SelectCard(boundCard);
        }
    }
}
