using System;
using UnityEngine;

/// <summary>
/// Makes a gameobject draggable.
/// </summary>
public class Draggable : MonoBehaviour
{
    public  bool    dragable = true;
    public  bool    dragging = false;
    private Vector3 offset   = Vector3.zero;
    
    public Transform topRight;
    public Transform bottomLeft;

    private void OnMouseDown()
    {
        if (!dragable)
            return;
        dragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
    }

    private void OnMouseDrag()
    {
        if (!dragable)
            return;
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = offset + mousePos;
        }
    }

    private void OnMouseUp()
    {
        if (!dragable)
            return;
        dragging = false;
        
        // Following code is for gates only (in case there are any non-gate draggable objects in the future)
        Gate gate = gameObject.GetComponent<Gate>();
        if (gate == null)
            return;


        // Check if the gate is to the left from all the Gates connected to its Outputs and if not move it there
        foreach (Put gateOutput in gate.Outputs)
        {
            foreach (Wire wire in gateOutput.wires)
                if (wire.Width < 0)
                    transform.position = new Vector3(Math.Min(wire.a.transform.position.x, wire.b.transform.position.x) - gate.outputsX * gate.transform.localScale.x,
                                    transform.position.y, transform.position.z);
        }

        // Check if the gate is to the right from all the Gates connected to its Inputs and if not move it there
        foreach (Put gateInput in gate.Inputs)
        {
            foreach (Wire wire in gateInput.wires)
                if (wire.Width < 0)
                    transform.position = new Vector3(Math.Max(wire.a.transform.position.x, wire.b.transform.position.x) - gate.inputsX * gate.transform.localScale.x,
                                    transform.position.y, transform.position.z);
        }

        // Check if the gate is inside the board and if not move it there
        if (topRight.transform.position.x > Board.instance.topRight.transform.position.x)
            transform.position = new Vector3(Board.instance.topRight.transform.position.x + transform.position.x - topRight.transform.position.x, transform.position.y, transform.position.z);
        if (topRight.transform.position.y > Board.instance.topRight.transform.position.y)
            transform.position = new Vector3(transform.position.x, Board.instance.topRight.transform.position.y + transform.position.y - topRight.transform.position.y, transform.position.z);
        
        if (bottomLeft.transform.position.x < Board.instance.bottomLeft.transform.position.x)
            transform.position = new Vector3(Board.instance.bottomLeft.transform.position.x + transform.position.x - bottomLeft.transform.position.x, transform.position.y, transform.position.z);
        if (bottomLeft.transform.position.y < Board.instance.bottomLeft.transform.position.y)
            transform.position = new Vector3(transform.position.x, Board.instance.bottomLeft.transform.position.y + transform.position.y - bottomLeft.transform.position.y, transform.position.z);

        // Update wire positions
        foreach (Put gates in gate.Puts)
        {
            foreach (Wire wire in gates.wires)
                wire.Move();
        }
    }
}