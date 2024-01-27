using UnityEngine;

public class ClearWindow : MonoBehaviour
{
  public RenderTexture windowTexture;

  void Start()
  {
    windowTexture.Release();
  }
}
