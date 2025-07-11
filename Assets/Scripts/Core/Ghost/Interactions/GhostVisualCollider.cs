﻿using ElectrumGames.Core.Common;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Interactions
{
    public class GhostVisualCollider : MonoBehaviour, IGhostCollider
    {
        public IHaveVisibility HaveVisibility { get; private set; }

        private void Start()
        {
            HaveVisibility = transform.parent.GetComponent<IHaveVisibility>();
        }
    }
}