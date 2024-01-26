using UnityEngine;

namespace Utils
{
    public static class CameraUtils
    {
        #region --- Fields ---
        
        private static Camera _mainCamera;
        
        #endregion
        
        
        #region --- Properties ---

        public static Camera MainCamera
        {
            get
            {
                if(_mainCamera == null)
                {
                    _mainCamera = Camera.main;
                }
                return _mainCamera;
            }
        }
        
        #endregion
    }
}