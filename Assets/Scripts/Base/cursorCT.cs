using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorCT : MonoBehaviour
{
    public bool showCursor = false;
    public CameraCT2 cameraCT;
    private void Start()
    {
        Cursor.visible = showCursor;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            showCursor = !showCursor;
            Cursor.visible = showCursor;

            cameraCT.canRotate = !showCursor;

        }
    }
}
