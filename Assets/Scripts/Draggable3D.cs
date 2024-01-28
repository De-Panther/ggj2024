using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Progress;

public class Draggable3D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private Vector3 screenPosition;
  private bool dragging = false;
  private Vector3 startPosition;
  public float zLimit;

  private void Awake()
  {
    startPosition = transform.position;
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    screenPosition = transform.position;
    Vector3 cameraRelative = CameraUtils.MainCamera.transform.InverseTransformPoint(transform.position);
    screenPosition.z = cameraRelative.z;
    dragging = true;
  }

  // Drag the selected item.
  public void OnDrag(PointerEventData data)
  {
    screenPosition.x = data.position.x;
    screenPosition.y = data.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(screenPosition);
    OnStartedDrag();
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    screenPosition.x = eventData.position.x;
    screenPosition.y = eventData.position.y;
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(screenPosition);
    dragging = false;
    OnEndedDrag();
  }

  private void Update()
  {
    if (!dragging)
    {
      return;
    }
    transform.position = CameraUtils.MainCamera.ScreenToWorldPoint(screenPosition);
  }

  public void OnStartedDrag()
  {
    if (GameController.Instance.InProgress)
    {
      return;
    }
    GameController.Instance.StartGame();
  }

  public void OnEndedDrag()
  {
    if (transform.position.z > zLimit)
    {
      var position = transform.position;
      position.z = startPosition.z;
      transform.position = position;
    }
  }
}
