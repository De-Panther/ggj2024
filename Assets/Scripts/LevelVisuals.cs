using UnityEngine;
using TMPro;
using Progress;

public class LevelVisuals : MonoBehaviour
{
  [SerializeField]
  private TMP_Text _score;
  [SerializeField]
  private GameObject _scoreVisual;

  void OnEnable()
  {
    GameController.OnGameEnd += HandleOnGameEnd;
    GameController.OnGameReset += HandleOnGameReset;
  }

  void OnDisable()
  {
    GameController.OnGameEnd -= HandleOnGameEnd;
    GameController.OnGameReset -= HandleOnGameReset;
  }

  void HandleOnGameEnd()
  {
    _score.text = GameController.Instance.GetScore().ToString();
    _scoreVisual.SetActive(true);
  }

  void HandleOnGameReset()
  {
    _scoreVisual.SetActive(false);
  }
}
