using System;
using System.Collections;
using System.Collections.Generic;
using AgarioShared.AgarioShared.Enums;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 nextPosition;
   
    public Transform player;
    private bool move = false;
    private float alpha;
    public float moveSpeed = 0.1f;
    private Vector3 currentPos;
    
    private void Awake()
    {
        PlayerLink.Instance.NewPositionGot += SetNewPosition;
    }

    private void OnDisable()
    {
        PlayerLink.Instance.NewPositionGot -= SetNewPosition;
    }

    private void SetNewPosition(Vector3 newPos, PlayerCounter playerNumber)
    {
        if (PlayerLink.Instance.playerNumber != playerNumber) return;
        if (currentPos == newPos) return;
        
        currentPos = player.position;
        nextPosition = newPos;
        alpha = 0;
        move = true;
    }
    
    void Update()
    {
        if (!move) return;
        
        player.position =
            Vector3.Lerp(currentPos, nextPosition, alpha);
        alpha += moveSpeed * Time.deltaTime;

        if (!(alpha > 0.99f)) return;
        
        move = false;
        alpha = 0;
    }

}
