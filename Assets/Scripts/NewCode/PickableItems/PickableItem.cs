using UnityEngine;

namespace NewCode.PickableItems
{
    public abstract class PickableItem : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private KeyCode pickKeyCode = KeyCode.U;

        public string ItemName => itemName;
        public KeyCode PickKeyCode => pickKeyCode;

        public void Pickup(PickableItemsHandler player)
        {
            EffectPlayer(player);
            Destroy(gameObject);
        }

        public bool CanPick()
        {
            return Input.GetKeyDown(pickKeyCode);
        }

        protected abstract void EffectPlayer(PickableItemsHandler player);
    }
}