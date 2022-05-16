using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var count = other.GetComponent<PlayerMesh>().PlayerCounter;
        
        PlayerLink.Instance.IncreaseScore(count,1, true);
        Destroy(gameObject);
    }


}
