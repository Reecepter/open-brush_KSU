using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRBrush : MonoBehaviour
{
    public InputActionReference triggerAction;   // Assign XRI right hand trigger action
    public GameObject strokePrefab;             // Has LineRenderer or tube mesh
    public float minDistance = 0.01f;

    private GameObject currentStroke;
    private LineRenderer lr;
    private Vector3 lastPoint;

    void OnEnable() => triggerAction.action.Enable();
    void OnDisable() => triggerAction.action.Disable();

    void Update()
    {
        float trigger = triggerAction.action.ReadValue<float>();
        Debug.Log(trigger);
        Debug.Log(currentStroke);

        if (trigger > 0.1f && currentStroke == null)
            StartStroke();

        if (trigger > 0.1f && currentStroke != null)
            ContinueStroke();

        if (trigger <= 0.1f && currentStroke != null)
            EndStroke();
    }

    void StartStroke()
    {
        currentStroke = Instantiate(strokePrefab);
        lr = currentStroke.GetComponent<LineRenderer>();
        Debug.Log(lr);
        lr.positionCount = 0;
        AddPoint();
    }

    void ContinueStroke() => AddPoint();

    void EndStroke()
    {
        currentStroke = null;
        lr = null;
    }

    void AddPoint()
    {
        Vector3 p = transform.position;
        if (lr.positionCount > 0 && Vector3.Distance(p, lastPoint) < minDistance) return;
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, p);
        lastPoint = p;
    }
}
