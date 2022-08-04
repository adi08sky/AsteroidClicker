using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject SpawnablePrefab;
    public GameObject RestartBtn;
    public Text PointsTxt;
    int randomQuantity;
    public int countOfAsteroids;
    public int points = 0;
    const int maxPoints = 15;

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

        RestartBtn.SetActive(false);
    }

    private void Update()
    {
        PointsTxt.text = "Punkty:" + points.ToString();
        MaintenanceOfAsteroidCount();
        MouseClick();
        VictoryCheck();
    }

    void Spawn()
    {
        GameObject obj = Instantiate(SpawnablePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        var x_rand = Random.Range(-x_dim, x_dim);
        var y_rand = Random.Range(-y_dim, y_dim);

        obj.transform.position = new Vector3(x_rand, y_rand, z_dim);
        countOfAsteroids++;
    }

    private void MaintenanceOfAsteroidCount()
    {
        randomQuantity = Random.Range(1, 6);

        if (countOfAsteroids < 5)
        {
            for (int i = 0; i < randomQuantity; i++)
            {
                Spawn();
            }
        }
    }
    void MouseClick()
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

    void VictoryCheck()
    {
        if (points >= maxPoints)
        {
            RestartBtn.SetActive(true); 
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
