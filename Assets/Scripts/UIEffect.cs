using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffects : MonoBehaviour
{
    public Text effectText;
    public Image overlay;

    public void ShowEffect(string message, Color color)
    {
        StopAllCoroutines();
        StartCoroutine(EffectRoutine(message, color));
    }

    IEnumerator EffectRoutine(string message, Color color)
    {
        // Text
        effectText.text = message;

        // Overlay flash
        overlay.color = new Color(color.r, color.g, color.b, 0.4f);

        yield return new WaitForSeconds(0.3f);

        overlay.color = new Color(color.r, color.g, color.b, 0f);

        yield return new WaitForSeconds(1f);

        effectText.text = "";
    }
}