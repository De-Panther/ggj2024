using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class Draggable3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private Vector3 position;
  private float timeCount = 0.0f;

  public void OnBeginDrag(PointerEventData eventData)
  {
    position = transform.position;
    Debug.Log("OnBeginDrag: " + position);
  }

  // Drag the selected item.
  public void OnDrag(PointerEventData data)
  {
    if (data.dragging)
    {
      // Object is being dragged.
      timeCount += Time.deltaTime;
      if (timeCount > 0.25f)
      {
        Debug.Log("Dragging:" + data.position);
        timeCount = 0.0f;
      }
    }
    position.x = data.position.x;
    position.y = data.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(position);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    position.x = eventData.position.x;
    position.y = eventData.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(position);
    Debug.Log("OnEndDrag: " + position);
  }
}
