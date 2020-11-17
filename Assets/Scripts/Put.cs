using System.Collections.Generic;
using UnityEngine;

public enum PutType { In, Out }

/// <summary>
/// {in, out}put Class
/// </summary>
public class Put : MonoBehaviour
{
    public new string     name = "";
    public     PutType    type;
    public     Gate       gate;
    public     List<Wire> wires;
    public     bool       connected;
    public     bool       value = false;

    private MeshRenderer _mr;
    private Wireer       _wireer;
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        _mr     = gameObject.GetComponent<MeshRenderer>();
        _wireer = Wireer.Instance;
    }

    private void OnMouseDown()
    {
        _wireer.PutClicked(this);
    }

    /// <summary>
    /// Controls this put's highlighting. Used when clicked on a Put to highlight that it is selected
    /// </summary>
    public void Highlight(bool on)
    {
        Color     clr = on ? Color.magenta : Color.gray; // TODO not really visible difference
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, clr);
        tex.Apply();
        _mr.material.SetTexture(MainTex, tex); 
    }
}
