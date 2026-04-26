using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace TacticalCards.Tests.Editor
{
    public class CardButtonViewTests
    {
        [Test]
        public void Bind_ClickInvokesBattleUiSelection()
        {
            using var scope = new TestObjectScope();
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var card = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit);
            var buttonObject = scope.Track(new GameObject("CardButton"));
            var button = buttonObject.AddComponent<Button>();
            var cardButtonView = buttonObject.AddComponent<CardButtonView>();

            cardButtonView.Bind(card, battleUi);
            button.onClick.Invoke();

            Assert.That(battleUi.SelectedCard, Is.EqualTo(card));
        }

        [Test]
        public void BindSnapshot_DisabledCard_ShowsOverlayReason()
        {
            using var scope = new TestObjectScope();
            var battleUi = scope.Track(new GameObject("BattleUI")).AddComponent<BattleUI>();
            var card = EditorTestSupport.CreateCard(scope, "strike", "Strike", CardType.Strike, TargetType.EnemyUnit);
            var buttonObject = scope.Track(new GameObject("CardButton"));
            var button = buttonObject.AddComponent<Button>();
            var background = buttonObject.AddComponent<Image>();
            var iconObject = scope.Track(new GameObject("Icon"));
            iconObject.transform.SetParent(buttonObject.transform, false);
            var icon = iconObject.AddComponent<Image>();
            var title = scope.Track(new GameObject("Title")).AddComponent<Text>();
            title.transform.SetParent(buttonObject.transform, false);
            var body = scope.Track(new GameObject("Body")).AddComponent<Text>();
            body.transform.SetParent(buttonObject.transform, false);
            var stats = scope.Track(new GameObject("Stats")).AddComponent<Text>();
            stats.transform.SetParent(buttonObject.transform, false);
            var overlay = scope.Track(new GameObject("Overlay"));
            overlay.transform.SetParent(buttonObject.transform, false);
            var overlayText = scope.Track(new GameObject("OverlayText")).AddComponent<Text>();
            overlayText.transform.SetParent(overlay.transform, false);
            var cardButtonView = buttonObject.AddComponent<CardButtonView>();
            cardButtonView.Configure(button, background, icon, title, body, stats, overlay, overlayText);

            cardButtonView.Bind(new CardHudSnapshot
            {
                Card = card,
                Title = "Strike",
                Subtitle = "Strike",
                Description = "Deal 2 damage",
                Stats = "Range 1  Power 2",
                DisabledReason = "No Energy",
                Tint = Color.white,
                IsPlayable = false,
            }, battleUi);

            Assert.That(overlay.activeSelf, Is.True);
            Assert.That(overlayText.text, Is.EqualTo("No Energy"));
            Assert.That(button.interactable, Is.False);
        }
    }
}
