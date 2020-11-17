using System;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Put a;
    public Put b;

    private void Update()
    {
        Draggable aDrag = a.gate.gameObject.GetComponent<Draggable>();
        Draggable bDrag = b.gate.gameObject.GetComponent<Draggable>();

        if (aDrag.dragging || bDrag.dragging)
            Move();
    }

    public void Move()
    {
        var aPos = a.transform.position;
        var bPos = b.transform.position;
        transform.position = (bPos + aPos) / 2; // average
        float yDiff = bPos.y - aPos.y;
        float xDiff = bPos.x - aPos.x;
        float angle = (float) ((180 / Math.PI) * Math.Atan(yDiff / xDiff));
        transform.eulerAngles = new Vector3(0, 0, angle);
        float length = Vector3.Distance(bPos, aPos);
        transform.localScale = new Vector3(length, 1, 1);
    }
}
