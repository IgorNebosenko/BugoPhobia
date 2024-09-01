using UnityEngine;

namespace ElectrumGames.Extensions
{
    public static class AudioSourceExtensions
    {
        public static void SetPreset(this AudioSource sourcePreset, AudioSource targetPreset)
        {
            targetPreset.pitch = sourcePreset.pitch;
            targetPreset.priority = sourcePreset.priority;
            targetPreset.spatialize = sourcePreset.spatialize;
            targetPreset.spread = sourcePreset.spread;

            targetPreset.playOnAwake = false;
            
            targetPreset.volume = sourcePreset.volume;
            
            targetPreset.dopplerLevel = sourcePreset.dopplerLevel;
            
            targetPreset.maxDistance = sourcePreset.maxDistance;
            targetPreset.minDistance = sourcePreset.minDistance;
            
            targetPreset.panStereo = sourcePreset.panStereo;
            targetPreset.rolloffMode = sourcePreset.rolloffMode;
            targetPreset.spatialBlend = sourcePreset.spatialBlend;

            targetPreset.reverbZoneMix = sourcePreset.reverbZoneMix;
            targetPreset.spatializePostEffects = sourcePreset.spatializePostEffects;
            targetPreset.velocityUpdateMode = sourcePreset.velocityUpdateMode;
            
            targetPreset.outputAudioMixerGroup = sourcePreset.outputAudioMixerGroup;

            targetPreset.SetCustomCurve(AudioSourceCurveType.CustomRolloff, sourcePreset.GetCustomCurve(AudioSourceCurveType.CustomRolloff));
            targetPreset.SetCustomCurve(AudioSourceCurveType.ReverbZoneMix, sourcePreset.GetCustomCurve(AudioSourceCurveType.ReverbZoneMix));
            targetPreset.SetCustomCurve(AudioSourceCurveType.SpatialBlend, sourcePreset.GetCustomCurve(AudioSourceCurveType.SpatialBlend));
            targetPreset.SetCustomCurve(AudioSourceCurveType.Spread, sourcePreset.GetCustomCurve(AudioSourceCurveType.Spread));
        }
    }
}