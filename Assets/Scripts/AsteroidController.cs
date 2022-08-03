using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    float rotateX;
    float rotateY;
    float rotateZ;

    private void Start()
    {
        rotateX = Random.Range(0, 90);
        rotateY = Random.Range(0, 90);
        rotateZ = Random.Range(0, 90);
        Rotation();
    }

    public void AsteroidDestroy()
    {
        Debug.Log("Obiekt zniszczony");
        Destroy(this.gameObject);
    }

    public void Rotation()
    {
        transform.Rotate(new Vector3(rotateX, rotateY, rotateZ));
    }
}
