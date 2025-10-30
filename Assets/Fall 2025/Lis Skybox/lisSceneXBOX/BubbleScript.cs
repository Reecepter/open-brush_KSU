using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    //billboarding
    private Transform cameraTransform;
    //floating
    public float speed = 2.0f;
    public float floatStrength = 0.1f;
    private float originalY;
    void Start()
    {
        //billboarding
        cameraTransform = Camera.main.transform;
        //floating
        originalY = transform.position.y;
    }

    void Update()
    {
        //billboarding
        Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        //floating
        float newY = originalY + Mathf.Sin(Time.time * speed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
}
