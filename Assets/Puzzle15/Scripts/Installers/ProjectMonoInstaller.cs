using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public sealed class ProjectMonoInstaller: MonoInstaller
    {
        [SerializeField] private List<LevelConfig> _levelConfigs;
        [SerializeField] private LevelConfig _defaultConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<LevelConfigurationManager>()
                .AsSingle()
                .WithArguments(_levelConfigs, _defaultConfig);
        }
    }
}
