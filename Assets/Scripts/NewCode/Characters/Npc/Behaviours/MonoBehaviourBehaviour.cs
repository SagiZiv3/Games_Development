using UnityEngine;

namespace NewCode.Characters.Npc.Behaviours
{
    public abstract class MonoBehaviourBehaviour : MonoBehaviour, IBehaviour
    {
        public abstract void OnEnter();
        public abstract void Handle();
        public abstract void OnExit();
        public abstract void Pause();
        public abstract void Resume();
    }
}