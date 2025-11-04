using System.Collections;
using System.Collections.Generic;
using TiltBrush;
using UnityEngine;

public class simpleScript : MonoBehaviour
{
    public string path;
    public SceneSettings sceneSettings;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchSkyBoxes()
    {
        sceneSettings.LoadCustomSkybox(path);
    }
}
