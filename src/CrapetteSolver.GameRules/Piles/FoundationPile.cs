using CrapetteSolver.Core;

namespace CrapetteSolver.GameRules.Piles;

public class FoundationPile : Pile
{
    public FoundationPile() : base() { }

    public bool CanPlaceCard(Card cardToPlay)
    {
        var topCard = GetTopCard();
        return topCard is null
            ? cardToPlay.Rank == Rank.Ace
            : cardToPlay.Suit == topCard.Suit && (int)cardToPlay.Rank == (int)topCard.Rank + 1;
    }
}