using CrapetteSolver.Core;

namespace CrapetteSolver.Tests.TestHelpers;

public class TestRandomService(params int[] sequence) : IRandomService
{
    private readonly int[] _sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));
    private int _currentIndex = 0;

    public int Next(int maxValue)
    {
        if (_sequence.Length == 0)
        {
            return 0;
        }

        var value = _sequence[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _sequence.Length;

        return value % maxValue;
    }
}