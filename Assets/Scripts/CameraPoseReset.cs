using UnityEngine;

public class CameraPoseReset : MonoBehaviour
{
  [SerializeField]
  private Vector3 _startPosition;
  [SerializeField]
  private Quaternion _startRotation;
  [SerializeField]
  private Camera _camera;
  private float _fieldOfView = 60;

  public void DoResetPose()
  {
    transform.SetLocalPositionAndRotation(_startPosition, _startRotation);
    _camera.fieldOfView = _fieldOfView;
    _camera.ResetProjectionMatrix();
  }
}
