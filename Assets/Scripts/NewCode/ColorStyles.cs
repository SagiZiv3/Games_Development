using UnityEngine;

namespace NewCode
{
    [CreateAssetMenu(fileName = "Color Styles", menuName = "Game Logic/Color Styles", order = 0)]
    public class ColorStyles : ScriptableObject
    {
        [SerializeField] private Color nameColor = Color.blue;
        [SerializeField] private Color healthLostColor = Color.red;
        [SerializeField] private Color healthGainedColor = Color.green;
        [SerializeField] private Color deathColor = new Color32(255, 165, 0, 255);
        [SerializeField] private Color teamColor = Color.magenta;
        [SerializeField] private Color gunColor = Color.magenta;

        public string NameColor => ColorUtility.ToHtmlStringRGB(nameColor);
        public string HealthLostColor => ColorUtility.ToHtmlStringRGB(healthLostColor);
        public string HealthGainedColor => ColorUtility.ToHtmlStringRGB(healthGainedColor);
        public string DeathColor => ColorUtility.ToHtmlStringRGB(deathColor);
        public string TeamColor => ColorUtility.ToHtmlStringRGB(teamColor);
        public string GunColor => ColorUtility.ToHtmlStringRGB(gunColor);
    }
}