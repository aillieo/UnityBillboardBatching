using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    public bool rotate;

    void Update()
    {
        if(rotate)
        {
            this.transform.Rotate(0, 0.1f, 0);
        }
    }
}
