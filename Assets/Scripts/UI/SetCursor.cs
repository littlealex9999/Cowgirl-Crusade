using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    [SerializeField] Texture2D crosshair;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorCenter = new Vector2(crosshair.width / 2, crosshair.height / 2);

        Cursor.SetCursor(crosshair, cursorCenter, CursorMode.ForceSoftware);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;

    }


}
