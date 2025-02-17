using System;
using System.Collections.Generic;
using System.Linq;
using ElectrumGames.CommonInterfaces;
using ElectrumGames.Configs;
using ElectrumGames.Core.Common;
using ElectrumGames.Core.Ghost.Configs;
using ElectrumGames.Core.Ghost.Controllers;
using ElectrumGames.Core.Ghost.Interactions.Pools;
using ElectrumGames.Core.Ghost.Logic.Abilities;
using ElectrumGames.Core.Ghost.Logic.GhostEvents;
using ElectrumGames.Core.Ghost.Logic.Hunt;
using ElectrumGames.Core.Ghost.Logic.NonHunt;
using ElectrumGames.Core.Journal;
using ElectrumGames.Core.Missions;
using ElectrumGames.Core.Player;
using ElectrumGames.Core.Rooms;
using ElectrumGames.GlobalEnums;
using ElectrumGames.Network;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
        private JournalManager _journalManager;
        private IFuseBoxInteractable _fuseBoxInteractable;
        private DiContainer _container;

        public event Action<GhostBaseController> GhostCreated;

        [Inject]
        private void Construct(NetIdFactory netIdFactory, GhostEnvironmentHandler ghostEnvironmentHandler, GhostModelsList modelsList,
            GhostDifficultyList ghostDifficultyList, ActivityConfig activityConfig, GhostEmfZonePool ghostEmfZonePool, EmfData emfData,
            EvidenceController evidenceController, MissionPlayersHandler missionPlayersHandler, GhostFlickConfig flickConfig,
            HuntPoints huntPoints, MissionDataHandler missionDataHandler, JournalManager journalManager, IFuseBoxInteractable fuseBox, 
            DiContainer container)
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
            _journalManager = journalManager;
            _fuseBoxInteractable = fuseBox;
            _container = container;

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
            
            ghost.Init(_ghostEnvironmentHandler, _modelsList, _evidenceController, _journalManager, _fuseBoxInteractable);
            ghost.SetGhostAnimationSpeed(0f);
            ghost.SetGhostVisibility(false);
            ghost.SetRoomsData(rooms);
            ghost.TeleportToSpawnPoint();
            
            ghost.ActivateAgent();
            
            SetLogic(ghost, _ghostEnvironmentHandler);
            
            GhostCreated?.Invoke(ghost);

            _container.BindInstance(ghost).AsSingle();
            
            return ghost;
        }

        private void SetLogic(GhostController controller, GhostEnvironmentHandler environmentHandler)
        {
            var nonHuntLogic = GetNonHuntLogicByGhostType(controller, 
                controller.GhostEnvironmentHandler.GhostVariables.ghostType);
            var ghostEventLogic = GetGhostEventLogicByType(controller,
                controller.GhostEnvironmentHandler.GhostVariables.ghostType);
            var huntLogic = GetHuntLogicByType(controller, controller.GhostEnvironmentHandler.GhostVariables.ghostType);
            var ghostAbility = GetGhostAbilityByType(controller, 
                controller.GhostEnvironmentHandler.GhostVariables.ghostType);
            
            nonHuntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostEventLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            huntLogic?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            ghostAbility?.Setup(environmentHandler.GhostVariables, environmentHandler.GhostConstants, environmentHandler.GhostRoomId);
            
            controller.SetLogic(nonHuntLogic, ghostEventLogic, huntLogic, ghostAbility, _ghostEmfZonePool, _emfData);
        }

        public INonHuntLogic GetNonHuntLogicByGhostType(GhostController controller, GhostType ghostType)
        {
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == ghostType);
            
            switch (ghostType)
            {
                case GhostType.Blaze:
                    return new BlazeNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Yrka:
                    return new YrkaNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int)_missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Wraith:
                    return new WraithNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Mare:
                    return new MareNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Babaduk:
                    return new BabadukNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Invisible:
                    return new InvisibleNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Yurei:
                    return new YureiNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Glitch:
                    return new GlitchNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Naamah:
                    return new NaamahNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.ElementalFear:
                    return new ElementalOfFearNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Deogen:
                    return new DeogenNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.LostSoul:
                    return new LostSoulNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Mimic:
                    return new MimicNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Polymorph:
                    return GetNonHuntLogicByGhostType(controller, (GhostType) Random.Range(0, (int) GhostType.Lich));
                case GhostType.Imp:
                    return new ImpNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Arsonist:
                    return new ArsonistNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Henchman:
                    return new HenchmanNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Poltergeist:
                    return new PoltergeistNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Ethereal:
                    return new EtherealNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                case GhostType.Lich:
                    return new LichNonHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                        (int) _missionDataHandler.MissionDifficulty], activityData, _ghostEmfZonePool, _emfData);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IGhostEventLogic GetGhostEventLogicByType(GhostController controller, GhostType ghostType)
        {
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == ghostType);
            
            switch (ghostType)
            {
                case GhostType.Blaze:
                    return new BlazeGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Yrka:
                    return new YrkaGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Wraith:
                    return new WraithGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Mare:
                    return new MareGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Babaduk:
                    return new BabadukGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Invisible:
                    return new InvisibleGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Yurei:
                    return new YureiGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Glitch:
                    return new GlitchGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Naamah:
                    return new NaamahGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.ElementalFear:
                    return new ElementalOfFearGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Deogen:
                    return new DeogenGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.LostSoul:
                    return new LostSoulGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Mimic:
                    return new MimicGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Polymorph:
                    return GetGhostEventLogicByType(controller, (GhostType) Random.Range(0, (int) GhostType.Lich));
                case GhostType.Imp:
                    return new ImpGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Arsonist:
                    return new ArsonistGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Henchman:
                    return new HenchmanGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Poltergeist:
                    return new PoltergeistGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Ethereal:
                    return new EtherealGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                case GhostType.Lich:
                    return new LichGhostEventLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty],
                        activityData, _ghostEmfZonePool, _emfData, _missionPlayersHandler);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IHuntLogic GetHuntLogicByType(GhostController controller, GhostType type)
        {
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == type);
            
            switch (type)
            {
                case GhostType.Blaze:
                    return new BlazeHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, 
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Yrka:
                    return new YrkaHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int)_missionDataHandler.MissionDifficulty], activityData, 
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Wraith:
                    return new WraithHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Mare:
                    return new MareHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Babaduk:
                    return new BabadukHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Invisible:
                    return new InvisibleHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Yurei:
                    return new YureiHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Glitch:
                    return new GlitchHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Naamah:
                    return new NaamahHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.ElementalFear:
                    return new ElementalOfFearHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Deogen:
                    return new DeogenHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.LostSoul:
                    return new LostSoulHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Mimic:
                    return new MimicHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Polymorph:
                    return GetHuntLogicByType(controller, (GhostType) Random.Range(0, (int) GhostType.Lich));
                case GhostType.Imp:
                    return new ImpHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Arsonist:
                    return new ArsonistHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Henchman:
                    return new HenchmanHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Poltergeist:
                    return new PoltergeistHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Ethereal:
                    return new EtherealHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                case GhostType.Lich:
                    return new LichHuntLogic(controller, _ghostDifficultyList.GhostDifficultyData[
                            (int) _missionDataHandler.MissionDifficulty], activityData,
                        _missionPlayersHandler, _flickConfig, _huntPoints);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IGhostAbility GetGhostAbilityByType(GhostController controller, GhostType ghostType)
        {
            var activityData = _activityConfig.GhostActivities.First(x => x.GhostType == ghostType);
            
            switch (ghostType)
            {
                case GhostType.Blaze:
                    return new BlazeAbility();
                case GhostType.Yrka:
                    return new YrkaAbility();
                case GhostType.Wraith:
                    return new WraithAbility();
                case GhostType.Mare:
                    return new MareAbility();
                case GhostType.Babaduk:
                    return new BabadukAbility(controller, _ghostEmfZonePool, activityData, _emfData);
                case GhostType.Invisible:
                    return new InvisibleAbilities();
                case GhostType.Yurei:
                    return new YureiAbility();
                case GhostType.Glitch:
                    return new GlitchAbility();
                case GhostType.Naamah:
                    return new NaamahAbility();
                case GhostType.ElementalFear:
                    return new ElementalOfFearAbility();
                case GhostType.Deogen:
                    return new DeogenAbility();
                case GhostType.LostSoul:
                    return new LostSoulAbility();
                case GhostType.Mimic:
                    return new MimicAbility();
                case GhostType.Polymorph:
                    return GetGhostAbilityByType(controller, (GhostType) Random.Range(0, (int) GhostType.Lich));
                case GhostType.Imp:
                    return new ImpAbility();
                case GhostType.Arsonist:
                    return new ArsonistAbility();
                case GhostType.Henchman:
                    return new HenchmanAbility();
                case GhostType.Poltergeist:
                    return new PoltergeistAbility();
                case GhostType.Ethereal:
                    return new EtherealAbility();
                case GhostType.Lich:
                    return new LichAbility();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}