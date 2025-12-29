using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public class MainMenuSceneMonoInstaller : MonoInstaller
    {
        [SerializeField] private ChooseLevelView _chooseLevelView;
        [SerializeField] private PlayButton _playButton;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayButton>()
                .FromInstance(_playButton)
                .AsSingle();

            Container
                .BindInterfacesTo<PlayButtonEventHandler>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ChooseLevelView>()
                .FromInstance(_chooseLevelView)
                .AsSingle();

            Container
                .BindInterfacesTo<ChooselLevelPresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}
