using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign skybox materials in Inspector

    public void ChangeSkybox(int index)
    {
        if (skyboxes == null || skyboxes.Length == 0)
        {
            Debug.LogWarning("Skybox list is empty or not assigned!");
            return;
        }

        if (index >= 0 && index < skyboxes.Length)
        {
            Debug.Log("button clicked");
            RenderSettings.skybox = skyboxes[index];
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Index out of range for skybox list: " + index);
        }
    }
}
