using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public  bool    dragging = false;
    private Vector3 offset   = Vector3.zero;
    
    public Transform topRight;
    public Transform bottomLeft;

    private void OnMouseDown()
    {
        dragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        
        Gate gate = gameObject.GetComponent<Gate>();
        if (gate == null)
            return;


        foreach (Put gateOutput in gate.Outputs)
        {
            foreach (Wire wire in gateOutput.wires)
                if (wire.width < 0)
                    transform.position = new Vector3(Math.Min(wire.a.transform.position.x, wire.b.transform.position.x) - gate.outputsX * gate.transform.localScale.x,
                                    transform.position.y, transform.position.z);
        }

        foreach (Put gateInput in gate.Inputs)
        {
            foreach (Wire wire in gateInput.wires)
                if (wire.width < 0)
                    transform.position = new Vector3(Math.Max(wire.a.transform.position.x, wire.b.transform.position.x) - gate.inputsX * gate.transform.localScale.x,
                                    transform.position.y, transform.position.z);
        }

        // Check if the gate is inside the board and if not move it there
        if (topRight.transform.position.x > Board.Instance.topRight.transform.position.x)
            transform.position = new Vector3(Board.Instance.topRight.transform.position.x + transform.position.x - topRight.transform.position.x, transform.position.y, transform.position.z);
        if (topRight.transform.position.y > Board.Instance.topRight.transform.position.y)
            transform.position = new Vector3(transform.position.x, Board.Instance.topRight.transform.position.y + transform.position.y - topRight.transform.position.y, transform.position.z);
        
        if (bottomLeft.transform.position.x < Board.Instance.bottomLeft.transform.position.x)
            transform.position = new Vector3(Board.Instance.bottomLeft.transform.position.x + transform.position.x - bottomLeft.transform.position.x, transform.position.y, transform.position.z);
        if (bottomLeft.transform.position.y < Board.Instance.bottomLeft.transform.position.y)
            transform.position = new Vector3(transform.position.x, Board.Instance.bottomLeft.transform.position.y + transform.position.y - bottomLeft.transform.position.y, transform.position.z);

        foreach (Put gates in gate.Puts)
        {
            foreach (Wire wire in gates.wires)
                wire.Move();
        }
    }
}