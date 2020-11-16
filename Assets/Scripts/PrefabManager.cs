using UnityEngine;

public class PrefabManager: MonoBehaviour 
{
    public static PrefabManager Control; // self-reference

    private GameObject[] _prefabs;

    public GameObject Put
    {
        get { return _prefabs[0]; }
    }
    public GameObject Gate
    {
        get { return _prefabs[1]; }
    }

    void Start ()
    {
        Control = this; // linking the self-reference
    }
}