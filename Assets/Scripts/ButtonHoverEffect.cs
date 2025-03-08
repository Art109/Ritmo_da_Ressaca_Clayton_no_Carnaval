using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image sprite1; // Primeiro sprite
    public Image sprite2; // Segundo sprite
    public TextMeshProUGUI buttonText; // Texto do botão

    public Sprite normalSprite1; // Sprite normal do primeiro
    public Sprite normalSprite2; // Sprite normal do segundo
    public Color normalTextColor; // Cor normal do texto

    public Sprite hoverSprite1; // Sprite do primeiro no hover
    public Sprite hoverSprite2; // Sprite do segundo no hover
    public Color hoverTextColor; // Cor do texto no hover

    public Sprite clickedSprite1; // Sprite do primeiro ao clicar
    public Sprite clickedSprite2; // Sprite do segundo ao clicar
    public Color clickedTextColor; // Cor do texto ao clicar

    // Quando o mouse passa por cima do botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        sprite1.sprite = hoverSprite1;
        sprite2.sprite = hoverSprite2;
        buttonText.color = hoverTextColor;
    }

    // Quando o mouse sai do botão
    public void OnPointerExit(PointerEventData eventData)
    {
        sprite1.sprite = normalSprite1;
        sprite2.sprite = normalSprite2;
        buttonText.color = normalTextColor;
    }

    // Quando o botão é clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        sprite1.sprite = clickedSprite1;
        sprite2.sprite = clickedSprite2;
        buttonText.color = clickedTextColor;
    }
}
