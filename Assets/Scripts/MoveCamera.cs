using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
  [SerializeField]
  private Transform target;
  [SerializeField]
  private float speed = 1f;
  [SerializeField]
  private float rotationSpeed = 1f;
  [SerializeField]
  private InputActionProperty enableMove;
  [SerializeField]
  private InputActionProperty forward;
  [SerializeField]
  private InputActionProperty backward;
  [SerializeField]
  private InputActionProperty right;
  [SerializeField]
  private InputActionProperty left;
  [SerializeField]
  private InputActionProperty cursorDelta;

  void Update()
  {
    if (enableMove.action?.IsPressed() == false)
    {
      return;
    }
    Vector3 direction = Vector3.zero;
    if (forward.action?.IsPressed() == true)
    {
      direction.z += 1f;
    }
    if (backward.action?.IsPressed() == true)
    {
      direction.z -= 1f;
    }
    if (right.action?.IsPressed() == true)
    {
      direction.x += 1f;
    }
    if (left.action?.IsPressed() == true)
    {
      direction.x -= 1f;
    }
    Vector2 rotation = cursorDelta.action.ReadValue<Vector2>() * rotationSpeed * Time.deltaTime;
    target.Rotate(-rotation.y, rotation.x, 0, Space.Self);
    Vector3 euler = target.localEulerAngles;
    euler.z = 0;
    target.localEulerAngles = euler;
    target.Translate(direction * speed * Time.deltaTime, Space.Self);
  }
}
