using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerLink.Instance.IncreaseScore(1, true);
        Destroy(gameObject);
    }


}
