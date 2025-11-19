using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Assign skybox materials in Inspector
    public Transform[] buttonTransforms;
    public GameObject buttonHighlight;
    public GameObject bubbles;
    public AudioSource music;
    public AudioClip[] musicClip;
    private int currentIndex = 0;

    private void Start()
    {
        bubbles.SetActive(false);
    }

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
