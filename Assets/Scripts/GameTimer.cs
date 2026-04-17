using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public float timeMultiplier = 1f;
    public Text timerText;

    private bool gameOver = false; // ← add this

    void Start()
    {
        gameOver = false;          // ← reset on scene load
        timeMultiplier = 1f;       // ← reset multiplier
    }

    void Update()
    {
        if (gameOver) return;      // ← stop running after loss
        if (Time.timeScale == 0f) return; // ← don't tick during UI screens

        timeRemaining -= Time.deltaTime * timeMultiplier;
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0)
        {
            gameOver = true;       // ← set BEFORE calling ShowLose
            Debug.Log("Game Over");
            AudioManager.Instance.PlaySound(AudioManager.Instance.loseSound);
            FindFirstObjectByType<UIScreenManager>().ShowLose();
        }
    }

    public void StopTimer()        // ← call this from ShowWin() too
    {
        gameOver = true;
    }
}