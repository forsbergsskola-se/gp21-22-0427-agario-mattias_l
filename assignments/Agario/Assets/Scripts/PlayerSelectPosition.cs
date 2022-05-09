using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectPosition : MonoBehaviour
{
    private Camera cam;
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);
    public Transform player;
    private bool move = false;
    private float alpha;
    public float moveSpeed = 0.01f;
    private Vector3 currentPos;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                PlayerLink.Instance.UpdateLocation(ray.GetPoint(distance));
            }
        }
    }
}
