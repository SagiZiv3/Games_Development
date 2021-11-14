using UnityEngine;

namespace NewCode.PickableItems
{
    public class PickableGrenade : PickableItem
    {
        [SerializeField] private Grenade grenadePrefab;

        protected override void EffectPlayer(PickableItemsHandler player)
        {
            player.PickGrenade(grenadePrefab);
        }
    }
}