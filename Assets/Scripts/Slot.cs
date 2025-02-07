using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public string expectedTag; // La etiqueta esperada en este slot
    private GameObject currentElement; // Para rastrear el botón colocado

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            AudioManager.instance.PlaySoundSlotPlace();
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            currentElement = eventData.pointerDrag;
            CheckCorrectPlacement();
        }
    }
    public void CheckCorrectPlacement()
    {
        if (currentElement != null)
        {
            DraggableUI draggable = currentElement.GetComponent<DraggableUI>();
            if (draggable != null && draggable.GetButtonText() == expectedTag)
            {
                Debug.Log("¡Etiqueta correcta en el slot: " + expectedTag + "!");
            }
            else
            {
                Debug.Log("¡Etiqueta incorrecta en el slot: " + expectedTag + "!");
            }
        }
    }
    public GameObject GetCurrentElement()
    {
        return currentElement; // Devuelve el objeto que está en el slot
    }
}
