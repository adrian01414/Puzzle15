using System;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GridView _view;
        [SerializeField] private CounterView _counterView;

        [Inject] private LevelConfigurationManager _levelConfigurationManager;

        public override void InstallBindings()
        {
            IGridGenerator gridGenerator = new SimpleGridGenerator();
            Grid model = new Grid(gridGenerator, _levelConfigurationManager.CurrentConfig.Size);

            Container
                .Bind<Grid>()
                .FromInstance(model)
                .AsSingle();

            Container
                .Bind<GridView>()
                .FromInstance(_view)
                .AsSingle();

            _view.Initialize(model.GetGridSize(), model.GetCells());

            Container
                .BindInterfacesTo<GridPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<Stopwatch>()
                .FromNew()
                .AsSingle();

            BindCounter();
        }

        private void BindCounter()
        {
            Container
                .Bind<Counter>()
                .AsSingle();

            Container
                .Bind<ISettableFieldView>()
                .FromInstance(_counterView)
                .AsSingle();

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

