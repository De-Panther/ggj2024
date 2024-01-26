using UnityEngine;
using Utils;

namespace Interactables.Gameplay
{
    public class WindowCleaner : MonoBehaviour
    {
        // Reference to the render texture
        public RenderTexture cleaningArea;

        // Reference to the window object
        public GameObject window;

        private Camera cleaningAreaCam;
        private float cleaningRadius = 0.01f;

        void Start()
        {
            // Assumes there is a camera pointed at the Render Texture.
            // This camera should have its Clear Flags set to Solid Color, and
            // it should have an alpha of 1 (completely white, completely opaque)
            cleaningAreaCam = CameraUtils.MainCamera;
        }

        void Update()
        {
            // Check if we are cleaning
            if ( true )
            {
                Vector2 relativePosition = CalculateRobotPositionRelativeToWindow();

                // Switch to the Render Texture
                RenderTexture previousActiveRT = RenderTexture.active;
                RenderTexture.active = cleaningArea;

                // Clear the RenderTexture at the position of the robot
                GL.PushMatrix();
                GL.LoadPixelMatrix();
                GL.Color(new Color(0f, 0f, 0f, 0f)); // clear color with 0 alpha
                GL.Begin(GL.QUADS);
                GL.Vertex3(relativePosition.x - cleaningRadius, relativePosition.y - cleaningRadius, 0);
                GL.Vertex3(relativePosition.x + cleaningRadius, relativePosition.y - cleaningRadius, 0);
                GL.Vertex3(relativePosition.x + cleaningRadius, relativePosition.y + cleaningRadius, 0);
                GL.Vertex3(relativePosition.x - cleaningRadius, relativePosition.y + cleaningRadius, 0);
                GL.End();
                GL.PopMatrix();

                // Restore previous active RenderTexture
                RenderTexture.active = previousActiveRT;
            }
        }

        private Vector2 CalculateRobotPositionRelativeToWindow()
        {
            Vector3 windowPosition = window.transform.InverseTransformPoint(transform.position);
            return cleaningAreaCam.WorldToViewportPoint(windowPosition);
        }
    }
}