﻿using ElectrumGames.Audio;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "Emf Data", menuName = "Ghosts configs/Emf Data")]
    public class EmfData : ScriptableObject
    {
        [field: SerializeField] public int EvidenceLevel { get; private set; } = 4;
        [field: SerializeField] public float ChanceEvidence { get; private set; } = 0.25f;
        [field: SerializeField] public float TimeEmfInteraction { get; private set; } = 15f;
        [field: Space] 
        [field: SerializeField] public int DoorDefaultEmf { get; private set; } = 1;
        [field: SerializeField] public Vector3 DoorCylinderSize { get; private set; } = new(2f, 1f, 2f);
        [field: SerializeField] public float DoorHeightOffset { get; private set; } = 1f;
        [field: Space]
        [field: SerializeField] public int SwitchDefaultEmf { get; private set; } = 1;
        [field: SerializeField] public Vector3 SwitchCylinderSize { get; private set; } = new(1.5f, 1f, 1.5f);
        [field: SerializeField] public float SwitchHeightOffset { get; private set; } = 0f;
        [field: Space]
        [field: SerializeField] public int ThrowDefaultEmf { get; private set; } = 2;
        [field: SerializeField] public float ThrowSphereSize { get; private set; } = 1.5f;
        [field: SerializeField] public float ThrowHeightOffset { get; private set; } = 0f;
        [field: Space]
        [field: SerializeField] public int TorchDefaultEmf { get; private set; } = 1;
        [field: SerializeField] public float TorchSphereSize { get; private set; } = 1.5f;
        [field: SerializeField] public float TorchHeightOffset { get; private set; } = 0f;
        [field: Space]
        [field: SerializeField] public int OtherInteractionDefaultEmf { get; private set; } = 1;
        [field: SerializeField] public Vector3 OtherInteractionCylinderSize { get; private set; } = new(2f, 1f, 2f);
        [field: SerializeField] public float OtherInteractionHeightOffset { get; private set; } = 1f;
        [field: Space]
        [field: SerializeField] public int GhostWritingDefaultEmf { get; private set; } = 1;
        [field: SerializeField] public Vector3 GhostWritingCylinderSize { get; private set; } = new(1.5f, 1f, 1.5f);
        [field: SerializeField] public float GhostWritingHeightOffset { get; private set; } = 1f;
        [field: Space]
        [field: SerializeField] public int GhostEventDefaultEmf { get; private set; } = 3;
        [field: SerializeField] public Vector3 GhostEventCylinderSize { get; private set; } = new(3f, 1f, 3f);
        [field: SerializeField] public float GhostEventHeightOffset { get; private set; } = 1f;
        [field: Space]
        [field: SerializeField] public WaveForm EmfWaveForm { get; private set; } = WaveForm.Meander;
        [field: SerializeField] public float EmfVolume { get; private set; } = 0.1f;
        [field: Space]
        [field: SerializeField] public float Level2Tone { get; private set; } = 200f;
        [field: SerializeField] public float Level3Tone { get; private set; } = 350f;
        [field: SerializeField] public float Level4Tone { get; private set; } = 500f;
        [field: SerializeField] public float Level5Tone { get; private set; } = 700f;

    }
}