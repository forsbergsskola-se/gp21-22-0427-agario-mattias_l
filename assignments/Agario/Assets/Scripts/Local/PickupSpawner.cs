using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public List<GameObject> pickups;
    public float SpawnInterval = 2f;
    public float timer;
    public int spawnAmount = 8;
    private int totalSpawned;
    public int maxSpawn = 100;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SpawnInterval)
        {
            timer = 0;
            SpawnPickups();
        }
            
        
    }

    private void SpawnPickups()
    {
        if (totalSpawned >= maxSpawn) return;
        
        for (int i = 0; i < spawnAmount; i++)
        {
            var x = Random.Range(-50f, 50f);
            var z = Random.Range(-50f, 50f);
            Vector3 spawnPos = new Vector3(x, 0.05f, z);
            
            var pickType = Random.Range(0, 2);
            
            var temp =Instantiate(pickups[pickType], spawnPos,Quaternion.identity);
            temp.transform.Rotate(Vector3.right, 90);
            totalSpawned++;
        }
    }
}
