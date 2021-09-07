using UnityEngine;

public class FoodProduct : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private AudioClip sound;

    public string GetDescription()
    {
        return $"pick <color=green>{name}</color> to eat";
    }

    public void Eat()
    {
        gameObject.SetActive(false);
        // Sound
    }
}
