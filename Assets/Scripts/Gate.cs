using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    #region Variables
    private List<Put>     _inputs  = new List<Put>();
    private List<Put>     _outputs = new List<Put>();
    private PrefabManager _pm;
    private Text          _text;
    private string        _name;

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
    #endregion

    #region Unity Events
    private void Start()
    {
        _pm        = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        _text      = gameObject.GetComponentInChildren<Text>();
        _text.text = _name;
    }
    #endregion

    #region Public methods

    public void AddPut(PutType type, string name = "")
    {
        Put newPut = Instantiate(_pm.Put).GetComponent<Put>();
        newPut.name      = name;
        newPut.type      = type;
        newPut.gate      = this;
        newPut.connected = null;
        newPut.Value     = null;
        if (type == PutType.In)
            _inputs.Add(newPut);
        else if (type == PutType.Out)
            _outputs.Add(newPut);
        else
            throw new Exception($"Unknown PutType value \"{type.ToString()}\"");
        UpdatePutsPosition();
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
    private void UpdatePutsPosition()
    {
        //TODO
    }
    #endregion
}
