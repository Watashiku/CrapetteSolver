namespace CrapetteSolver.Core;

public class DefaultRandomService : IRandomService
{
    private readonly Random _random = new();

    public int Next(int maxValue) => _random.Next(maxValue);
}