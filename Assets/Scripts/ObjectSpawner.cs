using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header ("Düşen Objeler")]
    public GameObject[] dynamicObstacle;

    void Start()
    {
        StartCoroutine(CreateDynmicObstacle());
    }

    IEnumerator CreateDynmicObstacle()
    {
        yield return new WaitForSeconds(Random.Range(4f,5));
        float i = Random.Range(0.01f, 1);

        if (i<=0.25f)
        {
            Instantiate(dynamicObstacle[Random.Range(0, dynamicObstacle.Length)],
                new Vector3(Random.Range(-2, 2), transform.position.y, -3.3f), Quaternion.identity);
        }
        else if (i<=0.50f)
        {
            Instantiate(dynamicObstacle[Random.Range(0, dynamicObstacle.Length)],
                new Vector3(-3.3f, transform.position.y, Random.Range(-2, 2)), Quaternion.identity);
        }
        else if (i<=0.75f)
        {
            Instantiate(dynamicObstacle[Random.Range(0, dynamicObstacle.Length)],
                new Vector3(Random.Range(-2, 2), transform.position.y,  3.3f), Quaternion.identity);
        }
        else
            Instantiate(dynamicObstacle[Random.Range(0, dynamicObstacle.Length)],
                new Vector3(3.3f, transform.position.y, Random.Range(-2, 2)), Quaternion.identity);


        StartCoroutine(CreateDynmicObstacle());
    }

    
}
