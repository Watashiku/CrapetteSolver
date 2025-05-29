using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.GameRules;

public record CrapettePlayer(int Id, CrapettePile CrapettePile, DrawPile DrawPile, DiscardPile DiscardPile);
