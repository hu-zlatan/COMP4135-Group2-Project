using System.Collections.Generic;
using UnityEngine;

namespace TacticalCards
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private List<CardData> startingDeck = new();
        [SerializeField] private int cardsDrawnPerTurn = 3;

        private readonly List<CardData> drawPile = new();
        private readonly List<CardData> hand = new();
        private readonly List<CardData> discardPile = new();

        public IReadOnlyList<CardData> Hand => hand;
        public int CardsDrawnPerTurn => cardsDrawnPerTurn;

        private void Awake()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            drawPile.Clear();
            hand.Clear();
            discardPile.Clear();
            if (startingDeck.Count == 0)
            {
                drawPile.AddRange(CreateFallbackDeck());
            }
            else
            {
                drawPile.AddRange(startingDeck);
            }

            Shuffle(drawPile);
        }

        public void DrawForTurn()
        {
            Draw(cardsDrawnPerTurn);
        }

        public void Draw(int count)
        {
            for (var i = 0; i < count; i++)
            {
                if (drawPile.Count == 0)
                {
                    ReshuffleDiscardIntoDrawPile();
                }

                if (drawPile.Count == 0)
                {
                    return;
                }

                var card = drawPile[^1];
                drawPile.RemoveAt(drawPile.Count - 1);
                hand.Add(card);
            }
        }

        public void DiscardFromHand(CardData card)
        {
            if (hand.Remove(card))
            {
                discardPile.Add(card);
            }
        }

        public void DiscardHand()
        {
            while (hand.Count > 0)
            {
                var card = hand[^1];
                hand.RemoveAt(hand.Count - 1);
                discardPile.Add(card);
            }
        }

        private void ReshuffleDiscardIntoDrawPile()
        {
            if (discardPile.Count == 0)
            {
                return;
            }

            drawPile.AddRange(discardPile);
            discardPile.Clear();
            Shuffle(drawPile);
        }

        private void Shuffle(List<CardData> cards)
        {
            for (var i = cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        private List<CardData> CreateFallbackDeck()
        {
            return new List<CardData>
            {
                CardData.CreateRuntime("move_01", "Move", CardType.Move, TargetType.Tile, 1, 0, 2, "Move up to 2 tiles."),
                CardData.CreateRuntime("move_02", "Move", CardType.Move, TargetType.Tile, 1, 0, 2, "Move up to 2 tiles."),
                CardData.CreateRuntime("move_03", "Move", CardType.Move, TargetType.Tile, 1, 0, 2, "Move up to 2 tiles."),
                CardData.CreateRuntime("strike_01", "Strike", CardType.Strike, TargetType.EnemyUnit, 1, 2, 0, "Deal 2 damage to an adjacent enemy."),
                CardData.CreateRuntime("strike_02", "Strike", CardType.Strike, TargetType.EnemyUnit, 1, 2, 0, "Deal 2 damage to an adjacent enemy."),
                CardData.CreateRuntime("guard_01", "Guard", CardType.Guard, TargetType.Self, 0, 0, 0, "Reduce the next incoming damage by 1."),
                CardData.CreateRuntime("push_01", "Push", CardType.Push, TargetType.EnemyUnit, 1, 1, 0, "Deal 1 damage and push the target back."),
            };
        }
    }
}
