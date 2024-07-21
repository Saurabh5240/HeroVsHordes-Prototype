using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnDistance = 50f;
    
    public GameObject goblin;
    private GameObject hero;
    //Invoke the spawn enemies function 
    private float repeatTime = 20f;

    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        InvokeRepeating("SpawnEnemyWave", 1f, repeatTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemyWave()
    {


        for (int i = 0; i < 30; i++)
        {
            
            Instantiate(goblin, GenerateRandomPos(), goblin.transform.rotation);


        }

    }

    private Vector3 GenerateRandomPos()
    {

        Vector3 randomDirection = Random.onUnitSphere;
        randomDirection.y = 0; 
           
        
        Vector3 randomSpawnPos = hero.transform.position + randomDirection * spawnDistance;
        return randomSpawnPos;

    }
}
