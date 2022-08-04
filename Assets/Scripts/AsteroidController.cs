using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Renderer asteroidRenderer;
    public Material[] materials;
    int colorNumber = 0;
    float rotateX;
    float rotateY;
    float rotateZ;
    float lifeTime;
    int lifePoints = 4;

    bool doubleClicked = false;
    float clickDelay = 0.5f;
    float passedTimeClick = 0.5f;

    private void Update()
    {
        asteroidRenderer.material = materials[colorNumber];

        passedTimeClick += Time.deltaTime;

        if (GameManager.gameManager.endGame) AsteroidDestroy();
    }

    private void Start()
    {
        rotateX = Random.Range(0, 90);
        rotateY = Random.Range(0, 90);
        rotateZ = Random.Range(0, 90);
        lifeTime = Random.Range(10, 30);
        Rotation();
        Invoke("AsteroidDestroy", lifeTime / 10);
    }

    public void AsteroidDestroy()
    {
        Destroy(this.gameObject);
        GameManager.gameManager.DecreaseAsteroids();
    }

    public void Rotation()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ));
    }

    private IEnumerator ClickReset()
    {
        yield return new WaitForEndOfFrame();
        doubleClicked = false;
    }

    /// <summary>
    /// If you double click change color and takes life points until destroing it
    /// </summary>
    public void OnClick()
    {
        if (passedTimeClick < clickDelay)
        {
            doubleClicked = true;
        }
        else
        {
            passedTimeClick = 0;
        }

        StartCoroutine(ClickReset());

        if (doubleClicked)
        {
            if (colorNumber < 3) colorNumber++;
            lifePoints--;

            if (lifePoints == 0)
            {
                AsteroidDestroy();
                GameManager.gameManager.IncreasePoints();
                Debug.Log("Obiekt zniszczony");
            }
        }
    }

}
