using UnityEngine;

namespace ElectrumGames.TestOnly
{
    public class FPSDisplay : MonoBehaviour
    {
        private float _deltaTime = 0.0f;
        private float _fps = 0.0f;
        private const float UpdateRate = 0.5f;
        private float _nextUpdate = 0.0f;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

            if (Time.time >= _nextUpdate)
            {
                _fps = 1.0f / _deltaTime;
                _nextUpdate = Time.time + UpdateRate;
            }
        }

        private void OnGUI()
        {
            var width = Screen.width;
            var height = Screen.height;
            var style = new GUIStyle();

            var rect = new Rect(0, 0, width, height * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = height * 2 / 50;
            style.normal.textColor = _fps switch
            {
                < 15 => Color.red,
                < 25 => new Color(1, 0.5f, 0),
                < 55 => Color.yellow,
                _ => Color.green
            };

            var text = $"{_fps:0.} FPS";
            GUI.Label(rect, text, style);
        }
    }
}