using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject spawnablePrefab;
    int quantity;
    public int countOfAsteroids;

    // Plane Properties
    float x_dim = 9;
    float y_dim = 4.8f;
    float z_dim = 0;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        for (int i = 0; i < 5; i++)
        {
            Spawn();
        }
    }

    private void FixedUpdate()
    {
        quantity = Random.Range(1, 6);

        if (countOfAsteroids < 5)
        {
            for (int i = 0; i < quantity; i++)
            {
                Spawn();
            }
        }        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Asteroid")
                {
                    hit.collider.GetComponent<AsteroidController>().Click();
                }
            }
        }
    }

    void Spawn()
    {
        GameObject obj = Instantiate(spawnablePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        var x_rand = Random.Range(-x_dim, x_dim);
        var y_rand = Random.Range(-y_dim, y_dim);

        obj.transform.position = new Vector3(x_rand, y_rand, z_dim);
        countOfAsteroids++;
    }
}
