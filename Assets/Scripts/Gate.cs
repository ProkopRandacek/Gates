using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    #region Variables

    private List<Put>     _inputs  = new List<Put>();
    private List<Put>     _outputs = new List<Put>();
    private BodyScaler    _bs;
    private Text          _text;
    private string        _name;
    private bool          _postStart = false;

    public string Name
    {
        get { return _name; }
        set
        {
            _name       = value;
            _text.text = value;
        }
    }
    public List<Put> Inputs
    {
        get { return _inputs; }
    }
    public List<Put> Outputs
    {
        get { return _outputs; }
    }

    public List<Put> Puts
    {
        get
        {
            List<Put> l = new List<Put>();
            l.AddRange(Inputs);
            l.AddRange(Outputs);
            return l;
        }
    }
    public bool Ready // Does this gate has values on all the inputs?
    {
        get
        {
            foreach (Put input in _inputs)
            {
                if (input.Value == null)
                    return false;
                if (input.Value == false)
                    return false;
            }

            return true;
        }
    }
    
    public float heightOfTheFirstPot = -0.25f;
    public float inputsX             = -0.5f;
    public float outputsX            = 0.5f;

    public GameObject putGO;
    #endregion

    #region Unity Events

    private void Start() // This is called between Awake and Start
    {
        _text      = gameObject.GetComponentInChildren<Text>();
        _bs        = gameObject.GetComponentInChildren<BodyScaler>();
        _text.text = _name;

        PostStart();

        _postStart = true;
        UpdatePutsPosition();
    }

    #endregion

    #region Public methods

    public void AddPut(PutType type, string name = "")
    {
        GameObject go = Instantiate(putGO, transform);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Put newPut = go.GetComponent<Put>();
        newPut.name        = name;
        newPut.type        = type;
        newPut.gate        = this;
        newPut.connected   = false;
        newPut.wires = new List<Wire>();
        newPut.Value       = null;
        if (type == PutType.In)
            _inputs.Add(newPut);
        else if (type == PutType.Out)
            _outputs.Add(newPut);
        else
            throw new Exception($"Unknown PutType value \"{type.ToString()}\"");
        if (_postStart) UpdatePutsPosition();
    }

    public List<bool> Evaluate(List<bool> inputs)
    {
        if (inputs.Count != Inputs.Count)
            throw new Exception($"Wrong number of input when evaluating a gate {inputs.Count} instead of {Inputs.Count}");
        
        //TODO
        return inputs;
    }
    #endregion

    #region Private Methods
    protected virtual void PostStart() { }

    private void UpdatePutsPosition()
    {
        float height = Math.Max(_inputs.Count, _outputs.Count) * 0.5f;
        _bs.SetScale(1.0f, height);
        for (int i = 0; i < _inputs.Count; i++)
            _inputs[i].gameObject.transform.localPosition =
                new Vector3(inputsX, heightOfTheFirstPot + (i / 2.0f), -1.0f);
        for (int i = 0; i < _outputs.Count; i++)
            _outputs[i].gameObject.transform.localPosition =
                new Vector3(outputsX, heightOfTheFirstPot + (i / 2.0f), -1.0f);
    }

    #endregion
}
