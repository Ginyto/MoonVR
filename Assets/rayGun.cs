using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayGun : MonoBehaviour
{
    private LineRenderer beam;
    private Camera cam;

    private Vector3 origin;
    private Vector3 endPoint;
    private Vector3 mousePos;

    void Start()
    {
        // Grab our laser
        beam = this.gameObject.AddComponent<LineRenderer>();
        beam.startWidth = 0.1f;
        beam.endWidth = 0.1f;

        // grab the main cam
        cam = Camera.main;
    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
        checkLaser();
        }   else beam.enabled = false;
    }

    void checkLaser()
    {
        //finding the orgiin and end point of laser
        origin = this.transform.position + 
            this.transform.forward *0.5f * this.transform.lossyScale.z;

        // finding mouse pos in 3D space
        mousePos = Input.mousePosition;
        mousePos.z = 300f;
        endPoint = cam.ScreenToWorldPoint(mousePos);

        //find direction of beam
        Vector3 dir = endPoint - origin;
        dir.Normalize();

        //Are we hitting any colliuders 
        RaycastHit hit; 
        if (Physics.Raycast(origin, dir, out hit, 300f))
        {
            endPoint = hit.point; // if yes, then set endpoint to hit point.
        }

        //set end point of laser
        beam.SetPosition(0, origin);
        beam.SetPosition(1, endPoint);

        //draw the laser
        beam.enabled = true;
    }
}
