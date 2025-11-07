using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign different skybox materials in the Inspector
    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSkybox();
        }
    }

    public void ChangeSkybox()
    {
        currentIndex = (currentIndex + 1) % skyboxes.Length;
        RenderSettings.skybox = skyboxes[currentIndex];
        DynamicGI.UpdateEnvironment(); // Updates lighting if needed
    }
}
