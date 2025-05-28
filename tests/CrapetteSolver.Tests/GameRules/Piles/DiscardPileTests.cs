using CrapetteSolver.Core;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules.Piles;

public class DiscardPileTests
{
    [Fact]
    public void DiscardPile_ShouldBeInitializedEmpty()
    {
        // Arrange & Act
        var discardPile = new DiscardPile();

        // Assert
        Assert.True(discardPile.IsEmpty);
        Assert.Equal(0, discardPile.Count);
    }

    [Fact]
    public void ClearAndGetCards_ShouldReturnAllCardsInCorrectOrderAndClearPile()
    {
        // Arrange
        var card1 = new Card(Rank.Ace, Suit.Spades);
        var card2 = new Card(Rank.King, Suit.Hearts);
        var discardPile = new DiscardPile();
        discardPile.AddCards([card1, card2]);

        // Act
        var retrievedCards = discardPile.ClearAndGetCards();

        // Assert
        Assert.Equal(2, retrievedCards.Count);
        Assert.Equal([card2, card1], retrievedCards);

        Assert.True(discardPile.IsEmpty);
        Assert.Equal(0, discardPile.Count);
    }

    [Theory]
    // Top Card (déjà sur la pile),     New Card (à placer),     Expected Result
    [InlineData(Rank.Five, Suit.Spades, Rank.Five, Suit.Spades, true)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Six, Suit.Spades, true)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Four, Suit.Spades, true)]

    [InlineData(Rank.Five, Suit.Spades, Rank.Five, Suit.Clubs, false)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Six, Suit.Clubs, false)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Four, Suit.Clubs, false)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Seven, Suit.Spades, false)]
    [InlineData(Rank.Five, Suit.Spades, Rank.Three, Suit.Spades, false)]
    [InlineData(Rank.Ace, Suit.Spades, Rank.King, Suit.Spades, false)]
    [InlineData(Rank.King, Suit.Spades, Rank.Ace, Suit.Spades, false)]
    public void CanPlaceCardFromOpponent_ShouldValidateAccordingToCrapetteRules(
        Rank topCardRank, Suit topCardSuit,
        Rank newCardRank, Suit newCardSuit,
        bool expectedResult)
    {
        // Arrange
        var discardPile = new DiscardPile();
        discardPile.AddCards([new Card(topCardRank, topCardSuit)]);
        var newCard = new Card(newCardRank, newCardSuit);

        // Act
        var canPlace = discardPile.CanPlaceCardFromOpponent(newCard);

        // Assert
        Assert.Equal(expectedResult, canPlace);
    }

    [Fact]
    public void CanPlaceCardFromOpponent_ShouldNotAllowAnyCardIfPileIsEmpty()
    {
        // Arrange
        var discardPile = new DiscardPile();
        var card = new Card(Rank.Queen, Suit.Diamonds);

        // Act
        var canPlace = discardPile.CanPlaceCardFromOpponent(card);

        // Assert
        Assert.False(canPlace);
    }
}