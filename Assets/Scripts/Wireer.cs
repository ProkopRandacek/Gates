using UnityEngine;

public class Wireer : MonoBehaviour
{
    public static Wireer     Instance;
    public        GameObject wire;

    private Put _selected;
    
    private void Awake()
    {
        Instance = this;
    }

    public void PutClicked(Put put)
    {
        if (put.connected)
            return;
        
        if (_selected == null)
        {
            _selected = put;
            put.Highlight(true);
            return;
        }

        if (_selected == put)
        {
            _selected.Highlight(false);
            _selected = null;
            return;
        }

        if (put.gate == _selected.gate || put.type == _selected.type)
            return;

        if (_selected.type == PutType.Out)
        {
            if (_selected.transform.position.x > put.transform.position.x)
                return;
        }
        else
            if (_selected.transform.position.x < put.transform.position.x)
                return;

        _selected.Highlight(false);
        put.Highlight(false);

        Wire w = Instantiate(wire, transform).GetComponent<Wire>();
        w.a = _selected;
        w.b = put;
        w.Move();

        if (put.type == PutType.Out)
        {
            put.wires.Add(w);
            _selected.connected = true;
            _selected.wires.Add(w);
        }
        else if (_selected.type == PutType.Out)
        {
            _selected.wires.Add(w);
            put.connected = true;
            put.wires.Add(w);
        }

        _selected = null;
    }
}