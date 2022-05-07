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
    
    public delegate void NewPositionPickedDelegate(Vector3 newPosition);
    public static event NewPositionPickedDelegate OnPositionChanged;
    
    void Start()
    {
       
    }


    private void OnDisable()
    {
       
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                PlayerLink.Link.UpdateLocation(ray.GetPoint(distance));
            //    OnPositionChanged?.Invoke(ray.GetPoint(distance));
            }
        }
    }
}
