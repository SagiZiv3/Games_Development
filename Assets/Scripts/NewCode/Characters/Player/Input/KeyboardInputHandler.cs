using System;
using UnityEngine;

namespace NewCode.Characters.Player.Input
{
    [CreateAssetMenu(fileName = "Input Handler", menuName = "Game Logic/Input Handler", order = 0)]
    public class KeyboardInputHandler : InputHandler
    {
        [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode fireKeyCode = KeyCode.Space;
        [SerializeField] private KeyCode grenadeKeyCode = KeyCode.Q;

        public override Vector3 GetMovementInput()
        {
            Func<string, float> inputFunction;
            if (useRawInput)
                inputFunction = UnityEngine.Input.GetAxisRaw;
            else
                inputFunction = UnityEngine.Input.GetAxis;
            return new Vector3(inputFunction("Horizontal"), 0f, inputFunction("Vertical"));
        }

        public override Vector3 GetRotationInput()
        {
            Func<string, float> inputFunction;
            if (useRawInput)
                inputFunction = UnityEngine.Input.GetAxisRaw;
            else
                inputFunction = UnityEngine.Input.GetAxis;
            return new Vector3(inputFunction("Mouse X"), -inputFunction("Mouse Y"));
        }

        public override bool IsBoostKeyPressed()
        {
            return UnityEngine.Input.GetKey(boostKey);
        }

        public override bool IsFireKeyPressed()
        {
            return UnityEngine.Input.GetKey(fireKeyCode);
        }

        public override bool IsGrenadeKeyPressed()
        {
            return UnityEngine.Input.GetKey(grenadeKeyCode);
        }
    }
}