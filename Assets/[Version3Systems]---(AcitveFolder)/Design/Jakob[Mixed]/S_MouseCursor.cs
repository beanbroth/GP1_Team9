using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MouseCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
