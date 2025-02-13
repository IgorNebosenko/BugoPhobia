using System.Collections.Generic;
using ElectrumGames.Core.Player.Interactions.Items;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Items;
using ElectrumGames.Core.Items.Inventory;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player.Interactions;
using ElectrumGames.Core.Player.Movement;
using ElectrumGames.Core.Player.Sanity;
using ElectrumGames.Core.PlayerVisuals;
using ElectrumGames.Core.Rooms;
using ElectrumGames.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

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

        protected EnvironmentHandler environmentHandler;

        protected IHaveVisibility _ghostVisibility;

        private bool _isInRoomPreviousState;

        private Collider[] _colliders = new Collider[CollidersArraySize];
        private const int CollidersArraySize = 40;

        public bool IsPlayablePlayer { get; protected set; }
        public bool IsHost { get; protected set; }
        public int NetId { get; protected set; }
        public int OwnerId { get; protected set; }
        
        public bool IsAlive { get; protected set; } = true;
        public Transform PlayerHead => headBob;
        public IInventory Inventory { get; private set; }
        public InventoryIndexHandler InventoryIndexHandler { get; protected set; }
        public ISanity Sanity { get; private set; }
        public FlashLightInteractionHandler FlashLightInteractionHandler { get; protected set; }
        
        public LookAtGhostHandler LookAtGhostHandler { get; protected set; }

        public Vector3 Position => transform.position;

        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        private void Start()
        {
            LookAtGhostHandler = new LookAtGhostHandler(playerCamera);
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
            PlayerUpdate();
            OnInteractionSimulate(Time.fixedDeltaTime);
        }

        protected virtual void PlayerUpdate()
        {
            var currentRoom = GetCurrentRoom();

            if (!currentRoom.UnityNullCheck() != _isInRoomPreviousState)
            {
                _isInRoomPreviousState = !_isInRoomPreviousState;
                
                if (_isInRoomPreviousState)
                    environmentHandler.SetEnvironmentIndoor();
                else
                    environmentHandler.SetEnvironmentOutdoor();
            }
            
            if (!currentRoom.UnityNullCheck() && !currentRoom.IsElectricityOn)
                Sanity.ChangeSanity(ghostDifficultyData.DefaultDrainSanity * Time.fixedDeltaTime, -1);

            var ghostCollider = LookAtGhostHandler.CheckIsLookAtGhost();

            if (!currentRoom.UnityNullCheck() && !ghostCollider.UnityNullCheck() &&
                ghostCollider.HaveVisibility.IsVisible)
            {
                Sanity.ChangeSanity(ghostDifficultyData.DrainSanityPerLookOnGhost * Time.fixedDeltaTime, -1);
                
            }

        }

        protected virtual void OnInteractionSimulate(float deltaTime)
        {}

        public void Spawn(DiContainer container, GhostDifficultyData difficultyData, bool isPlayablePlayer, bool isHost)
        {
            playerConfig = container.Resolve<PlayerConfig>();
            configService = container.Resolve<ConfigService>();

            inputActions = container.Resolve<InputActions>();
            itemsConfig = container.Resolve<ItemsConfig>();

            ghostDifficultyData = difficultyData;
            
            _ghostVisibility = container.TryResolve<IHaveVisibility>();
            
            input = new PlayerInput(inputActions);
            input.Init();

            Inventory = new PlayerInventory();
            Inventory.Init(playerConfig.InventorySlots, transform, NetId);
            
            Sanity = new PlayerSanity(ghostDifficultyData.DefaultSanity, NetId);

            IsPlayablePlayer = isPlayablePlayer;
            IsHost = isHost;

            var injectedCamera = container.Resolve<Camera>();

            if (IsHost)
            {
                injectedCamera.transform.parent = headBob;
                playerCamera = injectedCamera;
                playerCamera.transform.localPosition = Vector3.zero;
            }

            environmentHandler = container.Resolve<EnvironmentHandler>();
            FlashLightInteractionHandler = container.Resolve<FlashLightInteractionHandler>();

            _motor = new PlayerMovementMotor(characterController, playerCamera, playerConfig, configService);
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

        public abstract void OnGhostInterferenceStay();

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
            
            if (!IsAlive)
                return;

            IsAlive = false;

            for (var i = 0; i < Inventory.Items.Count; i++)
            {
                Inventory.Items[i]?.OnDropItem(transform);
            }

            SceneManager.LoadSceneAsync((int)MissionMap.LobbyTier0);
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