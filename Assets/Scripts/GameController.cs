using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject spawnablePrefab;

    // Plane Properties
    float x_dim = 9;
    float y_dim = 4.8f;
    float z_dim = 0;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject obj = Instantiate(spawnablePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        var x_rand = Random.Range(-x_dim, x_dim);
        var y_rand = Random.Range(-y_dim, y_dim);

        obj.transform.position = new Vector3(x_rand, y_rand, z_dim);
    }
}
