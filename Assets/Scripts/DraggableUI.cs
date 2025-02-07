using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    public Vector2 originalPosition; // Guarda la posición original
    public string htmlTag; // Etiqueta HTML que representa este botón
    private TextMeshProUGUI buttonText;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
        buttonText = GetComponentInChildren<TextMeshProUGUI>(); // Obtiene el texto al iniciar
        //Debug.Log("Texto: "+ buttonText.text+" posición: "+originalPosition);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.instance.winLevel || !GameManager.instance.activeGame) return;
        canvasGroup.alpha = 0.6f; // Hace el objeto más transparente al arrastrar
        canvasGroup.blocksRaycasts = false; // Permite que otros objetos reciban eventos de raycast
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GameManager.instance.winLevel || !GameManager.instance.activeGame) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Ajusta la posición relativa al Canvas
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (GameManager.instance.winLevel || !GameManager.instance.activeGame) return;
        canvasGroup.alpha = 1.0f; // Restaura la opacidad
        canvasGroup.blocksRaycasts = true; // Vuelve a permitir raycasts

        if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<Slot>() == null)
        {
            //rectTransform.anchoredPosition = originalPosition;
        }
    }

    public string GetButtonText()
    {
        return buttonText != null ? buttonText.text : "Texto no encontrado";
    }
}
