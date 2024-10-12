using System.Collections.Generic;
using Core.Items.Inventory;
using Core.Player.Interactions;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.Player.Sanity;
using ElectrumGames.Core.PlayerVisuals;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElectrumGames.Core.Player
{
    public abstract class PlayerBase : MonoBehaviour, IPlayer
    {
        [SerializeField] private CharacterController characterController;
        [Space]
        [SerializeField] private Transform headBob;
        [SerializeField] private Transform stayCameraTransform;
        [SerializeField] private Transform sitCameraTransform;
        [Space]
        [SerializeField] private float sphereRoomCastRadius = 0.5f;

        protected Camera playerCamera;

        protected InputActions inputActions;
        protected ItemsConfig itemsConfig;

        protected GhostDifficultyData ghostDifficultyData;
        
        protected IInput input;
        private IMotor _motor;
        private CameraLifter _cameraLifter;

        protected IInteraction interaction;

        private bool _isInited;
        
        protected List<IInputSimulateVisuals> simulateVisuals;

        protected PlayerConfig playerConfig;
        protected ConfigService configService;

        private Collider[] _colliders = new Collider[CollidersArraySize];
        private const int CollidersArraySize = 40;

        public bool IsHost { get; protected set; }
        public int NetId { get; protected set; }
        public int OwnerId { get; protected set; }
        public IInventory Inventory { get; private set; }
        public InventoryIndexHandler InventoryIndexHandler { get; protected set; }
        public ISanity Sanity { get; private set; }

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
            _motor.Simulate(input, deltaTime);
            
            _cameraLifter.UpdateInput(input);

            foreach (var simulateVisual in simulateVisuals)
            {
                simulateVisual.Simulate(input, deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (!_isInited)
                return;

            _motor.FixedSimulate(input, Time.fixedDeltaTime);
            SanityDrainProcess();
            OnInteractionSimulate(Time.fixedDeltaTime);
        }

        protected virtual void SanityDrainProcess()
        {
            var currentRoom = GetCurrentRoom();
            
            if (currentRoom.UnityNullCheck() || currentRoom.IsElectricityOn)
                return;
            
            Sanity.ChangeSanity(ghostDifficultyData.DefaultDrainSanity * Time.fixedDeltaTime, -1);
        }

        protected virtual void OnInteractionSimulate(float deltaTime)
        {}

        public void Spawn(PlayerConfig config, ConfigService configSrv, bool isHost, InputActions inputActions, 
            ItemsConfig itemsConfig, GhostDifficultyData difficultyData, Camera injectedCamera)
        {
            playerConfig = config;
            configService = configSrv;

            this.inputActions = inputActions;
            this.itemsConfig = itemsConfig;

            ghostDifficultyData = difficultyData;
            
            input = new PlayerInput(inputActions);
            input.Init();

            Inventory = new PlayerInventory();
            Inventory.Init(playerConfig.InventorySlots, transform, NetId);
            
            Sanity = new PlayerSanity(ghostDifficultyData.DefaultSanity, NetId);

            IsHost = isHost;

            if (IsHost)
            {
                injectedCamera.transform.parent = headBob;
                playerCamera = injectedCamera;
                playerCamera.transform.localPosition = Vector3.zero;
            }

            _motor = new PlayerMovementMotor(characterController, playerCamera, config, configService);
            _cameraLifter = new CameraLifter(playerConfig, headBob, stayCameraTransform.localPosition,
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

        public int GetCurrentStayRoom()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, sphereRoomCastRadius, _colliders);
            
            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<Room>(out var room))
                    return room.RoomId;
            }

            return -1;
        }

        public void Death()
        {
            //ToDo write this for multiplayer
            Debug.LogWarning("Player is dead! Loading zero scene, need to check index what to load!");
            SceneManager.LoadSceneAsync(0);
        }

        private Room GetCurrentRoom()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, sphereRoomCastRadius, _colliders);
            
            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<Room>(out var room))
                    return room;
            }

            return null;
        }
    }
}