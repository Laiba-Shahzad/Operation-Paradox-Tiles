using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScreenManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    static bool isRestart = false; //  persists across scene reloads

    void Start()
    {
        if (isRestart)
        {
            // Skip start screen, jump straight into game
            startScreen.SetActive(false);
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isRestart = false; // reset for next time
        }
        else
        {
            // Normal first load — show start screen
            Time.timeScale = 0f;
            startScreen.SetActive(true);
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        Debug.Log("Start game!");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowWin()
    {
        FindFirstObjectByType<GameTimer>()?.StopTimer(); // ← add this
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowLose()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        isRestart = true; //  set BEFORE reload
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}