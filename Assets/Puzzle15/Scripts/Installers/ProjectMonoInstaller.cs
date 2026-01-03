using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Puzzle15
{
    public sealed class ProjectMonoInstaller: MonoInstaller
    {
        [SerializeField] private int _targetFrameRate = -1;

        public override void InstallBindings()
        {
            Application.targetFrameRate = _targetFrameRate; //

            Container
                .Bind<LevelConfig>()
                .AsSingle();
        }
    }
}
