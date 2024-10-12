using System;
using System.Collections.Generic;
using ElectrumGames.Core.Environment.Enums;
using ElectrumGames.Core.Ghost;
using ElectrumGames.Core.Rooms;
using UnityEngine;
using Zenject;

namespace ElectrumGames.Core.Environment.House
{
    public class HouseController : MonoBehaviour
    {
        [SerializeField] private FuseBoxEnvironmentObject fuseBox;
        [Space]
        [SerializeField] private int minGhostId = 0;
        [SerializeField] private int maxGhostId = 19;
        [Space]
        [SerializeField] private int minRoomId = 0;
        [SerializeField] private int maxRoomId = 0;
        [Space]
        [SerializeField] private Room[] rooms;
        
        public FuseBoxState FuseBoxState { get; private set; }
        
        public bool IsKeyPicked { get; private set; }
        
        public bool IsEnterDoorOpened { get; set; }
        public IReadOnlyList<Room> Rooms => rooms;

        [Inject]
        private void Construct(GhostEnvironmentHandler ghostEnvironmentHandler)
        {
            ghostEnvironmentHandler.InitGhost(minGhostId, maxGhostId, minRoomId, maxRoomId);
        }

        public void OnFuseBoxStateChanged(bool state)
        {
            if (FuseBoxState == FuseBoxState.Broken)
                return;

            FuseBoxState = state ? FuseBoxState.Enabled : FuseBoxState.Disabled;
        }

        public void OnPickUpKey()
        {
            IsKeyPicked = true;

            for (var i = 0; i < rooms.Length; i++)
                rooms[i].DoorsRoomHandler.UnlockDoors();
        }

        private void OnEnable()
        {
            fuseBox.FuseBoxChanged += OnFuseBoxStateChanged;
            HouseKeyEnvironmentObject.PickUpKey += OnPickUpKey;
        }

        private void OnDisable()
        {
            fuseBox.FuseBoxChanged -= OnFuseBoxStateChanged;
            HouseKeyEnvironmentObject.PickUpKey -= OnPickUpKey;
        }
    }
}