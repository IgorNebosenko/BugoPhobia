using ElectrumGames.Core.Environment;
using UnityEngine;

namespace ElectrumGames.Core.Boards
{
    public class BoardEnvironmentObject : EnvironmentObjectBase
    {
        [SerializeField] private bool isMainBoard;
        [SerializeField] private BoardsHandler boardsHandler;
        [field: Space]
        [field: SerializeField] public Collider BoardCollider { get; set; }

        public override void OnInteract()
        {
            if (isMainBoard)
                boardsHandler.OnMainBoardClicked();
            else
                boardsHandler.OnAdditionalBoardClicked();
        }
    }
}