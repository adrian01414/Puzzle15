using System.Collections.Generic;

namespace Puzzle15
{
    public sealed class LevelConfigurationManager
    {
        public IReadOnlyList<LevelConfig> LevelConfigs => _levelConfigs;
        public LevelConfig CurrentConfig { get; set; }

        private List<LevelConfig> _levelConfigs;

        public LevelConfigurationManager(List<LevelConfig> configs) {
            _levelConfigs = new(configs);
        }
    }
}
