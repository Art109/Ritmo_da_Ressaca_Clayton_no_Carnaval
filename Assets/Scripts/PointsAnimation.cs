using TMPro;
using UnityEngine;
using DG.Tweening;

public class PointsAnimation : MonoBehaviour
{
    public float points = 300;
    public TextMeshProUGUI textPoints;

    public float animationDuration = 3f;

    private int lastDisplayedPoints = -1; // Armazena o último valor mostrado

    void Start()
    {
        AnimatePoints();
    }

    void AnimatePoints()
    {
        float currentPoints = 0;

        DOTween.To(() => currentPoints, x =>
        {
            currentPoints = x;
            int roundedPoints = Mathf.FloorToInt(currentPoints);

            // Só atualiza o texto e chama a animação se o número realmente mudou
            if (roundedPoints != lastDisplayedPoints)
            {
                textPoints.text = roundedPoints.ToString();
                AnimateScale();
                lastDisplayedPoints = roundedPoints; // Atualiza o último valor mostrado
            }
        }, points, animationDuration)
        .SetEase(Ease.OutQuad);
    }

    void AnimateScale()
    {
        textPoints.transform.DOScale(1.3f, 0.1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => textPoints.transform.DOScale(1f, 0.1f));
    }
}
