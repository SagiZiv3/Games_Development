using NewCode.Weapons;
using UnityEngine;

namespace NewCode.PickableItems
{
    public class PickableFirearm : PickableItem
    {
        [SerializeField] private Firearm firearmPrefab;

        public Firearm FirearmPrefab => firearmPrefab;

        protected override void EffectPlayer(PickableItemsHandler player)
        {
            player.PickFirearm(firearmPrefab);
        }
    }
}