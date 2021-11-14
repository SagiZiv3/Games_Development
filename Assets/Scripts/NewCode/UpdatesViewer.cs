using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NewCode
{
    public class UpdatesViewer : MonoBehaviour
    {
        [SerializeField] private TMP_Text updatesLabel;
        [SerializeField] private KeyCode minimizeButton = KeyCode.M;
        [SerializeField] private ColorStyles colorStyles;
        [SerializeField] private byte maxMessages = 10;
        private List<string> messages;
        private bool isMinimized = false;
        private Vector2 originalSize;
        private Coroutine running;

        private void Awake()
        {
            messages = new List<string>(maxMessages);
            originalSize = updatesLabel.rectTransform.sizeDelta;
        }

        public void WriteHealthLost(string characterName, float delta)
        {
            string message =
                $"<color=#{colorStyles.NameColor}>{characterName}</color> lost <color=#{colorStyles.HealthLostColor}>{delta:F2} HP</color>";
            WriteUpdate(message);
        }

        public void WriteHealthGained(string characterName, float delta)
        {
            string message =
                $"<color=#{colorStyles.NameColor}>{characterName}</color> healed by <color=#{colorStyles.HealthGainedColor}>{delta:F2} HP</color>";
            WriteUpdate(message);
        }

        public void WriteCharacterDied(string characterName)
        {
            string message =
                $"<b><color=#{colorStyles.NameColor}>{characterName}</color> <color=#{colorStyles.DeathColor}>died</color></b>";
            WriteUpdate(message);
        }

        public void WriteCharacterPickedGun(string characterName, string gunName)
        {
            string message =
                $"<b><color=#{colorStyles.NameColor}>{characterName}</color> picked gun: <color=#{colorStyles.GunColor}>{gunName}</color></b>";
            WriteUpdate(message);
        }

        public void WriteNewLeader(string characterName, string teamName)
        {
            string message =
                $"<color=#{colorStyles.NameColor}>{characterName}</color> is now the leader of <color=#{colorStyles.TeamColor}>{teamName}</color>";
            WriteUpdate(message);
        }

        private void WriteUpdate(string message)
        {
            messages.Add(message);
            if (messages.Count > maxMessages)
            {
                messages.RemoveAt(0);
            }

            UpdateUpdatesLabel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(minimizeButton))
            {
                isMinimized = !isMinimized;
                Vector2 smallSize = originalSize;
                smallSize.y /= maxMessages;
                if (running != null)
                    StopCoroutine(running);
                running = StartCoroutine(UpdateSize(isMinimized ? smallSize : originalSize));
            }
        }

        private IEnumerator UpdateSize(Vector2 desiredSize)
        {
            float timePassed = 0f;
            float animationTime = 0.3f;
            Vector2 startSize = updatesLabel.rectTransform.sizeDelta;
            while (timePassed < animationTime)
            {
                updatesLabel.rectTransform.sizeDelta = Vector2.Lerp(startSize, desiredSize, timePassed / animationTime);
                yield return null;
                timePassed += Time.deltaTime;
            }

            updatesLabel.rectTransform.sizeDelta = desiredSize;
            UpdateUpdatesLabel();
        }

        private void UpdateUpdatesLabel()
        {
            if (!isMinimized)
                updatesLabel.SetText(string.Join("\n\n", messages));
            else if (messages.Count > 0)
                updatesLabel.SetText(messages[messages.Count - 1]);
        }
    }
}