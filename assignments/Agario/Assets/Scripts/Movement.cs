using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Camera cam;
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, 0);
    public Transform player;
    private bool move = false;
    private float alpha;
    public float moveSpeed = 0.01f;
    private Vector3 currentPos;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // float distance;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                worldPosition = ray.GetPoint(distance);
                currentPos = player.position;
                alpha = 0;
                move = true;
            }
        }

        if (move)
        {
            player.position =
                Vector3.Lerp(currentPos, worldPosition, alpha);
            alpha += moveSpeed * Time.deltaTime;
            
            Debug.Log(alpha);
            
            if (alpha > 0.99f)
            {
                move = false;
                alpha = 0;
            }
        }
    }

}
