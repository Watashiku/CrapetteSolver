using CrapetteSolver.Core;
using CrapetteSolver.GameRules;
using CrapetteSolver.Tests.TestHelpers;

namespace CrapetteSolver.Tests.GameRules;

public class CrapetteGameInitializerTests
{
    [Fact]
    public void InitializeGame_ShouldCreateGameWithCorrectNumberOfCardsInAllPiles()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var initializer = new CrapetteGameInitializer(randomService);

        // Act
        var gameState = initializer.InitializeGame();

        // Assert
        Assert.Equal(13, gameState.Player1.CrapettePile.Count);
        Assert.Equal(13, gameState.Player2.CrapettePile.Count);

        Assert.Equal(8, gameState.Board.TableauPiles.Length);
        foreach (var pile in gameState.Board.TableauPiles)
        {
            Assert.Equal(1, pile.Count);
        }

        Assert.Equal(35, gameState.Player1.DrawPile.Count);
        Assert.Equal(35, gameState.Player2.DrawPile.Count);

        Assert.Equal(0, gameState.Player1.DiscardPile.Count);
        Assert.Equal(0, gameState.Player2.DiscardPile.Count);

        Assert.Equal(8, gameState.Board.FoundationPiles.Length);
        foreach (var pile in gameState.Board.FoundationPiles)
        {
            Assert.Equal(0, pile.Count);
        }

        var allCardsInPlay = new List<Card>();
        allCardsInPlay.AddRange(gameState.Player1.CrapettePile.Cards);
        allCardsInPlay.AddRange(gameState.Player2.CrapettePile.Cards);
        allCardsInPlay.AddRange(gameState.Board.TableauPiles.SelectMany(p => p.Cards));
        allCardsInPlay.AddRange(gameState.Player1.DrawPile.Cards);
        allCardsInPlay.AddRange(gameState.Player2.DrawPile.Cards);
        allCardsInPlay.AddRange(gameState.Player1.DiscardPile.Cards);
        allCardsInPlay.AddRange(gameState.Player2.DiscardPile.Cards);
        allCardsInPlay.AddRange(gameState.Board.FoundationPiles.SelectMany(p => p.Cards));

        Assert.Equal(104, allCardsInPlay.Count);
    }

    [Fact]
    public void InitializeGame_ShouldDistributeUniqueCardsAcrossAllPiles()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var initializer = new CrapetteGameInitializer(randomService);

        // Act
        var gameState = initializer.InitializeGame();

        // Assert
        var allCards = new List<Card>();
        allCards.AddRange(gameState.Player1.CrapettePile.Cards);
        allCards.AddRange(gameState.Player2.CrapettePile.Cards);
        allCards.AddRange(gameState.Board.TableauPiles.SelectMany(p => p.Cards));
        allCards.AddRange(gameState.Player1.DrawPile.Cards);
        allCards.AddRange(gameState.Player2.DrawPile.Cards);
        var groups = allCards.GroupBy(c => c);

        Assert.Equal(104, allCards.Count);
        Assert.Equal(52, groups.Count());
        Assert.Equal(2, groups.Select(g => g.Count()).Distinct().Single());
    }

    [Fact]
    public void InitializeGame_MainDescendingPilesAreCorrectlyInitializedByPlayers()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var initializer = new CrapetteGameInitializer(randomService);

        // Act
        var gameState = initializer.InitializeGame();

        // Assert
        var cardsFromPlayer1 = new List<Card>();
        cardsFromPlayer1.AddRange(gameState.Player1.CrapettePile.Cards);
        cardsFromPlayer1.AddRange(gameState.Board.TableauPiles.Take(4).SelectMany(p => p.Cards));
        cardsFromPlayer1.AddRange(gameState.Player1.DrawPile.Cards);

        var cardsFromPlayer2 = new List<Card>();
        cardsFromPlayer2.AddRange(gameState.Player2.CrapettePile.Cards);
        cardsFromPlayer2.AddRange(gameState.Board.TableauPiles.Skip(4).SelectMany(p => p.Cards));
        cardsFromPlayer2.AddRange(gameState.Player2.DrawPile.Cards);

        Assert.Equal(52, cardsFromPlayer1.Count);
        Assert.Equal(52, cardsFromPlayer2.Count);
        Assert.Equal(52, cardsFromPlayer1.Distinct().Count());
        Assert.Equal(52, cardsFromPlayer2.Distinct().Count());
    }

    [Fact]
    public void InitializeGame_DiscardPilesAreInitiallyEmpty()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var initializer = new CrapetteGameInitializer(randomService);

        // Act
        var gameState = initializer.InitializeGame();

        // Assert
        Assert.True(gameState.Player1.DiscardPile.IsEmpty);
        Assert.True(gameState.Player2.DiscardPile.IsEmpty);
    }

    [Fact]
    public void InitializeGame_MainAscendingPilesAreInitiallyEmpty()
    {
        // Arrange
        var randomService = new TestRandomService(0);
        var initializer = new CrapetteGameInitializer(randomService);

        // Act
        var gameState = initializer.InitializeGame();

        // Assert
        Assert.Equal(8, gameState.Board.FoundationPiles.Length);
        foreach (var pile in gameState.Board.FoundationPiles)
        {
            Assert.True(pile.IsEmpty);
        }
    }
}