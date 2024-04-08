using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{ 
    public bool lockXRotation = false;
    public bool lockYRotation = false;
    public bool lockZRotation = false;
   
    void Update()
    {
     
        Quaternion currentRotation = transform.rotation;

        if (lockXRotation)
            currentRotation.x = 0f;
        if (lockYRotation)
            currentRotation.y = 0f;
        if (lockZRotation)
            currentRotation.z = 0f;

        transform.rotation = currentRotation;
    }
}
