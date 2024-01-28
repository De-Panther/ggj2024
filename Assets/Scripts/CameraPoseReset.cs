using UnityEngine;

public class CameraPoseReset : MonoBehaviour
{
  private Vector3 startPosition;
  private Quaternion startRotation;

  private void Awake()
  {
    startPosition = transform.localPosition;
    startRotation = transform.localRotation;
  }

  public void ResetPose()
  {
    transform.SetLocalPositionAndRotation(startPosition, startRotation);
  }
}
