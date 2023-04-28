using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light Light;
    public bool activLight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            activLight = !activLight;

            if (activLight == true)
            {
                Light.enabled = true;
            }

            if (activLight == false)
            {
                Light.enabled = false;
            }
        }
    }
}
