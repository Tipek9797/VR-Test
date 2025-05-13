using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
    }
    public void LightOn()
    {
        light.enabled = true;
    }

    public void LighOff()
    {
        light.enabled = false;
    }

}
