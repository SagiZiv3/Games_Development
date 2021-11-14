using UnityEngine;

namespace NewCode.PickableItems.Visualization
{
    public abstract class PickableItemViewer : MonoBehaviour
    {
        [SerializeField] private string descriptionFormat = "Click <color={0}>{2}</color> to pick <color={1}>{3}</color>";
        [SerializeField] private Color keyColor = Color.cyan;
        [SerializeField] private Color itemNameColor = new Color(1f, 0.65f, 0f);


        public void View(PickableItem item)
        {
            string keyColorStr = string.Concat("#", ColorUtility.ToHtmlStringRGB(keyColor));
            string itemNameColorStr = string.Concat("#", ColorUtility.ToHtmlStringRGB(itemNameColor));
            string description = string.Format(descriptionFormat, keyColorStr, itemNameColorStr, item.PickKeyCode,
                item.ItemName);
            ShowDescription(description);
        }
        public abstract void Hide();
        protected abstract void ShowDescription(string description);
    }
}