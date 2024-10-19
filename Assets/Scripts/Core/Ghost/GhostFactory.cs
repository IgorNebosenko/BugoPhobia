using System;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Ghost
{
    public class GhostFactory : MonoBehaviour, IHaveNetId
    {
        [SerializeField] private GhostController ghostControllerTemplate;
        
        public int NetId { get; private set; }
        public int OwnerId { get; private set; }

        private NetIdFactory _netIdFactory;
        private GhostEnvironmentHandler _ghostEnvironmentHandler;
        private GhostModelsList _modelsList;
        private GhostDifficultyList _ghostDifficultyList;
        private ActivityConfig _activityConfig;
        private GhostEmfZonePool _ghostEmfZonePool;
        private EmfData _emfData;
        private EvidenceController _evidenceController;
        private MissionPlayersHandler _missionPlayersHandler;
        private GhostFlickConfig _flickConfig;
        private HuntPoints _huntPoints;
        private MissionDataHandler _missionDataHandler;

        public event Action<GhostBaseController> GhostCreated;

        [Inject]
        private void Construct(NetIdFactory netIdFactory, GhostEnvironmentHandler ghostEnvironmentHandler, GhostModelsList modelsList,
            GhostDifficultyList ghostDifficultyList, ActivityConfig activityConfig, GhostEmfZonePool ghostEmfZonePool, EmfData emfData,
            EvidenceController evidenceController, MissionPlayersHandler missionPlayersHandler, GhostFlickConfig flickConfig,
            HuntPoints huntPoints, MissionDataHandler missionDataHandler)
        {
            _netIdFactory = netIdFactory;
            _ghostEnvironmentHandler = ghostEnvironmentHandler;
            _modelsList = modelsList;
            _ghostDifficultyList = ghostDifficultyList;
            _activityConfig = activityConfig;
            _ghostEmfZonePool = ghostEmfZonePool;
            _emfData = emfData;
            _evidenceController = evidenceController;
            _missionPlayersHandler = missionPlayersHandler;
            _flickConfig = flickConfig;
            _huntPoints = huntPoints;
            _missionDataHandler = missionDataHandler;

            _netIdFactory.Initialize(this);
        }

        public void SetNetId(int netId, int ownerId = -1)
        {
            NetId = netId;
            OwnerId = ownerId;
        }

        public GhostBaseController CreateGhost(IReadOnlyList<Room> rooms)
        {
            var ghost = Instantiate(ghostControllerTemplate, transform);
            _netIdFactory.Initialize(ghost);
            
            ghost.Init(_ghostEnvironmentHandler, _modelsList, _evidenceController);
            ghost.SetGhostAnimationSpeed(0f);
            ghost.SetGhostVisibility(false);
            ghost.SetRoomsData(rooms);
            ghost.TeleportToSpawnPoint();
            
            ghost.ActivateAgent();
            
            SetLogic(ghost, _ghostEnvironmentHandler);
            
            GhostCreated?.Invoke(ghost);
            return ghost;
        }

        private void SetLogic(GhostController controller, GhostEnvironmentHandler environmentHandler)
        {
            INonHuntLogic nonHuntLogic = null;
            IGhostEventLogic ghostEventLogic = null;
            IHuntLogic huntLogic = null;
            IGhostAbility ghostAbility = null;
            var activityData = _activityConfig.GhostActivities.First(x =>
                x.GhostType == controller.GhostEnvironmentHandler.GhostVariables.ghostType);
            
            switch (controller.GhostEnvironmentHandler.GhostVariables.ghostType)
            {
                case GhostType.Blaze:
                    nonHuntLogic = new BlazeNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new BlazeGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new BlazeHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, 
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new BlazeAbility();
                    break;
                case GhostType.Yrka:
                    nonHuntLogic = new YrkaNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int)_missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new YrkaGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new YrkaHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, 
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new YrkaAbility();
                    break;
                case GhostType.Wraith:
                    nonHuntLogic = new WraithNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new WraithGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new WraithHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new WraithAbility();
                    break;
                case GhostType.Mare:
                    nonHuntLogic = new MareNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new MareGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new MareHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new MareAbility();
                    break;
                case GhostType.Babaduk:
                    nonHuntLogic = new BabadukNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new BabadukGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new BabadukHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new BabadukAbility();
                    break;
                case GhostType.Invisible:
                    nonHuntLogic = new InvisibleNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new InvisibleGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new InvisibleHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new InvisibleAbilities();
                    break;
                case GhostType.Yurei:
                    nonHuntLogic = new YureiNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new YureiGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new YureiHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new YureiAbility();
                    break;
                case GhostType.Glitch:
                    nonHuntLogic = new GlitchNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new GlitchGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new GlitchHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new GlitchAbility();
                    break;
                case GhostType.Naamah:
                    nonHuntLogic = new NaamahNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new NaamahGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new NaamahHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new NaamahAbility();
                    break;
                case GhostType.ElementalFear:
                    nonHuntLogic = new ElementalOfFearNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                    ghostEventLogic = new ElementalOfFearGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                    huntLogic = new ElementalOfFearHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                    ghostAbility = new ElementalOfFearAbility();
                    break;
                case GhostType.Deogen:
                    break;
                case GhostType.LostSoul:
                    break;
                case GhostType.Mimic:
                    break;
                case GhostType.Polymorph:
                    break;
                case GhostType.Imp:
                    break;
                case GhostType.Arsonist:
                    break;
                case GhostType.Hechman:
                    break;
                case GhostType.Poltergeist:
                    break;
                case GhostType.Etheral:
                    break;
                case GhostType.Lich:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            nonHuntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostEventLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            huntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostAbility?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            
            
            controller.SetLogic(nonHuntLogic, ghostEventLogic, huntLogic, ghostAbility);
        }
    }
}