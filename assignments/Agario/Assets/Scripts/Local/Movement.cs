using System;
using System.Collections;
using System.Collections.Generic;
using AgarioShared.AgarioShared.Enums;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 nextPosition;
    private Vector3 _currentPos;
    private bool _move;
    private float _alpha;
    public float moveSpeed = 0.4f;
    public PlayerCounter PlayerCounter;
    public float elevation = 0.1f;

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
        if (this.PlayerCounter != playerNumber) return;
        
        Debug.Log($"Player {(int)playerNumber} is moving");
        if (_currentPos == newPos) return;

        nextPosition = newPos + new Vector3(0,elevation, 0);
        
        Dispatcher.RunOnMainThread(SetNewPosition);
    }

    private void SetNewPosition()
    {
        _currentPos = transform.position;
        _alpha = 0;
        _move = true;
    }
    
    void Update()
    {
        if (!_move) return;
        
        transform.position =
            Vector3.Lerp(_currentPos, nextPosition, _alpha);
        _alpha += moveSpeed * Time.deltaTime;

        if (!(_alpha > 0.99f)) return;
        
        _move = false;
        _alpha = 0;
    }

}
