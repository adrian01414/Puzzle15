using System.Collections.Generic;

namespace Puzzle15
{
    public sealed class LevelConfigurationManager
    {
        public IReadOnlyList<LevelConfig> LevelConfigs => _levelConfigs;
        public LevelConfig CurrentConfig
        {
            get => _currentConfig == null ? _defaultConfig : _currentConfig;
            set => _currentConfig = value;
        }

        private LevelConfig _currentConfig;
        private LevelConfig _defaultConfig;
        private List<LevelConfig> _levelConfigs;

        public LevelConfigurationManager(List<LevelConfig> configs, LevelConfig defaultConfig) {
            _levelConfigs = new(configs);
            _defaultConfig = defaultConfig;
        }
    }
}
