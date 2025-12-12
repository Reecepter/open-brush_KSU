using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;   // <-- add this

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign skybox materials in Inspector
    public Transform[] buttonTransforms;
    public GameObject buttonHighlight;
    public GameObject bubbles;
    public AudioSource music;
    public AudioClip[] musicClip;
    private int currentIndex = 0;

    // XR input
    private InputDevice leftHandDevice;
    private bool lastTriggerPressed = false;

    private void Start()
    {
        bubbles.SetActive(false);
        InitializeLeftHand();
    }

    private void InitializeLeftHand()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);   // get left controller [web:8]

        if (devices.Count > 0)
        {
            leftHandDevice = devices[0];
        }
    }

    private void Update()
    {
        // Ensure left hand device is valid
        if (!leftHandDevice.isValid)
        {
            InitializeLeftHand();
        }

        // Read left trigger button (bool) [web:15]
        if (leftHandDevice.isValid)
        {
            bool triggerPressed;
            if (leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed))
            {
                // Rising edge detection: just pressed this frame
                if (triggerPressed && !lastTriggerPressed)
                {
                    ChangeSkybox(currentIndex);
                }

                lastTriggerPressed = triggerPressed;
            }
        }

        // Optional: set initial skybox if needed (fixed null check)
        if (skyboxes != null && skyboxes.Length > 0 && RenderSettings.skybox == null)
        {
            RenderSettings.skybox = skyboxes[0];
        }
    }

    public void ChangeSkybox(int index)
    {
        if (skyboxes == null || skyboxes.Length == 0)
        {
            Debug.LogWarning("Skybox list is empty or not assigned!");
            return;
        }

        // Advance index
        currentIndex = (currentIndex + 1) % skyboxes.Length;
        index = currentIndex;

        if (index >= 0 && index < skyboxes.Length)
        {
            Debug.Log("button clicked");
            if (index == 0)
            {
                bubbles.SetActive(true);
            }
            else
            {
                bubbles.SetActive(false);
            }

            RenderSettings.skybox = skyboxes[index];

            if (music.isPlaying)
                music.Stop();
            music.clip = musicClip[index];
            music.Play();

            buttonHighlight.transform.position = buttonTransforms[index].transform.position;

            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogWarning("Index out of range for skybox list: " + index);
        }
    }
}
