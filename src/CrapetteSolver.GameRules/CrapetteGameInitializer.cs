using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.GameRules;

public class CrapetteGameInitializer(IRandomService randomService)
{
    private readonly IRandomService _randomService = randomService;

    public CrapetteGameState InitializeGame()
    {
        var deck1 = Deck.CreateStandard52CardDeck(_randomService);
        var deck2 = Deck.CreateStandard52CardDeck(_randomService);

        var player1CrapettePile = new CrapettePile(deck1.DrawCards(13));
        var player2CrapettePile = new CrapettePile(deck2.DrawCards(13));

        var mainDescendingPiles = new TableauPile[8];

        for (var i = 0; i < 4; i++)
        {
            var card1 = deck1.DrawCard()!;
            var card2 = deck2.DrawCard()!;

            var pile1 = new TableauPile();
            pile1.AddCard(card1);
            mainDescendingPiles[i] = pile1;

            var pile2 = new TableauPile();
            pile2.AddCard(card2);
            mainDescendingPiles[i + 4] = pile2;
        }

        var player1DrawPile = new DrawPile(deck1.DrawCards(deck1.Count));
        var player2DrawPile = new DrawPile(deck2.DrawCards(deck2.Count));

        var player1DiscardPile = new DiscardPile();
        var player2DiscardPile = new DiscardPile();

        var mainAscendingPiles = new FoundationPile[8];
        for (var i = 0; i < 8; i++)
        {
            mainAscendingPiles[i] = new FoundationPile();
        }

        var player1 = new CrapettePlayer(1, player1CrapettePile, player1DrawPile, player1DiscardPile);
        var player2 = new CrapettePlayer(2, player2CrapettePile, player2DrawPile, player2DiscardPile);

        var board = new CrapetteBoard(mainAscendingPiles, mainDescendingPiles);

        return new CrapetteGameState(player1, player2, board);
    }
}
