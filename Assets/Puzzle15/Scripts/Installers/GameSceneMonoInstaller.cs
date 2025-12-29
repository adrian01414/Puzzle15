using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GameSceneMonoInstaller : MonoInstaller
    {
        [SerializeField] private Transform _stopwatchSpawnTransform;

        [SerializeField] private GridView _view;
        [SerializeField] private CounterView _counterView;
        [SerializeField] private StopwatchView _stopwatchView;
        [SerializeField] private WinPanelView _winPanelView;

        [Inject] private LevelConfig _levelConfig;

        public override void InstallBindings()
        {
            BindGrid();
            BindStopwatch();
            BindCounter();
            BindWinPanel();
        }

        private void BindWinPanel()
        {
            Container
                .Bind<WinPanelView>()
                .FromInstance(_winPanelView)
                .AsSingle();

            Container
                .BindInterfacesTo<WinPanelPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGrid()
        {
            IGridGenerator gridGenerator = new SimpleGridGenerator();

            Container
                .Bind<PuzzleGrid>()
                .AsSingle()
                .WithArguments(gridGenerator, _levelConfig.GridSize);

            Container
                .Bind<GridView>()
                .FromInstance(_view)
                .AsSingle();

            Container
                .BindInterfacesTo<GridPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStopwatch()
        {
            Container
                .BindInterfacesAndSelfTo<Stopwatch>()
                .AsSingle();

            Container
                .Bind<ISettableFieldView>()
                .FromInstance(_stopwatchView)
                .WhenInjectedInto<StopwatchPresenter>();

            Container
                .BindInterfacesTo<StopwatchPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<StopwatchEventHandler>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCounter()
        {
            Container
                .Bind<Counter>()
                .AsSingle();

            Container
                .Bind<ISettableFieldView>()
                .FromInstance(_counterView)
                .WhenInjectedInto<CounterPresenter>();

            Container
                .BindInterfacesTo<CounterPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<CounterEventHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}

