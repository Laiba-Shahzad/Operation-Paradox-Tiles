using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public float timeMultiplier = 1f;
    public Text timerText;

    void Update()
    {
        timeRemaining -= Time.deltaTime * timeMultiplier;

        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0)
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}