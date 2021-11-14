using NewCode.Characters;
using NewCode.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace NewCode
{
    public class GameStarter : MonoBehaviour
    {
        [FormerlySerializedAs("weaponsHandler")] [SerializeField] private WeaponsPlacer weaponsPlacer;
        [SerializeField] private Character[] characters;

        private void Start()  // TODO: Activate only when game starts (like button press)
        {
            weaponsPlacer.PlaceWeapons();
            foreach (Character character in characters)
            {
                character.Activate();
            }
        }
    }
}