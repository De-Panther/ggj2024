using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

public class Draggable3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private Vector3 position;
  private bool dragging = false;

  public void OnBeginDrag(PointerEventData eventData)
  {
    position = transform.position;
    Vector3 cameraRelative = CameraUtils.MainCamera.transform.InverseTransformPoint(transform.position);
    position.z = cameraRelative.z;
    dragging = true;
  }

  // Drag the selected item.
  public void OnDrag(PointerEventData data)
  {
    position.x = data.position.x;
    position.y = data.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(position);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    position.x = eventData.position.x;
    position.y = eventData.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(position);
    dragging = false;
  }

  private void Update()
  {
    if (!dragging)
    {
      return;
    }
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(position);
  }
}
