using UnityEngine;

namespace ElectrumGames.Core.Player.Movement
{
    public interface IInput
    {
        Vector2 Movement { get; }
        Vector2 Look { get; }
        bool Sprint { get; }
        bool IsCrouch { get; }

        void Init();
        void Update (float deltaTime);
    }
}