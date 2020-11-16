using UnityEditor.PackageManager;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            #region cast()
            Vector3 mousePos   = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.DrawRay(mousePos2D, Vector3.zero, Color.green);
            #endregion
            
        /*    if (hit.collider != null)
                if (hit.collider.GetComponent<Draggable>() != null)
                {
                    Draggable drag = hit.collider.GetComponent<Draggable>();
                    if (drag.draggable)
                        drag.dragging = true;
                }*/
        }
    }
}
