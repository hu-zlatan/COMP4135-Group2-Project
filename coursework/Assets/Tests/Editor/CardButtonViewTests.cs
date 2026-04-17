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
    }
}
