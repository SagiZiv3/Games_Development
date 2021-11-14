using System;
using UnityEngine;

namespace NewCode.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] protected Health.Health health;
        [SerializeField] protected internal Team team;
        [SerializeField] private bool isLeader = false;
        [SerializeField] protected UpdatesViewer updatesViewer;

        public float HealthPercentage => health.HealthPercentage;

        public virtual void Activate()
        {
            if (!gameObject.activeSelf)
                return;
            team.AddCharacter(this, isLeader);
            health.onHealthChanged.AddListener(OnHealthChanged);
        }

        public virtual void Pause()
        {
            enabled = false;
        }

        public virtual void Resume()
        {
            enabled = true;
        }

        private void OnHealthChanged(float newHealthPercentage)
        {
            if (newHealthPercentage == 0)
            {
                OnCharacterDied();
            }
        }

        protected virtual void OnCharacterDied()
        {
            updatesViewer.WriteCharacterDied(transform.name);
            health.onHealthChanged.RemoveListener(OnHealthChanged);
            enabled = false;
            GetComponent<Collider>().enabled = false;
            team.RemoveCharacter(this);
        }
    }
}