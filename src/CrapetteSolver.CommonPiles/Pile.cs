using CrapetteSolver.Core;

namespace CrapetteSolver.CommonPiles;

public abstract class Pile
{
    protected List<Card> cards = [];

    public event EventHandler<CardEventArgs>? CardAdded;
    public event EventHandler<CardEventArgs>? CardRemoved;
    public event EventHandler? PileCleared;

    public IReadOnlyList<Card> Cards => cards;

    public Card? GetTopCard() => cards.LastOrDefault();

    public bool IsEmpty => cards.Count == 0;
    public int Count => cards.Count;

    public virtual void AddCard(Card card)
    {
        if (card == null)
        {
            return;
        }

        cards.Add(card);
        OnCardAdded(card);
    }

    public virtual Card? RemoveTopCard()
    {
        if (IsEmpty)
        {
            return null;
        }

        var card = cards[^1];
        cards.RemoveAt(cards.Count - 1);
        OnCardRemoved(card);
        return card;
    }

    public virtual void AddCards(IEnumerable<Card> cardsToAdd)
    {
        if (cardsToAdd == null)
        {
            return;
        }

        foreach (var card in cardsToAdd)
        {
            AddCard(card);
        }
    }

    public virtual void Clear()
    {
        if (!IsEmpty)
        {
            cards.Clear();
            OnPileCleared();
        }
    }

    protected virtual void OnCardAdded(Card card) => CardAdded?.Invoke(this, new CardEventArgs(card));

    protected virtual void OnCardRemoved(Card card) => CardRemoved?.Invoke(this, new CardEventArgs(card));

    protected virtual void OnPileCleared() => PileCleared?.Invoke(this, EventArgs.Empty);

}