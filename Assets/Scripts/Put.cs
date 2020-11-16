using UnityEngine;

public enum PutType { In, Out }

public class Put : MonoBehaviour // {in, out}put => put
{
    public new string  name = "";
    public     PutType type;
    public     Gate    gate;
    public     Put     connected;
    public     bool ?  Value = null;
}
