using Sirenix.OdinInspector;
using UnityEngine;

namespace ElectrumGames.Core.Ghost.Configs
{
    [CreateAssetMenu(fileName = "GhostFlickConfig", menuName = "Ghosts configs/Ghost Flick Config")]
    public class GhostFlickConfig : ScriptableObject
    {
        [SerializeField, MinMaxSlider(0f, 3f, true)] private Vector2 flickLessVisibilityVisible;
        [SerializeField, MinMaxSlider(0f, 3f, true)] private Vector2 flickLessVisibilityInvisible;
        [Space]
        [SerializeField, MinMaxSlider(0f, 3f, true)] private Vector2 flickMoreVisibilityVisible;
        [SerializeField, MinMaxSlider(0f, 3f, true)] private Vector2 flickMoreVisibilityInvisible;
        
        public float FlickLessVisibilityVisibleMin => flickLessVisibilityVisible.x;
        public float FlickLessVisibilityVisibleMax => flickLessVisibilityVisible.y;
        
        public float FlickLessVisibilityInvisibleMin => flickLessVisibilityInvisible.x;
        public float FlickLessVisibilityInvisibleMax => flickLessVisibilityInvisible.y;
        
        public float FlickMoreVisibilityVisibleMin => flickMoreVisibilityVisible.x;
        public float FlickMoreVisibilityVisibleMax => flickMoreVisibilityVisible.y;
        
        public float FlickMoreVisibilityInvisibleMin => flickMoreVisibilityInvisible.x;
        public float FlickMoreVisibilityInvisibleMax => flickMoreVisibilityInvisible.y;
    }
}