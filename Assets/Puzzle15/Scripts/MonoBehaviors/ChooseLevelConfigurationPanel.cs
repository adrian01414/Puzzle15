using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Puzzle15 {
    public class ChooseLevelConfigurationPanel : MonoBehaviour
    {
        private LevelConfigurationManager _configManager;

        [Inject]
        public void Construct(LevelConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        private void Awake()
        {
            // generate buttons

        }

        public void ChooseDifficult(LevelConfig config)
        {
            _configManager.CurrentConfig = config;
        }
    }
}
