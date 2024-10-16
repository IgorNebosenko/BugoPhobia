using UnityEngine;

namespace ElectrumGames.Audio
{
    public class NoiseGenerator : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [Space]
        [SerializeField] private AudioLowPassFilter lowPassFilter;
        [SerializeField] private AudioHighPassFilter highPassFilter;
        [Space]
        [SerializeField] private float minSampleValue = -1f;
        [SerializeField] private float maxSampleValue = 1f;
        
        private float[] _noiseSamples;
        private int _sampleIndex;
        
        private const int SampleSize = 48000;

        private void Start()
        {
            audioSource.loop = true;
            audioSource.spatialBlend = 0;
            audioSource.Play();

            _noiseSamples = new float[SampleSize];
            GenerateNoiseSamples();
        }

        private void GenerateNoiseSamples()
        {
            for (var i = 0; i < _noiseSamples.Length; i++)
            {
                _noiseSamples[i] = Random.Range(minSampleValue, maxSampleValue);
            }
        }

        public void SetPassFrequency(float minFrequency, float maxFrequency)
        {
            lowPassFilter.cutoffFrequency = minFrequency;
            highPassFilter.cutoffFrequency = maxFrequency;
        }

        private void OnAudioFilterRead(float[] data, int channels)
        {
            for (var i = 0; i < data.Length; i += channels)
            {
                var sample = _noiseSamples[_sampleIndex];
                _sampleIndex = (_sampleIndex + 1) % SampleSize;

                for (var channel = 0; channel < channels; channel++)
                {
                    data[i + channel] = sample;
                }
            }
        }
    }
}