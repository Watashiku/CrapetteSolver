using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.GameRules;

public class CrapetteMoveValidator
{
    public static bool ValidateMove(CrapetteGameState gameState, Card cardToMove, Pile sourcePile, Pile destinationPile)
    {
        if (sourcePile.IsEmpty || !sourcePile.GetTopCard()!.Equals(cardToMove))
        {
            return false;
        }

        return destinationPile switch
        {
            TableauPile descendingPile => descendingPile.CanPlaceCard(cardToMove),
            FoundationPile ascendingPile => ascendingPile.CanPlaceCard(cardToMove),
            DiscardPile discardPile => IsOwnDiscardPile(gameState, discardPile, sourcePile)
                                    || discardPile.CanPlaceCardFromOpponent(cardToMove),
            CrapettePile => false,
            DrawPile => false,
            _ => throw new InvalidOperationException("Unknown destination pile type.")
        };
    }

    private static bool IsOwnDiscardPile(CrapetteGameState gameState, DiscardPile discardPile, Pile sourcePile) =>
        (gameState.Player1.DiscardPile == discardPile && gameState.Player1.DrawPile == sourcePile)
     || (gameState.Player2.DiscardPile == discardPile && gameState.Player2.DrawPile == sourcePile);
}