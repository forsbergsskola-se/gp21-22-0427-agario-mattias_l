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
    public float moveSpeed = 0.1f;
    private Vector3 currentPos;
    
    private void Awake()
    {
        cam = GetComponent<Camera>();
        PlayerLink.Link.NewPositionGot += SetNewPosition;
    }

    private void OnDisable()
    {
        PlayerLink.Link.NewPositionGot -= SetNewPosition;
    }

    private void SetNewPosition(Vector3 newPos)
    {
        currentPos = player.position;
        worldPosition = newPos;
        alpha = 0;
        move = true;
    }
    
    void Update()
    {
        if (!move) return;
        
        player.position =
            Vector3.Lerp(currentPos, worldPosition, alpha);
        alpha += moveSpeed * Time.deltaTime;

        if (!(alpha > 0.99f)) return;
        
        move = false;
        alpha = 0;
    }

}
