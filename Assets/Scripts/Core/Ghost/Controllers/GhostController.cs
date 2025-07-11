﻿using System.Collections.Generic;
using System.Linq;
using ElectrumGames.Core.Environment;
using ElectrumGames.Core.Environment.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Interactions;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Ghost.Logic;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Items.Zones;
using ElectrumGames.Core.Items.Zones.Active;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;
using UnityEngine;
using UnityEngine.AI;

namespace ElectrumGames.Core.Ghost.Controllers
{
    public class GhostController : GhostBaseController
    {
        [Space]
        [SerializeField] private NavMeshAgent navmeshAgent;
        [field: Space]
        [field: SerializeField] public InteractionAura InteractionAura { get; private set; }
        [field: SerializeField] public GhostAppearAura GhostEventAura { get; private set; }
        [field: SerializeField] public ContactAura ContactAura { get; private set; }
        [field: SerializeField] public GhostAppearAura GhostHuntAura { get; private set; }
        [field: Space]
        [field: SerializeField] public RadiationGhostZone RadiationGhostZone { get; private set; }
        [field: SerializeField] public SpiritBoxGhostZone SpiritBoxGhostZone { get; private set; }
        [field: SerializeField] public TorchingGhostZone TorchingGhostZone { get; private set; }
        [field: SerializeField] public GhostWritingZone GhostWritingZone { get; private set; }
        [Space]
        [SerializeField] private float sphereRoomCastRadius = 0.5f;

        private Collider[] _colliders = new Collider[CollidersCount];
        private const int CollidersCount = 16;
        
        public IReadOnlyList<Room> Rooms => _rooms;
        public float NavmeshRemainingDistance => navmeshAgent.remainingDistance;
        public float NavmeshSpeed => navmeshAgent.speed;

        private IReadOnlyList<Room> _rooms;

        private void Start()
        {
            EvidenceController.SetData(EvidenceConfig.ConfigData.First(x => 
                x.GhostType == GhostEnvironmentHandler.GhostVariables.ghostType).Evidences);
            
            RadiationGhostZone.Init(
                _container.Resolve<EvidenceController>().Evidences.Contains(EvidenceType.Radiation), 
                _container.Resolve<RadiationConfig>());

            SpiritBoxGhostZone.Init(
                _container.Resolve<EvidenceController>().Evidences.Contains(EvidenceType.SpiritBox),
                GhostEnvironmentHandler.GhostVariables.isMale,
                GhostEnvironmentHandler.GhostVariables.age < 30,
                _container.Resolve<SpiritBoxConfig>());
            
            TorchingGhostZone.Init(_container.Resolve<TorchConfig>(),
                _container.Resolve<GhostEmfZonePool>(),
                EvidenceController.Evidences.Contains(EvidenceType.Torching),
                EvidenceController.Evidences.Contains(EvidenceType.EMF5),
                _container.Resolve<EmfData>());
            
            GhostWritingZone.Init(_container.Resolve<GhostWritableConfig>(),
                _container.Resolve<GhostEmfZonePool>(),
                EvidenceController.Evidences.Contains(EvidenceType.GhostWriting),
                EvidenceController.Evidences.Contains(EvidenceType.EMF5),
                _container.Resolve<EmfData>());
            
            SetTemperatureAtGhostRoom();
        }

        private void FixedUpdate()
        {
            NonHuntLogic?.FixedSimulate();
            GhostEventLogic?.FixedSimulate();
            HuntLogic?.FixedSimulate();
            GhostAbility?.FixedSimulate();
            
            FuseBoxCounter.FixedSimulate();
        }

        public void SetRoomsData(IReadOnlyList<Room> rooms)
        {
            _rooms = rooms;
        }

        public void SetEnabledLogic(GhostLogicSelector logicSelector)
        {
            Debug.Log($"Set logic as {logicSelector}");
            
            NonHuntLogic.IsInterrupt = logicSelector != GhostLogicSelector.All && logicSelector != GhostLogicSelector.NonHunt;
            GhostEventLogic.IsInterrupt = logicSelector != GhostLogicSelector.All && logicSelector != GhostLogicSelector.GhostEvent;
            HuntLogic.IsInterrupt = logicSelector != GhostLogicSelector.All && logicSelector != GhostLogicSelector.Hunt;
            GhostAbility.IsInterrupt = logicSelector != GhostLogicSelector.All && logicSelector != GhostLogicSelector.Ability;
        }

        public void TeleportToSpawnPoint()
        {
            var currentRoom = _rooms[GhostEnvironmentHandler.GhostRoomId];

            transform.position = currentRoom.GhostRoomHandler.SpawnPointAtRoom.position;
            transform.rotation = currentRoom.GhostRoomHandler.SpawnPointAtRoom.rotation;
        }

        public void ActivateAgent()
        {
            navmeshAgent.enabled = true;
        }

        public void MoveTo(Vector3 position)
        {
            navmeshAgent.SetDestination(position);
        }

        public void SetSpeed(float speed)
        {
            navmeshAgent.speed = speed;
        }

        public void IsStopped(bool isStopped)
        {
            navmeshAgent.isStopped = isStopped;
            
            if (isStopped)
                SetGhostAnimationSpeed(0f);
        }
        

        public void SetLogic(INonHuntLogic nonHuntLogic, IGhostEventLogic ghostEventLogic, IHuntLogic huntLogic, 
            IGhostAbility ghostAbility, GhostEmfZonePool emfZonePool, EmfData emfData)
        {
            NonHuntLogic = nonHuntLogic;
            GhostEventLogic = ghostEventLogic;
            HuntLogic = huntLogic;
            GhostAbility = ghostAbility;

            FuseBoxCounter = new FuseBoxCounterLogic(fuseBox, 
                GhostEnvironmentHandler.GhostConstants.chanceShutDownFuseBox,
                GhostEnvironmentHandler.GhostConstants.cooldownShutDown,
                emfZonePool, emfData, this);
        }

        public Room GetCurrentRoom()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, sphereRoomCastRadius, _colliders);

            for (var i = 0; i < size; i++)
            {
                if (_colliders[i].TryGetComponent<Room>(out var room))
                    return room;
            }

            return null;
        }

        public void SetTemperatureAtGhostRoom()
        {
            for (var i = 0; i < _rooms.Count; i++)
            {
                if (_rooms[i].RoomId != GhostEnvironmentHandler.GhostVariables.roomId)
                {
                    _rooms[i].TemperatureRoom.SetTemperatureRoomState(TemperatureRoomState.NoGhost);
                }
                else
                {
                    _rooms[i].TemperatureRoom.SetTemperatureRoomState(
                        EvidenceController.Evidences.Contains(EvidenceType.FreezingTemperature) ?
                            TemperatureRoomState.GhostWithFreezing : TemperatureRoomState.GhostWithoutFreezing);
                }
            }
        }
    }
}