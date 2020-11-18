using UnityEngine;

public class Board : MonoBehaviour //TODO
{
    public static Board instance;

    public Transform topRight;
    public Transform bottomLeft;
    
    private void Awake()
    {
        instance = this;
    }
}
