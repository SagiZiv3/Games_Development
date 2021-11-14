using System.Linq;
using NewCode.Characters;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewCode
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Team playerTeam;
        [SerializeField] private Team[] teams;
        [SerializeField] private TMP_Text gameOverLabel;
        [SerializeField] private TMP_Text replayText;
        [SerializeField] private CanvasGroup pauseMenu;
        private int numOfDefeatedTeams;
        private bool gamePaused, isGameOver;

        private void Start()
        {
            foreach (Team team in teams)
            {
                team.OnAllCharactersLeft += OnTeamDefeated;
            }

            playerTeam.OnAllCharactersLeft += OnPlayerLost;
        }

        private void OnDestroy()
        {
            foreach (Team team in teams)
            {
                team.OnAllCharactersLeft -= OnTeamDefeated;
                team.Clear();
            }

            playerTeam.OnAllCharactersLeft -= OnPlayerLost;
            playerTeam.Clear();
        }

        private void Update()
        {
            if (isGameOver && Input.GetKeyDown(KeyCode.Return))
            {
                ReloadGame();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGameOver)
                    QuitApplication();
                else
                {
                    // If the game is not paused, show the pause menu
                    if (!gamePaused)
                    {
                        PauseGame();
                    }
                    else
                    {
                        ResumeGame();
                    }
                }
            }
        }

        public void PauseGame()
        {
            gamePaused = true;
            // Free the cursor so the user can select item in the menu
            Cursor.lockState = CursorLockMode.None;
            // Show the menu
            pauseMenu.alpha = 1;
            pauseMenu.interactable = true;

            foreach (Team team in teams.Append(playerTeam))
            {
                team.SendPause();
            }
        }

        public void ResumeGame()
        {
            gamePaused = false;
            // Lock the cursor to the center of the screen
            Cursor.lockState = CursorLockMode.Locked;
            // Hide the menu
            pauseMenu.alpha = 0;
            pauseMenu.interactable = false;

            foreach (Team team in teams.Append(playerTeam))
            {
                team.SendResume();
            }
        }

        public void ReloadGame()
        {
            ResumeGame();
            // And load the current scene again
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void QuitApplication()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        private void OnPlayerLost()
        {
            gameOverLabel.SetText("!GAME OVER!\nYour team was defeated...");
            gameOverLabel.enabled = true;
            replayText.enabled = true;
            isGameOver = true;
        }

        private void OnPlayerWon()
        {
            gameOverLabel.SetText("!YOU WON!\nYOU defeated all the other teams");
            gameOverLabel.enabled = true;
            replayText.enabled = true;
        }

        private void OnTeamDefeated()
        {
            numOfDefeatedTeams += 1;
            if (numOfDefeatedTeams == teams.Length)
            {
                OnPlayerWon();
                isGameOver = true;
            }
        }
    }
}
