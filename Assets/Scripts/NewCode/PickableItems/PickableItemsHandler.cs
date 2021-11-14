using NewCode.Characters.Health;
using NewCode.PickableItems.Visualization;
using NewCode.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace NewCode.PickableItems
{
    public class PickableItemsHandler : MonoBehaviour
    {
        [SerializeField] private Transform eyes;
        [SerializeField] private Health health;
        [SerializeField] private WeaponHandler weaponsHandler;
        [SerializeField] private LayerMask itemsLayerMask;
        [SerializeField] private PickableItemViewer pickableItemViewer;
        [SerializeField] private float maxDistance = 4.5f;
        [SerializeField] private Image[] firearmImages;
        [SerializeField] private Image grenadeImage;


        public void Heal(float healAmount)
        {
            health.IncreaseHealth(healAmount);
        }

        public void PickFirearm(Firearm firearmPrefab)
        {
            weaponsHandler.SetFirearm(firearmPrefab);
            foreach (Image firearmImage in firearmImages)
            {
                firearmImage.enabled = firearmImage.name.StartsWith(firearmPrefab.WeaponName);
            }
        }

        public void PickGrenade(Grenade grenadePrefab)
        {
            weaponsHandler.SetGrenade(grenadePrefab);
            grenadeImage.enabled = true;
        }

        private void Update()
        {
            pickableItemViewer.Hide();
            if (Physics.Raycast(eyes.position, eyes.forward, out RaycastHit hitInfo, maxDistance,
                itemsLayerMask))
            {
                if (hitInfo.collider.TryGetComponent(out PickableItem pickableItem))
                {
                    pickableItemViewer.View(pickableItem);
                    if (pickableItem.CanPick())
                        pickableItem.Pickup(this);
                }
            }
        }
    }
}