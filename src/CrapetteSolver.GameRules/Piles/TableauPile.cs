using CrapetteSolver.Core;

namespace CrapetteSolver.GameRules.Piles;

public class TableauPile : Pile
{
    public TableauPile() : base() { }

    public bool CanPlaceCard(Card cardToPlay)
    {
        var topCard = GetTopCard();
        if (topCard is null)
        {
            return true;
        }
        else
        {
            var isNextRank = cardToPlay.Rank == topCard.Rank - 1;

            var isAlternatingColor = (cardToPlay.Suit.IsRed() && topCard.Suit.IsBlack()) ||
                                     (cardToPlay.Suit.IsBlack() && topCard.Suit.IsRed());

            return isNextRank && isAlternatingColor;
        }
    }
}