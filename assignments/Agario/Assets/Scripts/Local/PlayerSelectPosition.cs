using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectPosition : MonoBehaviour
{
    private Camera _cam;
    private Plane _plane = new Plane(Vector3.up, 0);

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out var distance))
            {
                PlayerLink.Instance.UpdateLocation(ray.GetPoint(distance));
            }
        }
    }
}
