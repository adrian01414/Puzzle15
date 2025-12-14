using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GridView _view;
        [SerializeField] private CounterView _counterView;
        [SerializeField] private StopwatchView _stopWatchView;

        [Inject] private LevelConfigurationManager _levelConfigurationManager;

        public override void InstallBindings()
        {
            BindGrid();
            BindStopWatch();
            BindCounter();
        }

        private void BindGrid()
        {
            IGridGenerator gridGenerator = new SimpleGridGenerator();

            Container
                .Bind<PuzzleGrid>()
                .AsSingle()
                .WithArguments(gridGenerator, _levelConfigurationManager.CurrentConfig.Size);

            Container
                .Bind<GridView>()
                .FromInstance(_view)
                .AsSingle();

            Container
                .BindInterfacesTo<GridPresenter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStopWatch()
        {
            Container
                .BindInterfacesAndSelfTo<Stopwatch>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<ISettableFieldView>()
                .FromInstance(_stopWatchView)
                .WhenInjectedInto<StopwatchPresenter>();

            Container
                .BindInterfacesTo<StopwatchPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<StopWatchEventHandler>()
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

