using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageFillManager : MonoBehaviour
{
    public static UIImageFillManager Instance { get; private set; }

    // UI
    [Header("UI - Player")]
    [SerializeField] private Image glitterImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateGlitterImage(float value)
    {
        glitterImage.fillAmount = value;
    }
}
