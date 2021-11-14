using TMPro;
using UnityEngine;

namespace NewCode.PickableItems.Visualization
{
    public class UiPickableItemViewer : PickableItemViewer
    {
        [SerializeField] private TextMeshProUGUI descriptionLabel;

        protected override void ShowDescription(string description)
        {
            descriptionLabel.SetText(description);
            descriptionLabel.enabled = true;
        }

        public override void Hide()
        {
            descriptionLabel.enabled = false;
        }
    }
}