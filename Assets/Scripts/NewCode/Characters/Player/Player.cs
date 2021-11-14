using NewCode.Characters.Player.Input;
using NewCode.Weapons;
using UnityEngine;

namespace NewCode.Characters.Player
{
    public class Player : Character
    {
        [SerializeField] private MovementHandler movementHandler;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private WeaponHandler weaponHandler;
        [SerializeField] private Transform eyes;
        [SerializeField] private float throwForce = 35f;
        [SerializeField] private float throwCooldownTime=1f;
        private float nextThrowTime = 0f;

        private void Update()
        {
            movementHandler.Move(inputHandler.GetMovementInput(), inputHandler.IsBoostKeyPressed());
            movementHandler.Rotate(inputHandler.GetRotationInput());
            UseWeapons();
        }

        private void UseWeapons()
        {
            if (weaponHandler.HasFirearm && inputHandler.IsFireKeyPressed())
            {
                weaponHandler.UseFirearm();
            }
            else if (weaponHandler.HasGrenade && Time.time > nextThrowTime && inputHandler.IsGrenadeKeyPressed())
            {
                weaponHandler.UseGrenade(eyes, throwForce);
                nextThrowTime = Time.time + throwCooldownTime;
            }
        }

        protected override void OnCharacterDied()
        {
            base.OnCharacterDied();
            GetComponent<Rigidbody>().isKinematic = true; // Make sure the player won't fall
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}