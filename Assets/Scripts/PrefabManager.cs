using UnityEngine;

public class PrefabManager: MonoBehaviour 
{
    public static PrefabManager Instance; // self-reference

    public GameObject put;
    public GameObject gate;

    void Awake ()
    {
        Instance = this; // linking the self-reference
    }
}