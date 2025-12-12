using NaughtyAttributes;
using Puzzle15;
using UnityEngine;
using Zenject;

public class UpPanelTest : MonoBehaviour
{
    private Stopwatch _stopwatch;
    private Counter _counter;

    [Inject]
    public void Construct(Stopwatch stopWatch, Counter counter)
    {
        _stopwatch = stopWatch;
        _counter = counter;
    }

    [Button]
    public void StopwatchStart()
    {
        _stopwatch?.Start();
    }

    [Button]
    public void StopwatchStop()
    {
        _stopwatch?.Stop();
    }

    [Button]
    public void StopwatchRestart()
    {
        _stopwatch?.Restart();
    }

    [Button]
    public void StopwatchReset()
    {
        _stopwatch?.Reset();
    }

    [Button]
    public void CounterIncrease()
    {
        _counter?.Increase();
    }

    [Button]
    public void CounterReset()
    {
        _counter?.Reset();
    }
}