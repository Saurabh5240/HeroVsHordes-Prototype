using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOrbs : MonoBehaviour
{
    public GameObject hero;            // Reference to the hero GameObject
    public GameObject firePrefab;      // Reference to the fire object prefab
    private float speed = 3f;          // Speed of the fire object

    void Start()
    {
        // Start invoking the SpawnAndChase method every 1 second
        InvokeRepeating("SpawnAndChase", 1f, 1f);
    }

    void SpawnAndChase()
    {
        // Instantiate fire object at hero's position
        GameObject fireObject = Instantiate(firePrefab, hero.transform.position, Quaternion.identity);
        Debug.Log("Fire object spawned at hero's position.");

        GameObject nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            StartCoroutine(ChaseEnemy(fireObject, nearestEnemy));
        }
        else
        {
            // Destroy the fire object if no enemies are found
            Destroy(fireObject);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Goblin");

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 heroPosition = hero.transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(heroPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    IEnumerator ChaseEnemy(GameObject fireObject, GameObject enemy)
    {
        while (enemy != null && fireObject != null)
        {
            // Check if the enemy is still active in the hierarchy
            if (enemy.activeInHierarchy)
            {
                // Calculate direction to the enemy
                Vector3 direction = (enemy.transform.position - fireObject.transform.position).normalized;

                // Move fire object towards the enemy
                fireObject.transform.position += direction * speed * Time.deltaTime;

                // Wait until the next frame
                yield return null;
            }
            else
            {
                break;
            }
        }

        // Destroy the fire object if the enemy no longer exists
        if (fireObject != null)
        {
            Destroy(fireObject);
        }
    }
}
