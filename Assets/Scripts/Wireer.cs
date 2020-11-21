using UnityEngine;

/// <summary>
/// Wire-er. Handles wire spawning and puts highlighting in scene.
/// </summary>
public class Wireer : MonoBehaviour
{
    /// <summary>
    /// Self-reference
    /// </summary>
    public static Wireer instance;

    /// <summary>
    /// Prefab reference
    /// </summary>
    public GameObject wire;

    private Put _selected;
    
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Called from a put when it is clicked.
    /// </summary>
    /// <param name="put">The clicked put</param>
    public void PutClicked(Put put)
    {
        if (put.connected) // Don't select already connected puts
            return;
        
        if (_selected == null)
        {
            _selected = put;
            put.Highlight(true);
            return;
        }

        if (_selected == put) // When clicked the highlighted put, cancel the selection
        {
            _selected.Highlight(false);
            _selected = null;
            return;
        }

        if (put.gate == _selected.gate || put.type == _selected.type) // Don't connect Put of the same type on on the same gate
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
        w.transform.position += Vector3.back;

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