using CrapetteSolver.Core;

namespace CrapetteSolver.CommonPiles;

public class CardEventArgs(Card card) : EventArgs
{
    public Card Card { get; } = card;
}
