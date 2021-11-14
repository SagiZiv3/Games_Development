using UnityEngine;

namespace NewCode.PickableItems
{
    public class FoodProduct : PickableItem
    {
        [SerializeField] private float healAmount = 10f;

        protected override void EffectPlayer(PickableItemsHandler player)
        {
            player.Heal(healAmount);
        }
    }
}