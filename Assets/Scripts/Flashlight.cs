using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        Debug.Log("found flaslight ?: ", light);
    }
    public void LightOn()
    {
        Debug.Log("lights oooooooooooooooooooon");
        light.enabled = true;
    }

    public void LighOff()
    {
        Debug.Log("lights oooooooooooooooooooof");
        light.enabled = false;
    }

}
