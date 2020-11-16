using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool    _dragging = false;
    private Vector3 offset    = Vector3.zero;

    private void OnMouseDown()
    {
        _dragging = true;
        Vector3 mousePos   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
    }

    private void OnMouseDrag()
    {
        if (_dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = offset + mousePos;
        }
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }
}