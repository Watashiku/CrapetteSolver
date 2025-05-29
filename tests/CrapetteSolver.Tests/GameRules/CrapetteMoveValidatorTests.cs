using CrapetteSolver.Core;
using CrapetteSolver.GameRules;
using CrapetteSolver.GameRules.Piles;

namespace CrapetteSolver.Tests.GameRules;

public class CrapetteMoveValidatorTests
{
    private readonly CrapetteGameState _initialGameState;

    public CrapetteMoveValidatorTests()
    {
        var foundationPiles = new FoundationPile[8];
        var tableauPiles = new TableauPile[8];
        for (var i = 0; i < foundationPiles.Length; i++)
        {
            foundationPiles[i] = new FoundationPile();
            tableauPiles[i] = new TableauPile();
            tableauPiles[i].AddCard(new Card((Rank)i + 2, Suit.Clubs)); // 2 to 9
        }

        _initialGameState = new CrapetteGameState(
            new CrapettePlayer(1,
                new CrapettePile([new Card(Rank.Ten, Suit.Hearts)]),
                new DrawPile([new Card(Rank.King, Suit.Spades)]),
                new DiscardPile()),
            new CrapettePlayer(2,
                new CrapettePile([new Card(Rank.Eight, Suit.Spades)]),
                new DrawPile([new Card(Rank.Queen, Suit.Spades)]),
                new DiscardPile()),
            new CrapetteBoard(
                foundationPiles,
                tableauPiles));
    }

    [Fact]
    public void ValidateMove_SourcePileIsEmpty_ShouldReturnFalse()
    {
        // Arrange
        var emptyDrawPile = new DrawPile([]);
        var card = new Card(Rank.Ace, Suit.Clubs);

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, emptyDrawPile, _initialGameState.Player1.DiscardPile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_CardToMoveIsNotTopCard_ShouldReturnFalse()
    {
        // Arrange
        var bottomCard = new Card(Rank.Two, Suit.Hearts);
        var topCard = new Card(Rank.Three, Suit.Hearts);
        var drawPile = new DrawPile([bottomCard, topCard]);

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, bottomCard, drawPile, _initialGameState.Player1.DiscardPile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_CardToMoveIsTopCard_ShouldReturnTrueForValidDestination()
    {
        // Arrange
        var player1DrawPile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        player1DrawPile.AddCard(card);
        var player1DiscardPile = _initialGameState.Player1.DiscardPile;

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, player1DrawPile, player1DiscardPile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateMove_ToOwnDiscardPileFromOwnDrawPile_ShouldReturnTrue()
    {
        // Arrange
        var player1DrawPile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        player1DrawPile.AddCard(card);
        var player1DiscardPile = _initialGameState.Player1.DiscardPile;

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, player1DrawPile, player1DiscardPile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateMove_ToOpponentDiscardPile_CanPlaceCardFromOpponentReturnsTrue_ShouldReturnTrue()
    {
        // Arrange
        var player1DrawPile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        player1DrawPile.AddCard(card);
        var player2DiscardPile = _initialGameState.Player2.DiscardPile;
        player2DiscardPile.AddCard(new(Rank.Ten, Suit.Spades));

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, player1DrawPile, player2DiscardPile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateMove_ToOpponentDiscardPile_CanPlaceCardFromOpponentReturnsFalse_ShouldReturnFalse()
    {
        // Arrange
        var player1DrawPile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        player1DrawPile.AddCard(card);
        var player2DiscardPile = _initialGameState.Player2.DiscardPile;
        player2DiscardPile.Clear();

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, player1DrawPile, player2DiscardPile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_ToTableauPile_CanPlaceCardReturnsTrue_ShouldReturnTrue()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        sourcePile.AddCard(card);

        var targetTableauPile = _initialGameState.Board.TableauPiles[0];
        var card2 = new Card(Rank.Queen, Suit.Hearts);
        targetTableauPile.AddCard(card2);

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, targetTableauPile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateMove_ToTableauPile_CanPlaceCardReturnsFalse_ShouldReturnFalse()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        sourcePile.AddCard(card);
        var targetTableauPile = _initialGameState.Board.TableauPiles[0];
        targetTableauPile.AddCard(new Card(Rank.Two, Suit.Diamonds));

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, targetTableauPile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_ToFoundationPile_CanPlaceCardReturnsTrue_ShouldReturnTrue()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Ace, Suit.Clubs);
        sourcePile.AddCard(card);

        var targetFoundationPile = _initialGameState.Board.FoundationPiles[0];

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, targetFoundationPile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateMove_ToFoundationPile_CanPlaceCardReturnsFalse_ShouldReturnFalse()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Two, Suit.Hearts);
        sourcePile.AddCard(card);

        var targetFoundationPile = _initialGameState.Board.FoundationPiles[0];
        targetFoundationPile.AddCard(new Card(Rank.Ace, Suit.Clubs));

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, targetFoundationPile);

        // Assert
        Assert.False(result);
    }

    // --- Tests pour les types de piles non autorisés en destination ---

    [Fact]
    public void ValidateMove_ToCrapettePile_ShouldReturnFalse()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        sourcePile.AddCard(card);
        var crapettePile = _initialGameState.Player1.CrapettePile;

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, crapettePile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_ToDrawPile_ShouldReturnFalse()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DiscardPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        sourcePile.AddCard(card);
        var drawPile = _initialGameState.Player1.DrawPile;

        // Act
        var result = CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, drawPile);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ValidateMove_UnknownDestinationPileType_ShouldThrowException()
    {
        // Arrange
        var sourcePile = _initialGameState.Player1.DrawPile;
        var card = new Card(Rank.Jack, Suit.Spades);
        sourcePile.AddCard(card);
        var unknownPile = new UnknownPileType();

        // Act & Assert
        _ = Assert.Throws<InvalidOperationException>(() =>
            CrapetteMoveValidator.ValidateMove(_initialGameState, card, sourcePile, unknownPile));
    }
}

public class UnknownPileType : Pile { }