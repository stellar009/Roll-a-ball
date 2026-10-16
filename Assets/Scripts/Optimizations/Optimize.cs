using UnityEngine;
using UnityEngine.Rendering;

public class Optimize : MonoBehaviour
{
    private void Awake()
    {
        // Cap the game's frame rate at 60 FPS (saves battery/performance)
        Application.targetFrameRate = 60;

        // Enable SRP batching to reduce draw calls and improve rendering performance
        GraphicsSettings.useScriptableRenderPipelineBatching = true;
    }
}