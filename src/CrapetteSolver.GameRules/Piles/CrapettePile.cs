using CrapetteSolver.Core;

namespace CrapetteSolver.GameRules.Piles;

public class CrapettePile : Pile
{
    public CrapettePile(IEnumerable<Card> initialCards) : base()
    {
        if (initialCards != null)
        {
            cards.AddRange(initialCards);
        }
    }
}