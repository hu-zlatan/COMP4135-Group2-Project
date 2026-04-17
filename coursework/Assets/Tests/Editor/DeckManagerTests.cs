using System.Collections.Generic;
using NUnit.Framework;

namespace TacticalCards.Tests.Editor
{
    public class DeckManagerTests
    {
        [Test]
        public void ResetDeck_WithoutStartingDeck_UsesFallbackDeck()
        {
            using var scope = new TestObjectScope();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);

            var drawPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "drawPile");
            var discardPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "discardPile");

            Assert.That(drawPile.Count, Is.EqualTo(7));
            Assert.That(deckManager.Hand.Count, Is.EqualTo(0));
            Assert.That(discardPile.Count, Is.EqualTo(0));
            Assert.That(deckManager.CardsDrawnPerTurn, Is.EqualTo(3));
        }

        [Test]
        public void DrawForTurn_DrawsConfiguredNumberOfCards()
        {
            using var scope = new TestObjectScope();
            var cards = new List<CardData>
            {
                EditorTestSupport.CreateCard(scope, "c1", "Move", CardType.Move, TargetType.Tile),
                EditorTestSupport.CreateCard(scope, "c2", "Strike", CardType.Strike, TargetType.EnemyUnit),
                EditorTestSupport.CreateCard(scope, "c3", "Guard", CardType.Guard, TargetType.Self),
                EditorTestSupport.CreateCard(scope, "c4", "Push", CardType.Push, TargetType.EnemyUnit),
                EditorTestSupport.CreateCard(scope, "c5", "Move", CardType.Move, TargetType.Tile),
            };

            var deckManager = EditorTestSupport.CreateDeckManager(scope, cards, cardsDrawnPerTurn: 3);
            deckManager.DrawForTurn();

            var drawPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "drawPile");
            Assert.That(deckManager.Hand.Count, Is.EqualTo(3));
            Assert.That(drawPile.Count, Is.EqualTo(2));
        }

        [Test]
        public void DiscardFromHand_MovesCardToDiscardPile()
        {
            using var scope = new TestObjectScope();
            var cards = new List<CardData>
            {
                EditorTestSupport.CreateCard(scope, "c1", "Move", CardType.Move, TargetType.Tile),
                EditorTestSupport.CreateCard(scope, "c2", "Strike", CardType.Strike, TargetType.EnemyUnit),
                EditorTestSupport.CreateCard(scope, "c3", "Guard", CardType.Guard, TargetType.Self),
            };

            var deckManager = EditorTestSupport.CreateDeckManager(scope, cards, cardsDrawnPerTurn: 2);
            deckManager.DrawForTurn();

            var card = deckManager.Hand[0];
            deckManager.DiscardFromHand(card);

            var discardPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "discardPile");
            Assert.That(deckManager.Hand.Count, Is.EqualTo(1));
            Assert.That(discardPile, Does.Contain(card));
        }

        [Test]
        public void DiscardHand_MovesAllCardsToDiscardPile()
        {
            using var scope = new TestObjectScope();
            var cards = new List<CardData>
            {
                EditorTestSupport.CreateCard(scope, "c1", "Move", CardType.Move, TargetType.Tile),
                EditorTestSupport.CreateCard(scope, "c2", "Strike", CardType.Strike, TargetType.EnemyUnit),
                EditorTestSupport.CreateCard(scope, "c3", "Guard", CardType.Guard, TargetType.Self),
            };

            var deckManager = EditorTestSupport.CreateDeckManager(scope, cards, cardsDrawnPerTurn: 2);
            deckManager.DrawForTurn();
            deckManager.DiscardHand();

            var discardPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "discardPile");
            Assert.That(deckManager.Hand.Count, Is.EqualTo(0));
            Assert.That(discardPile.Count, Is.EqualTo(2));
        }

        [Test]
        public void Draw_WhenDrawPileEmpty_ReshufflesDiscardPile()
        {
            using var scope = new TestObjectScope();
            var onlyCard = EditorTestSupport.CreateCard(scope, "c1", "Move", CardType.Move, TargetType.Tile);
            var deckManager = EditorTestSupport.CreateDeckManager(scope, new List<CardData> { onlyCard }, cardsDrawnPerTurn: 1);

            deckManager.Draw(1);
            deckManager.DiscardHand();
            deckManager.Draw(1);

            var discardPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "discardPile");
            Assert.That(deckManager.Hand.Count, Is.EqualTo(1));
            Assert.That(discardPile.Count, Is.EqualTo(0));
        }

        [Test]
        public void Draw_WhenNoCardsRemain_LeavesHandUnchanged()
        {
            using var scope = new TestObjectScope();
            var deckManager = EditorTestSupport.CreateDeckManager(scope);
            var drawPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "drawPile");
            var discardPile = EditorTestSupport.GetPrivateField<List<CardData>>(deckManager, "discardPile");

            drawPile.Clear();
            discardPile.Clear();
            deckManager.Draw(2);

            Assert.That(deckManager.Hand.Count, Is.EqualTo(0));
            Assert.That(drawPile.Count, Is.EqualTo(0));
        }
    }
}
