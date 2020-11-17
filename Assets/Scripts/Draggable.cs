using System;
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
        
        Gate gate = gameObject.GetComponent<Gate>();
        if (gate == null)
            return;

        bool moved = false;

        foreach (Put gateOutput in gate.Outputs)
        {
            foreach (Wire wire in gateOutput.wires)
                if (wire.width < 0)
                {
                    transform.position = new Vector3(Math.Min(wire.a.transform.position.x, wire.b.transform.position.x) - gate.outputsX / 2,
                                    transform.position.y, transform.position.z);
                    moved = true;
                }
        }

        foreach (Put gateInput in gate.Inputs)
        {
            foreach (Wire wire in gateInput.wires)
                if (wire.width < 0)
                {
                    transform.position = new Vector3(Math.Max(wire.a.transform.position.x, wire.b.transform.position.x) - gate.inputsX / 2,
                                    transform.position.y, transform.position.z);
                    moved = true;
                }
        }

        if (moved)
        {
            foreach (Put gates in gate.Puts)
            {
                foreach (Wire wire in gates.wires)
                    wire.Move();
            }
        }
    }
}