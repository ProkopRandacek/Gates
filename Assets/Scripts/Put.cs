using System.Collections.Generic;
using UnityEngine;

public enum PutType { In, Out }

public class Put : MonoBehaviour // {in, out}put => put
{
    public new string     name = "";
    public     PutType    type;
    public     Gate       gate;
    public     List<Wire> wires;
    public     bool       connected;
    public     bool ?     Value = null;

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

    public void Highlight(bool on)
    {
        Color     clr = on ? Color.green : Color.gray;
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, clr);
        tex.Apply();
        _mr.material.SetTexture(MainTex, tex); 
    }
}
