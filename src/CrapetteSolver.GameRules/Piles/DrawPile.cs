using CrapetteSolver.Core;

namespace CrapetteSolver.GameRules.Piles;

public class DrawPile : Pile
{
    public DrawPile(IEnumerable<Card> initialCards) : base() => cards.AddRange(initialCards);

    public void ReconstituteFromCards(IEnumerable<Card> discardCards)
    {
        if (!IsEmpty)
        {
            throw new InvalidOperationException("Cannot reconstitute a non-empty draw pile.");
        }

        cards.AddRange(discardCards);
    }
}