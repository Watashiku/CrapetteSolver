using CrapetteSolver.Core;

namespace CrapetteSolver.Tests.Core;

public class CardTests
{
    [Fact]
    public void Card_ShouldBeCreatedWithCorrectRankAndSuit()
    {
        // Arrange
        var rank = Rank.Ace;
        var suit = Suit.Spades;

        // Act
        var card = new Card(rank, suit);

        // Assert
        Assert.Equal(rank, card.Rank);
        Assert.Equal(suit, card.Suit);
    }

    [Fact]
    public void Cards_WithSameRankAndSuit_ShouldBeEqual()
    {
        // Arrange
        var card1 = new Card(Rank.King, Suit.Hearts);
        var card2 = new Card(Rank.King, Suit.Hearts);

        // Act & Assert
        Assert.Equal(card1, card2);
        Assert.True(card1 == card2);
        Assert.False(card1 != card2);
    }

    [Fact]
    public void Cards_WithDifferentRankOrSuit_ShouldNotBeEqual()
    {
        // Arrange
        var card1 = new Card(Rank.Queen, Suit.Clubs);
        var card2 = new Card(Rank.Queen, Suit.Diamonds); // Different suit
        var card3 = new Card(Rank.Jack, Suit.Clubs);    // Different rank

        // Act & Assert
        Assert.NotEqual(card1, card2);
        Assert.False(card1 == card2);
        Assert.True(card1 != card2);

        Assert.NotEqual(card1, card3);
        Assert.False(card1 == card3);
        Assert.True(card1 != card3);
    }

    [Fact]
    public void ToString_ShouldReturnExpectedFormat()
    {
        // Arrange
        var card = new Card(Rank.Ace, Suit.Spades);

        // Act
        var cardString = card.ToString();

        // Assert
        Assert.Equal("Ace of Spades", cardString);
    }
}