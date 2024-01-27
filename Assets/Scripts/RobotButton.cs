using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Progress;

public class RobotButton : MonoBehaviour
{
  [SerializeField]
  private GameObject buttonVisual;

  void Start()
  {
    buttonVisual.SetActive(false);
  }

  void OnEnable()
  {
    GameController.OnGameEnd += HandleOnGameEnd;
  }

  void OnDisable()
  {
    GameController.OnGameEnd += HandleOnGameEnd;
  }

  void HandleOnGameEnd()
  {
    buttonVisual.SetActive(true);
  }

  public void OnButtonSelect()
  {
    // todo: reset level
    Debug.LogError("OnButtonSelect");
  }
}
