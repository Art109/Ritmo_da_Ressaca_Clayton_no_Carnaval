using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VectorGraphics;

public class SVGButtonSwap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler
{
    public Sprite hoverSprite;

    private SVGImage _targetSVG;
    private Sprite _normalSprite;
    private bool isSelected = false;

    void Start()
    {
        if (_targetSVG == null)
        {
            _targetSVG = GetComponent<SVGImage>();
            _normalSprite = _targetSVG.sprite;
        }

        _targetSVG.sprite = _normalSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
            _targetSVG.sprite = hoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected && EventSystem.current.currentSelectedGameObject != gameObject)
            _targetSVG.sprite = _normalSprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _targetSVG.sprite = hoverSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isSelected && EventSystem.current.currentSelectedGameObject != gameObject)
            _targetSVG.sprite = hoverSprite;
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        _targetSVG.sprite = hoverSprite;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        _targetSVG.sprite = _normalSprite;
    }
}
