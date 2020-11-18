﻿using System;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Put a;
    public Put b;

    /// <summary>
    /// Distance from Output to Input. When negative, the gate position is very wrong.
    /// </summary>
    public float Width
    {
        get
        {
            if (a.type == PutType.In)
                return a.transform.position.x - b.transform.position.x;
            else
                return b.transform.position.x - a.transform.position.x;
        }
    }

    private void Update()
    {
        Draggable aDrag = a.gate.gameObject.GetComponent<Draggable>();
        Draggable bDrag = b.gate.gameObject.GetComponent<Draggable>();

        if (aDrag.dragging || bDrag.dragging)
            Move();
    }

    /// <summary>
    /// Updates wire position, scale and rotation to connect Input and Output.
    /// </summary>
    public void Move()
    {
        var aPos = a.transform.position;
        var bPos = b.transform.position;
        transform.position = (bPos + aPos) / 2; // average
        float yDiff = bPos.y - aPos.y;
        float xDiff = bPos.x - aPos.x;
        float angle = (float) ((180 / Math.PI) * Math.Atan(yDiff / xDiff));
        transform.eulerAngles = new Vector3(0, 0, angle);
        float length = Vector2.Distance(bPos, aPos);
        transform.localScale = new Vector3(length, 1, 1);
    }

    public void Remove()
    {
        if (a.type == PutType.In)
        {
            a.connected = false;
            a.wires.Remove(this);
        }
        else
            a.wires.Remove(this);

        if (b.type == PutType.In)
        {
            b.connected = false;
            b.wires.Remove(this);
        }
        else
            b.wires.Remove(this);
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Wire is destroyed on click.
    /// </summary>
    private void OnMouseUp()
    {
        Remove();
    }
}
