using UnityEngine;

public class Board : MonoBehaviour //TODO
{
    public static Board Instance;

    public Transform topRight;
    public Transform bottomLeft;
    
    private void Awake()
    {
        Instance = this;
    }
}
