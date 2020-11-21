using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// General logical gate class
/// </summary>
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
    
    public float heightOfTheFirstPot = -0.25f;
    public float inputsX             = -0.5f;
    public float outputsX            = 0.5f;

    public GameObject putGO;
    #endregion

    #region Unity Events

    private void Start()
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

    /// <summary>
    /// Safe way to add Put to gate
    /// </summary>
    /// <param name="type">Type of the put to be added</param>
    /// <param name="name">Name of the put to be added</param>
    public void AddPut(PutType type, string name = "", bool togglable = false)
    {
        GameObject go = Instantiate(putGO, transform);
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Put newPut = go.GetComponent<Put>();
        newPut.name      = name;
        newPut.type      = type;
        newPut.gate      = this;
        newPut.connected = false;
        newPut.wires     = new List<Wire>();
        newPut.value     = false;
        newPut.toggleable = togglable;
        if (type == PutType.In)
            _inputs.Add(newPut);
        else if (type == PutType.Out)
            _outputs.Add(newPut);
        else
            throw new Exception($"Unknown PutType value \"{type.ToString()}\"");
        if (_postStart) UpdatePutsPosition();
    }

    public void RemovePut(PutType type)
    {
        Put toRemove;
        if (type == PutType.In)
            toRemove = _inputs[_inputs.Count - 1];
        else
            toRemove = _outputs[_outputs.Count - 1];

        for (int i = 0; i < toRemove.wires.Count; i++)
        {
            toRemove.wires[i].Remove();
            i--;
        }

        if (type == PutType.In)
            _inputs.Remove(toRemove);
        else
            _outputs.Remove(toRemove);
        
        Destroy(toRemove.gameObject);
        UpdatePutsPosition();
    }
    
    /// <summary>
    /// Evaluates the gate output values based on the input values.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public virtual void Evaluate() { }
    
    #endregion

    #region Private Methods
    
    /// <summary>
    /// Override in Built-in gates to create Puts and set gate's name
    /// </summary>
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
