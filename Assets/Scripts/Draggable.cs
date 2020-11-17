using UnityEngine;

public class Draggable : MonoBehaviour
{
    public  bool    dragging = false;
    private Vector3 offset   = Vector3.zero;

    private void OnMouseDown()
    {
        dragging = true;
        Vector3 mousePos   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
    }

    private void OnMouseDrag()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = offset + mousePos;
        }
    }

    private void OnMouseUp()
    {
        dragging = false;
    }
}