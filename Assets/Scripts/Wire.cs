using System;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Put a;
    public Put b;
    public MeshRenderer mr;

    private Draggable    _aDrag;
    private Draggable    _bDrag;
    private Texture2D    _on;
    private Texture2D    _off;
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

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

    private void Start()
    {
        _aDrag = a.gate.gameObject.GetComponent<Draggable>();
        _bDrag = b.gate.gameObject.GetComponent<Draggable>();
        
        _on    = new Texture2D(1, 1);
        _off   = new Texture2D(1, 1);
        _on.SetPixel(0, 0, Color.yellow);
        _off.SetPixel(0, 0, Color.black);
        _on.Apply();
        _off.Apply();
        mr.material.SetTexture(MainTex, _off);
    }

    private void Update()
    {
        if (_aDrag.dragging || _bDrag.dragging)
            Move();
        if (a.type == PutType.Out)
            b.value = a.value;
        if (b.type == PutType.Out)
            a.value = b.value;

        mr.material.SetTexture(MainTex, a.value ? _on : _off);
    }

    /// <summary>
    /// Updates wire position, scale and rotation to connect Input and Output.
    /// </summary>
    public void Move()
    {
        var aPos = a.transform.position;
        var bPos = b.transform.position;
        transform.position = (bPos + aPos) / 2; // average position
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

        a.value = false;
        b.value = false;
        
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
