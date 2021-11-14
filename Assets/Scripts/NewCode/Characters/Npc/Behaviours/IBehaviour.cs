namespace NewCode.Characters.Npc.Behaviours
{
    public interface IBehaviour
    {
        void OnEnter();
        void Handle();
        void OnExit();
        void Pause();
        void Resume();
    }
}