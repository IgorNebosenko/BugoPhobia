using System.Collections;
using ElectrumGames.Audio;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items.Zones;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Extensions;
using ElectrumGames.MVP.Managers;
using ElectrumGames.UI.Presenters;
using TMPro;
using UnityEngine;

namespace ElectrumGames.Core.Items.Equipment.WithVisualElements
{
    public class ItemSpiritBoxElectronic : ItemInstanceBase,
        IGhostHuntingHasElectricity, IGhostHuntingInteractableStay, IGhostHuntingInteractableExit
    {
        [SerializeField] private float percentChanceResponse = 0.35f;
        [SerializeField] private float cooldownTime = 2f;
        [Space]
        [SerializeField] private TMP_Text frequencyText;
        [SerializeField] private Light onLight;
        [SerializeField] private float minFrequency = 88f;
        [SerializeField] private float maxFrequency = 110f;
        [Space]
        [SerializeField] private float frequencyStep = 0.1f;
        [SerializeField] private float frequencyStepTime = 0.2f;
        [Space]
        [SerializeField] private string textOff = "OFF";
        [SerializeField] private string textOnFormat = "{0:0.0}";
        [Space] 
        [SerializeField] private float responsePause = 1f;
        [Space]
        [SerializeField] private NoiseGenerator noiseGenerator;
        [SerializeField] private AudioSource audioSourceResponse;

        private PopupManager _popupManager;
        private InputActions _inputActions;
        
        private SpiritBoxPopupPresenter _spiritBoxPopupPresenter;
        
        private bool _isOn;

        private float _currentFrequency;
        private bool _isFrequencyIncrease = true;
        private float _currentTime;

        private bool _ghostInteractionOn;

        private float _responsePauseTime;
        private float _cooldownRequest;
        
        private Collider[] _colliders = new Collider[CollidersCount];
        private const float SphereRoomCastRadius = 0.5f;
        private const int CollidersCount = 16;
        
        public bool IsElectricityOn => _isOn;

        private void Awake()
        {
            _currentFrequency = minFrequency;

            _cooldownRequest = cooldownTime;
        }

        protected override void OnAfterInit()
        {
            _popupManager = container.Resolve<PopupManager>();
            _inputActions = container.Resolve<InputActions>();
        }

        private void Close()
        {
            _isOn = false;
            onLight.enabled = false;
            
            _inputActions.Moving.Enable();
            _inputActions.UiEvents.Enable();

            noiseGenerator.SetPlayState(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            if (!_isOn)
            {
                frequencyText.text = textOff;
                return;
            }
            
            if (_cooldownRequest >= 0)
                _cooldownRequest -= Time.fixedDeltaTime;
            
            if (_ghostInteractionOn)
                return;

            if (_responsePauseTime > 0)
            {
                _responsePauseTime -= Time.fixedDeltaTime;
                return;
            }

            _currentTime -= Time.fixedDeltaTime;
            
            if (_currentTime > 0)
                return;

            _currentTime = frequencyStepTime;

            if (_isFrequencyIncrease)
            {
                _currentFrequency += frequencyStep;
                if (_currentFrequency >= maxFrequency)
                    _isFrequencyIncrease = false;
            }
            else
            {
                _currentFrequency -= frequencyStep;

                if (_currentFrequency <= minFrequency)
                    _isFrequencyIncrease = true;
            }
            
            DisplayFrequency(_currentFrequency);
        }

        public override void OnMainInteraction()
        {
            _isOn = !_isOn;

            onLight.enabled = _isOn;

            if (!_spiritBoxPopupPresenter.UnityNullCheck())
            {
                if (_spiritBoxPopupPresenter.ViewExists)
                {
                    UnsubscribeToPopup();
                    _spiritBoxPopupPresenter.Close();
                }

                _spiritBoxPopupPresenter = null;
            }

            if (!_isOn)
            {
                noiseGenerator.SetPlayState(false);
                
                _inputActions.Moving.Enable();
                _inputActions.UiEvents.Enable();

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                noiseGenerator.SetPlayState(true);
                
                _spiritBoxPopupPresenter = _popupManager.ShowPopup<SpiritBoxPopupPresenter>();
                _spiritBoxPopupPresenter.Init(Close);
                SubscribeToPopup();

                _inputActions.Moving.Disable();
                _inputActions.UiEvents.Disable();

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public override void OnAlternativeInteraction()
        {
            if (_spiritBoxPopupPresenter.UnityNullCheck())
                return;
            
            noiseGenerator.SetPlayState(false);
            
            UnsubscribeToPopup();
            _spiritBoxPopupPresenter.Close();
            _spiritBoxPopupPresenter = null;
            
            _inputActions.Moving.Enable();
            _inputActions.UiEvents.Enable();
            
            Cursor.visible = false;
            
            _isOn = false;
            onLight.enabled = false;
        }

        public override void OnAfterDrop()
        {
            noiseGenerator.SetPlayState(false);
            
            if (_spiritBoxPopupPresenter.UnityNullCheck())
                return;
            
            if (_spiritBoxPopupPresenter.ViewExists)
            {
                UnsubscribeToPopup();
                _spiritBoxPopupPresenter.Close();
            }
            
            _spiritBoxPopupPresenter = null;
            
            _inputActions.Moving.Enable();
            _inputActions.UiEvents.Enable();
            
            Cursor.visible = false;

            _isOn = false;
            onLight.enabled = false;
        }

        private void DisplayFrequency(float frequency)
        {
            frequencyText.text = string.Format(textOnFormat, frequency);
        }

        private void DisplayResponse(string response)
        {
            _responsePauseTime = responsePause;

            frequencyText.text = response;
        }

        private void PlayResponse(AudioClip clip)
        {
            audioSourceResponse.PlayOneShot(clip);
        }

        public void OnGhostInteractionStay()
        {
            if (!_isOn)
                return;
            
            _ghostInteractionOn = true;
            
            DisplayFrequency(Random.Range(minFrequency, maxFrequency));
            onLight.enabled = Random.Range(0, 3) != 0;
        }

        public void OnGhostInteractionExit()
        {
            _ghostInteractionOn = false;
            onLight.enabled = _isOn;
        }

        private void SubscribeToPopup()
        {
            _spiritBoxPopupPresenter.WhereAreYouClicked += OnWhereAreYouClicked;
            _spiritBoxPopupPresenter.IsMaleClicked += OnIsMaleClicked;
            _spiritBoxPopupPresenter.AgeClicked += OnAgeClicked;
        }
        
        private void UnsubscribeToPopup()
        {
            _spiritBoxPopupPresenter.WhereAreYouClicked -= OnWhereAreYouClicked;
            _spiritBoxPopupPresenter.IsMaleClicked -= OnIsMaleClicked;
            _spiritBoxPopupPresenter.AgeClicked -= OnAgeClicked;
        }

        private void OnWhereAreYouClicked()
        {
            OnClicked(SpiritBoxGhostRequest.WhereGhost);
        }
        
        private void OnIsMaleClicked()
        {
            OnClicked(SpiritBoxGhostRequest.IsMale);
        }
        
        private void OnAgeClicked()
        {
            OnClicked(SpiritBoxGhostRequest.Age);
        }

        private void OnClicked(SpiritBoxGhostRequest request)
        {
            if (_cooldownRequest > 0)
                return;

            _cooldownRequest = cooldownTime;
            
            var zone = CheckIsOnZone();
            
            if (zone.UnityNullCheck())
                return;

            if (Random.Range(0f, 1f) < percentChanceResponse)
            {
                DisplayResponse(SpiritBoxDataElement.Empty().Text);
            }
            else
            {
                var response = zone.GetResponse(request);
                DisplayResponse(response.Text);
                PlayResponse(response.Clip);
            }
        }

        private SpiritBoxGhostZone CheckIsOnZone()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, SphereRoomCastRadius, _colliders);

            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<SpiritBoxGhostZone>(out var spiritBoxGhostZone))
                    return spiritBoxGhostZone;
            }

            return null;
        }
    }
}