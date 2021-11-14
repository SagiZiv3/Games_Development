using UnityEngine;

namespace NewCode.Characters.Player.Input
{
    public abstract class InputHandler : ScriptableObject
    {
        [SerializeField] protected bool useRawInput;

        public abstract Vector3 GetMovementInput();
        public abstract Vector3 GetRotationInput();

        public abstract bool IsBoostKeyPressed();

        public abstract bool IsFireKeyPressed();
        public abstract bool IsGrenadeKeyPressed();
    }
}