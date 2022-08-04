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
    public GameObject WinPanel;
    public Text PointsTxt;

    // Game Properties
    int randomQuantity;
    int countOfAsteroids;
    int points = 0;
    const int maxPoints = 3;
    public bool endGame = false;

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

        WinPanel.SetActive(false);
    }

    void Update()
    {
        PointsTxt.text = "Punkty:" + points.ToString();
        if (!endGame)
        {
            MaintenanceOfAsteroidCount();
            OnMouseClick();
            VictoryCheck();
        }

    }

    /// <summary>
    /// Spawns an asteroid in a random location
    /// </summary>
    void Spawn()
    {
        GameObject obj = Instantiate(SpawnablePrefab, Vector3.zero, Quaternion.identity) as GameObject;

        var x_rand = Random.Range(-x_dim, x_dim);
        var y_rand = Random.Range(-y_dim, y_dim);

        obj.transform.position = new Vector3(x_rand, y_rand, z_dim);
        countOfAsteroids++;
    }

    /// <summary>
    /// Maintains a certain number of asteroids on the screen
    /// </summary>
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

    /// <summary>
    /// On the object selected and clicked on with the mouse, calls the OnClick() method.
    /// </summary>
    void OnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Asteroid")
                {
                    hit.collider.GetComponent<AsteroidController>().OnClick();
                }
            }
        }
    }

    public void IncreasePoints()
    {
        points++;
    }

    public void DecreaseAsteroids()
    {
        countOfAsteroids--;
    }

    void VictoryCheck()
    {
        if (points >= maxPoints)
        {
            endGame = true;
            WinPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Restart the game by reloading the scene
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
