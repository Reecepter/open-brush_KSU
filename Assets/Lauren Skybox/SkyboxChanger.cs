using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign skybox materials in Inspector
    public Transform[] buttonTransforms;
    public GameObject buttonHighlight;
    private int currentIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeSkybox(currentIndex);
        }
        if (skyboxes == null || skyboxes.Length == 0)
        {
            RenderSettings.skybox = skyboxes[0];
        }
    }

    public void ChangeSkybox(int index)
    {
        currentIndex = (currentIndex + 1) % skyboxes.Length;
        if (skyboxes == null || skyboxes.Length == 0)
        {
            Debug.LogWarning("Skybox list is empty or not assigned!");
            return;
        }

        if (index >= 0 && index < skyboxes.Length)
        {
            Debug.Log("button clicked");
            RenderSettings.skybox = skyboxes[index];
            buttonHighlight.transform.position = buttonTransforms[index].transform.position;
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Index out of range for skybox list: " + index);
        }
    }
}
