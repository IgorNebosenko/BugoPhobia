using UnityEngine;
using UnityEngine.Serialization;

namespace ElectrumGames.Audio
{
    public class ToneGenerator : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private float amNoiseFrequency = 0.1f;
        [SerializeField] private float fmNoiseFrequency = 5f;
        [SerializeField] private float modulationDepth = 50f;
        
        [SerializeField] private bool useAmNoise;
        [SerializeField] private bool useFmNoise;

        private const int SampleRate = 44100;
        private const float Duration = 1f;

        public void GenerateSoundSinusoid(float frequency, float volume)
        {
            var clip = AudioClip.Create($"GeneratedSoundSinusoid {frequency} Hz",
                SampleRate * (int)Duration, 1, SampleRate, false);
            var samples = new float[SampleRate * (int)Duration];

            for (var i = 0; i < samples.Length; i++)
            {
                var sample = Mathf.Sin(2 * Mathf.PI * frequency * i / SampleRate);
                if (useAmNoise)
                {
                    var noise = Mathf.PerlinNoise(i * amNoiseFrequency / SampleRate, 0f);
                    sample *= noise;
                }
                samples[i] = sample;
            }

            clip.SetData(samples, 0);
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.Play();
        }

        public void GenerateSoundTriangular(float frequency, float volume)
        {
            var clip = AudioClip.Create($"GeneratedSoundTriangular {frequency} Hz",
                SampleRate * (int)Duration, 1, SampleRate, false);
            var samples = new float[SampleRate * (int)Duration];
            var period = SampleRate / frequency;

            for (var i = 0; i < samples.Length; i++)
            {
                var t = i % period / period;
                var sample = Mathf.PingPong(t * 2f, 1f) * 2f - 1f;

                if (useFmNoise)
                {
                    var noise = Mathf.PerlinNoise(i * fmNoiseFrequency / SampleRate, 0f);
                    var modulatedFrequency = frequency + noise * modulationDepth;
                    var modulatedPeriod = SampleRate / modulatedFrequency;
                    t = (i % modulatedPeriod) / modulatedPeriod;
                    sample = Mathf.PingPong(t * 2f, 1f) * 2f - 1f;
                }
                samples[i] = sample;
            }

            clip.SetData(samples, 0);
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.Play();
        }

        public void GenerateSoundQuad(float frequency, float volume)
        {
            var clip = AudioClip.Create($"GeneratedSoundQuad {frequency} Hz",
                SampleRate * (int)Duration, 1, SampleRate, false);
            var samples = new float[SampleRate * (int)Duration];
            var period = SampleRate / frequency;

            for (var i = 0; i < samples.Length; i++)
            {
                var t = i % period / period;
                var sample = t < 0.5f ? 1f : -1f;

                if (useAmNoise)
                {
                    var noise = Mathf.PerlinNoise(i * amNoiseFrequency / SampleRate, 0f);
                    sample *= noise;
                }

                samples[i] = sample;
            }

            clip.SetData(samples, 0);
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.Play();
        }

        public void GenerateSoundSaw(float frequency, float volume)
        {
            var clip = AudioClip.Create($"GeneratedSoundSaw {frequency} Hz",
                SampleRate * (int)Duration, 1, SampleRate, false);
            var samples = new float[SampleRate * (int)Duration];
            var period = SampleRate / frequency;

            for (var i = 0; i < samples.Length; i++)
            {
                var t = (i % period) / period;
                var sawWave = 2f * t - 1f;

                if (useAmNoise)
                {
                    var noise = Mathf.PerlinNoise(i * amNoiseFrequency / SampleRate, 0);
                    sawWave *= noise;
                }

                samples[i] = sawWave;
            }

            clip.SetData(samples, 0);
            audioSource.clip = clip;

            audioSource.volume = volume;
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
