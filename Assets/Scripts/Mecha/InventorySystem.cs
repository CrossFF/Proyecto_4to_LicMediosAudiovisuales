using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler,IPointerExitHandler
{
    //parametro
    public SystemMecha system;
    /// referencias
    public CanvasGroup canvasGroup;
    public Text nombreSistema;
    public Image imagenSistema;
    public Canvas canvas;
    public RectTransform rectTransform;
    public Transform parent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.parent = canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.transform.tag == "Mecha")
            {
                PartGameObject part = hit.transform.GetComponent<PartGameObject>();
                if(part.Equiped())
                {
                    if (part.CheckSystemCapacity())
                    {
                        part.SetSystem(system);
                    }
                }
            }
        }
        canvasGroup.blocksRaycasts = true;
        transform.parent = parent;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.8f;
    }
}
