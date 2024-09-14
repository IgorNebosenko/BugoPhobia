using System.Collections.Generic;
using Core.Player.Interactions;
using ElectrumGames.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.PlayerVisuals;
using UnityEngine;

namespace ElectrumGames.Core.Player
{
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        [SerializeField] private CharacterController characterController;
        [Space]
        [SerializeField] private Transform headBob;
        [SerializeField] private Transform stayCameraTransform;
        [SerializeField] private Transform sitCameraTransform;

        protected Camera playerCamera;

        protected InputActions inputActions;
        protected ItemsConfig itemsConfig;
        
        protected IInput input;
        protected IMotor motor;
        protected CameraLifter cameraLifter;

        protected IInteraction interaction;

        private bool _isInited;
        
        protected List<IInputSimulateVisuals> simulateVisuals;

        protected PlayerConfig playerConfig;
        protected ConfigService configService;

        public bool IsHost { get; protected set; }
        public int NetId { get; protected set; }
        public int OwnerId { get; protected set; }
        public IInventory Inventory { get; private set; }

        public Vector3 Position => transform.position;

        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        private void Update()
        {
            if (!_isInited)
                return;

            var deltaTime = Time.deltaTime;

            input.Update(deltaTime);
            motor.Simulate(input, deltaTime);
            
            cameraLifter.UpdateInput(input);

            foreach (var simulateVisual in simulateVisuals)
            {
                simulateVisual.Simulate(input, deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (!_isInited)
                return;

            motor.FixedSimulate(input, Time.fixedDeltaTime);
            OnInteractionSimulate(Time.fixedDeltaTime);
        }
        
        protected virtual void OnInteractionSimulate(float deltaTime)
        {}

        public void Spawn(PlayerConfig config, ConfigService configSrv, bool isHost, InputActions inputActions, 
            ItemsConfig itemsConfig, Camera injectedCamera)
        {
            playerConfig = config;
            configService = configSrv;

            this.inputActions = inputActions;
            this.itemsConfig = itemsConfig;
            
            input = new PlayerInput(inputActions);
            input.Init();

            Inventory = new PlayerInventory();
            Inventory.Init(playerConfig.InventorySlots, transform, NetId);

            IsHost = isHost;

            if (IsHost)
            {
                injectedCamera.transform.parent = headBob;
                playerCamera = injectedCamera;
                playerCamera.transform.localPosition = Vector3.zero;
            }

            motor = new PlayerMovementMotor(characterController, playerCamera, config, configService);
            cameraLifter = new CameraLifter(playerConfig, headBob, stayCameraTransform.localPosition,
                sitCameraTransform.localPosition);

            playerCamera.fieldOfView = configService.FOV;

            simulateVisuals = new List<IInputSimulateVisuals>();
            
            OnAfterSpawn();
            _isInited = true;
        }

        protected virtual void OnAfterSpawn()
        {
        }

        public void Despawn()
        {
        }
    }
}