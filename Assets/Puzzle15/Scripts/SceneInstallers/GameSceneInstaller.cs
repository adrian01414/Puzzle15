using System;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GridView _view = null;

        private LevelConfig _levelConfig;

        public override void InstallBindings()
        {
            IGridGenerator gridGenerator = new SimpleGridGenerator();
            Grid model = new Grid(gridGenerator, _levelConfig.Size);

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
                .Bind<Stopwatch>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<Counter>()
                .FromNew()
                .AsSingle();
        }
    }
}

