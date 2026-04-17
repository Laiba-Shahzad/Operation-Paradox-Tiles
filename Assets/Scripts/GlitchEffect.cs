using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlitchEffect : MonoBehaviour
{
    public Image glitchOverlay;
    public RectTransform uiRoot; // Canvas root for shake

    public void TriggerGlitch(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(GlitchRoutine(duration));
    }

    IEnumerator GlitchRoutine(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Flicker overlay
            float alpha = Random.Range(0.1f, 0.5f);
            glitchOverlay.color = new Color(Random.value, 0f, Random.value, alpha);

            if (Random.value > 0.7f)
                FindFirstObjectByType<UIEffects>().effectText.text = "???";

            // UI jitter
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(-5f, 5f);
            uiRoot.anchoredPosition = new Vector2(x, y);

            yield return new WaitForSeconds(0.05f);

            elapsed += 0.05f;
        }

        // Reset
        glitchOverlay.color = new Color(1f, 0f, 1f, 0f);
        uiRoot.anchoredPosition = Vector2.zero;
    }
}