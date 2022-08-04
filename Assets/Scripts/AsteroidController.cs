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

    public bool doubleClicked = false;
    float clickDelay = 0.5f;
    float passedTimeClick = 0.5f;

    private void Update()
    {
        if (doubleClicked)
        {
            if (colorNumber < 3) colorNumber++;
            lifePoints--;
            if (lifePoints == 0)
            {
                AsteroidDestroy();
                GameManager.gameManager.points++;
                Debug.Log("Obiekt zniszczony");
            }
        }

        asteroidRenderer.material = materials[colorNumber];

        passedTimeClick += Time.deltaTime;
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
        GameManager.gameManager.countOfAsteroids--;
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

    public void Click()
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
    }

}
