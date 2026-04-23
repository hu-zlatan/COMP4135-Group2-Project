using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public class CardButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        [SerializeField] private Text titleText;
        [SerializeField] private Text bodyText;
        [SerializeField] private Text statsText;

        private CardData boundCard;
        private BattleUI battleUI;

        public void Configure(Button runtimeButton, Image runtimeBackground, Text runtimeTitleText, Text runtimeBodyText, Text runtimeStatsText)
        {
            button = runtimeButton;
            background = runtimeBackground;
            titleText = runtimeTitleText;
            bodyText = runtimeBodyText;
            statsText = runtimeStatsText;
        }

        public void Bind(CardData card, BattleUI owner)
        {
            boundCard = card;
            battleUI = owner;
            button ??= GetComponent<Button>();

            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HandleClick);
            }
        }

        public void Bind(CardHudSnapshot snapshot, BattleUI owner)
        {
            Bind(snapshot.Card, owner);

            if (background != null)
            {
                background.color = snapshot.Tint;
            }

            if (titleText != null)
            {
                titleText.text = snapshot.Title;
                titleText.color = new Color(0.13f, 0.13f, 0.13f);
            }

            if (bodyText != null)
            {
                bodyText.text = $"{snapshot.Subtitle}\n{snapshot.Description}";
                bodyText.color = new Color(0.16f, 0.16f, 0.16f);
            }

            if (statsText != null)
            {
                statsText.text = snapshot.Stats;
            }

            if (button != null)
            {
                button.interactable = snapshot.IsPlayable;
            }
        }

        private void HandleClick()
        {
            battleUI?.SelectCard(boundCard);
        }
    }
}
