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
        [SerializeField] private GameObject disabledOverlay;
        [SerializeField] private Text disabledText;

        private CardData boundCard;
        private BattleUI battleUI;

        public void Configure(
            Button runtimeButton,
            Image runtimeBackground,
            Image runtimeIconImage,
            Text runtimeTitleText,
            Text runtimeBodyText,
            Text runtimeStatsText,
            GameObject runtimeDisabledOverlay,
            Text runtimeDisabledText)
        {
            button = runtimeButton;
            background = runtimeBackground;
            iconImage = runtimeIconImage;
            titleText = runtimeTitleText;
            bodyText = runtimeBodyText;
            statsText = runtimeStatsText;
            disabledOverlay = runtimeDisabledOverlay;
            disabledText = runtimeDisabledText;
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
                background.color = snapshot.IsPlayable
                    ? Color.Lerp(Color.white, snapshot.Tint, snapshot.IsSelected ? 0.56f : 0.35f)
                    : Color.Lerp(snapshot.Tint, Color.black, 0.42f);
                background.sprite = UiThemeResources.GetSprite(UiThemeResources.Paths.CardPanel);
                background.type = background.sprite == null ? Image.Type.Simple : Image.Type.Sliced;
            }

            if (iconImage != null)
            {
                iconImage.sprite = snapshot.Card == null ? null : UiThemeResources.GetCardIcon(snapshot.Card.CardType);
                iconImage.color = snapshot.IsPlayable
                    ? UiThemeResources.IconTint
                    : new Color(0.70f, 0.67f, 0.61f, 0.95f);
                iconImage.enabled = iconImage.sprite != null;
            }

            if (titleText != null)
            {
                titleText.text = snapshot.Title;
                UiThemeResources.ApplyTextStyle(titleText, snapshot.IsPlayable ? UiThemeResources.InkColor : UiThemeResources.MutedTextColor);
            }

            if (bodyText != null)
            {
                bodyText.text = $"{snapshot.Subtitle}\n{snapshot.Description}";
                UiThemeResources.ApplyTextStyle(bodyText, snapshot.IsPlayable ? UiThemeResources.InkColor : UiThemeResources.MutedTextColor, withShadow: false);
            }

            if (statsText != null)
            {
                statsText.text = snapshot.Stats;
                UiThemeResources.ApplyTextStyle(
                    statsText,
                    snapshot.IsPlayable ? new Color(0.27f, 0.18f, 0.11f, 1f) : UiThemeResources.MutedTextColor,
                    withShadow: false);
            }

            if (disabledOverlay != null)
            {
                disabledOverlay.SetActive(!snapshot.IsPlayable && !string.IsNullOrWhiteSpace(snapshot.DisabledReason));
            }

            if (disabledText != null)
            {
                disabledText.text = snapshot.DisabledReason;
                UiThemeResources.ApplyTextStyle(disabledText, UiThemeResources.BrightTextColor);
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
