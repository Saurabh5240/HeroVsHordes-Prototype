using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblins : MonoBehaviour
{
    private GameObject hero;
    public GameObject coins;
    [SerializeField] float speed = 3;

    void Start()
    {
        hero = GameObject.Find("Hero");
    }  

    void Update()
    {

        if (hero != null)
        {
            // Calculate direction towards the hero
            Vector3 moveDir = (hero.transform.position - transform.position).normalized;

            // Rotate to face the hero
            transform.LookAt(hero.transform);

            // Move towards the hero
            transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
        }
    }  


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            speed = 0;
        
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            Vector3 newPos = transform.position;
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Instantiate(coins, newPos, coins.transform.rotation);
        
        }
    
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            speed = 3;

        }

    }
}

