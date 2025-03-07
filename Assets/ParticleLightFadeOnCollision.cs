using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleLightFadeOnCollision : MonoBehaviour
{
    private Light pointLight;
    private bool isFading = false;
    public float fadeDuration = 600f; // Tempo para a cor desaparecer (600 segundos)
    public Color targetColor = Color.black; // Cor final da luz

    private void Start()
    {
        pointLight = GetComponent<Light>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!isFading && other.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(FadeOutColor());
        }
    }

    private IEnumerator FadeOutColor()
    {
        isFading = true;
        Color startColor = pointLight.color;
        float time = 0;

        while (time < fadeDuration)
        {
            pointLight.color = Color.Lerp(startColor, targetColor, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        pointLight.color = targetColor; // Garante que a cor final seja aplicada
        isFading = false;
    }
}
