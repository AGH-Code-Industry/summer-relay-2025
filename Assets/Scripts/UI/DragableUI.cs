using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform clampArea; // The area where the ticket can move (e.g. Canvas or Panel)

    private RectTransform rectTransform;
    public Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        //canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = false;
        Debug.Log("Begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.localPosition = ClampToArea(localPoint);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
    }

    private Vector3 ClampToArea(Vector3 targetPos)
    {
        if (clampArea == null)
            return targetPos;

        Vector3 clampedPos = targetPos;

        // Get clamp area bounds in local space
        Vector3 min = clampArea.rect.min;
        Vector3 max = clampArea.rect.max;

        // Half size of ticket
        Vector2 halfSize = rectTransform.rect.size * 0.5f;

        clampedPos.x = Mathf.Clamp(clampedPos.x, min.x + halfSize.x, max.x - halfSize.x);
        clampedPos.y = Mathf.Clamp(clampedPos.y, min.y + halfSize.y, max.y - halfSize.y);

        return clampedPos;
    }
}
