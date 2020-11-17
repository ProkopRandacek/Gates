using UnityEngine;

/// <summary>
/// Provides safe method to scale the gates without moving in space or any other unintended behaviour.
/// </summary>
public class BodyScaler : MonoBehaviour
{
    public RectTransform canvas;

    public void SetScale(float x, float y)
    {
        transform.localPosition = new Vector3((x - 1.0f) / 2.0f, (y - 1.0f) / 2.0f, 0.0f);
        canvas.localPosition    = new Vector3((x - 1.0f) / 2.0f, (y - 1.0f) / 2.0f, -1.0f);
        transform.localScale    = new Vector3(x,                 y,                 1.0f);
    }
}
