using UnityEngine;

public class ClearWindow : MonoBehaviour
{
  public RenderTexture windowTexture;

  private void OnEnable()
  {
    windowTexture.Release();
  }
}
