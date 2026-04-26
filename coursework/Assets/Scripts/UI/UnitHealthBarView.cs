using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards
{
    public class UnitHealthBarView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image frameImage;
        [SerializeField] private Image fillImage;
        [SerializeField] private Text nameText;
        [SerializeField] private Text valueText;

        private UnitHudSnapshot snapshot;

        public void Configure(
            RectTransform runtimeRectTransform,
            Image runtimeFrameImage,
            Image runtimeFillImage,
            Text runtimeNameText,
            Text runtimeValueText)
        {
            rectTransform = runtimeRectTransform;
            frameImage = runtimeFrameImage;
            fillImage = runtimeFillImage;
            nameText = runtimeNameText;
            valueText = runtimeValueText;
        }

        public void Bind(UnitHudSnapshot runtimeSnapshot)
        {
            snapshot = runtimeSnapshot;
            if (snapshot == null)
            {
                gameObject.SetActive(false);
                return;
            }

            gameObject.SetActive(true);
            if (nameText != null)
            {
                nameText.text = snapshot.DisplayName;
                UiThemeResources.ApplyTextStyle(nameText, UiThemeResources.BrightTextColor);
            }

            if (valueText != null)
            {
                valueText.text = $"{snapshot.CurrentHp}/{snapshot.MaxHp}";
                UiThemeResources.ApplyTextStyle(valueText, UiThemeResources.BrightTextColor);
            }

            if (frameImage != null)
            {
                frameImage.color = snapshot.IsSelected
                    ? new Color(0.88f, 0.74f, 0.29f, 0.96f)
                    : new Color(0.08f, 0.08f, 0.10f, 0.84f);
            }

            if (fillImage != null)
            {
                fillImage.fillAmount = Mathf.Clamp01(snapshot.HealthRatio);
                fillImage.color = snapshot.Team == TeamType.Player
                    ? new Color(0.22f, 0.68f, 0.43f, 1f)
                    : new Color(0.78f, 0.28f, 0.24f, 1f);
            }
        }

        public void UpdatePosition(RectTransform container, Camera worldCamera)
        {
            if (snapshot == null || rectTransform == null || container == null || worldCamera == null)
            {
                return;
            }

            var screenPosition = worldCamera.WorldToScreenPoint(snapshot.WorldPosition);
            if (screenPosition.z <= 0f)
            {
                rectTransform.gameObject.SetActive(false);
                return;
            }

            rectTransform.gameObject.SetActive(true);
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(container, screenPosition, null, out var localPoint))
            {
                rectTransform.anchoredPosition = localPoint + new Vector2(0f, 18f);
            }
        }
    }
}
