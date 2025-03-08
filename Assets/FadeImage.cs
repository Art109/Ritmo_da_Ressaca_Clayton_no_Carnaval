using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public Image targetImage; // Arraste a imagem do UI aqui no Inspector
    public float duration = 5f; // Tempo de cada fade (fade in e fade out)

    void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        yield return StartCoroutine(Fade(0f, 1f, duration, true)); // Fade In (0% -> 100%)
        yield return StartCoroutine(Fade(1f, 0f, duration, false)); // Fade Out (100% -> 0%)

        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator Fade(float startAlpha, float endAlpha, float duration, bool isFadingIn)
    {
        float elapsed = 0f;
        Color color = targetImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Suavização: usa SmoothStep para um fade mais natural
            float alpha = Mathf.SmoothStep(startAlpha, endAlpha, t);

            // Força transparência total no final do fade out
            if (!isFadingIn && alpha < 0.05f) alpha = 0f;

            targetImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Garante o valor final exato
        targetImage.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
