using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public class CardButtonView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        [SerializeField] private Image iconImage;
        [SerializeField] private Text titleText;
        [SerializeField] private Text bodyText;
        [SerializeField] private Text statsText;

        private CardData boundCard;
        private BattleUI battleUI;

        public void Configure(
            Button runtimeButton,
            Image runtimeBackground,
            Image runtimeIconImage,
            Text runtimeTitleText,
            Text runtimeBodyText,
            Text runtimeStatsText)
        {
            button = runtimeButton;
            background = runtimeBackground;
            iconImage = runtimeIconImage;
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
                background.color = Color.Lerp(Color.white, snapshot.Tint, 0.35f);
                background.sprite = UiThemeResources.GetSprite(UiThemeResources.Paths.CardPanel);
                background.type = background.sprite == null ? Image.Type.Simple : Image.Type.Sliced;
            }

            if (iconImage != null)
            {
                iconImage.sprite = snapshot.Card == null ? null : UiThemeResources.GetCardIcon(snapshot.Card.CardType);
                iconImage.color = UiThemeResources.IconTint;
                iconImage.enabled = iconImage.sprite != null;
            }

            if (titleText != null)
            {
                titleText.text = snapshot.Title;
                UiThemeResources.ApplyTextStyle(titleText, UiThemeResources.InkColor);
            }

            if (bodyText != null)
            {
                bodyText.text = $"{snapshot.Subtitle}\n{snapshot.Description}";
                UiThemeResources.ApplyTextStyle(bodyText, UiThemeResources.InkColor);
            }

            if (statsText != null)
            {
                statsText.text = snapshot.Stats;
                UiThemeResources.ApplyTextStyle(statsText, new Color(0.27f, 0.18f, 0.11f, 1f));
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
