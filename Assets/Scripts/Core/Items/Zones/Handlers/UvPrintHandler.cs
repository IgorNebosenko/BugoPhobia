using System;
using ElectrumGames.Extensions;
using UniRx;
using UnityEngine;

namespace ElectrumGames.Core.Items.Zones.Handlers
{
    public class UvPrintHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] decals;
        [SerializeField] private float vanishTime = 120f;
        [SerializeField] private float glowDelay = 3f;

        private GameObject _currentDecal;

        private IDisposable _vanishProcess;

        private float _timeWithoutLight;

        private Material _material;
        private static readonly int MainColor = Shader.PropertyToID("_MainColor");

        public float Charge { get; private set; }

        private void Awake()
        {
            for (var i = 0 ; i < decals.Length; i++)
                decals[i].SetActive(false);
            
            //MakeRandomPrint();
        }

        private void Update()
        {
            if (_currentDecal.UnityNullCheck())
                return;

            _timeWithoutLight += Time.deltaTime;

            if (_timeWithoutLight >= glowDelay && Charge > 0f)
                Charge -= Time.deltaTime;
            
            _currentDecal.SetActive(Charge > 0);
            
            var color = _material.GetColor(MainColor);
            color.a = Charge;
            _material.SetColor(MainColor, color);
        }

        public void MakeRandomPrint()
        {
            _currentDecal = decals.PickRandom();
            
            _material = _currentDecal.GetComponent<MeshRenderer>().material;
            
            var color = _material.GetColor(MainColor);
            color.a = 0f;
            _material.SetColor(MainColor, color);
            
            _vanishProcess?.Dispose();

            _vanishProcess = Observable.Timer(TimeSpan.FromSeconds(vanishTime)).
                Subscribe((_) => _currentDecal = null);
        }

        public void ChargingProcess(float value)
        {
            if (_currentDecal.UnityNullCheck())
                return;
            
            Charge += value;

            if (Charge > 1f)
                Charge = 1f;

            _timeWithoutLight = 0f;
        }
    }
}