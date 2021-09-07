using System;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private KeyCode key = KeyCode.U;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private float maxPickDistance = 0.5f;

    private void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward,
            out RaycastHit hitInfo, maxPickDistance, layerMask))
        {
            if (hitInfo.collider.TryGetComponent(out FoodProduct foodProduct))
            {
                if (!promptText.gameObject.activeSelf)
                {
                    promptText.gameObject.SetActive(true);
                }
                promptText.SetText($"Press {key} to {foodProduct.GetDescription()}");
                if (Input.GetKeyDown(key))
                {
                    foodProduct.Eat();
                }
            }
            else if (promptText.gameObject.activeSelf)
            {
                promptText.gameObject.SetActive(false);
            }
        }
        else if (promptText.gameObject.activeSelf)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        if (mainCamera != null && maxPickDistance > mainCamera.farClipPlane)
        {
            maxPickDistance = mainCamera.farClipPlane;
        }
    }
}
