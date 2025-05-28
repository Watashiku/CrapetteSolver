using CrapetteSolver.Core;

namespace CrapetteSolver.GameRules.Piles;

public class DiscardPile : Pile
{
    public DiscardPile() : base() { }

    public bool CanPlaceCardFromOpponent(Card cardFromOpponent)
    {
        var topCard = GetTopCard();
        if (topCard is null)
        {
            return false;
        }

        if (topCard.Suit != cardFromOpponent.Suit)
        {
            return false;
        }

        var rankDiff = Math.Abs((int)topCard.Rank - (int)cardFromOpponent.Rank);
        return rankDiff <= 1;
    }

    public List<Card> ClearAndGetCards()
    {
        var retrievedCards = cards.ToList(); // Deep copy of the current cards
        retrievedCards.Reverse();
        Clear();
        return retrievedCards;
    }
}
