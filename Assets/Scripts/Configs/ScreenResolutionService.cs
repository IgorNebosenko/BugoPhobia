using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ElectrumGames.Configs
{
    public class ScreenResolutionService
    {
        private readonly ConfigService _configService;

        private List<Vector2Int> _resolutions;
        
        public ScreenResolutionService(ConfigService configService)
        {
            _configService = configService;
            
            _resolutions = Screen.resolutions.Select(x => new Vector2Int(x.width, x.height)).
                Distinct().ToList();

            if (!_resolutions.Contains(_configService.Resolution))
            {
                _configService.Resolution = _resolutions[0];
            }

        }
    }
}