using UnityEngine;

public class Wireer : MonoBehaviour
{
    public static Wireer     Instance;
    public        GameObject Wire;

    private Put _selected;
    
    private void Awake()
    {
        Instance = this;
    }

    public void PutClicked(Put put)
    {
        if (_selected == null)
        {
            _selected = put;
            put.Highlight(true);
            return;
        }

        if (put.gate == _selected.gate)
            return;

        _selected.Highlight(false);
        put.Highlight(false);

        Wire wire      = Instantiate(Wire, transform).GetComponent<Wire>();
        wire.a = _selected;
        wire.b = put;
        wire.Move();

        _selected = null;
    }
}