using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.GameRules;

public record CrapetteBoard(FoundationPile[] FoundationPiles, TableauPile[] TableauPiles);
