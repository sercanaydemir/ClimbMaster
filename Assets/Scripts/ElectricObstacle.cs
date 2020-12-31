using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricObstacle : MonoBehaviour
{
    public ParticleSystem electricParticle;
    public BoxCollider electricColl;
    public Transform startPos;
    public Transform endPos;
    public float speed;
    private Transform target;
    private float timer;
    void Start()
    {
        target = startPos;

    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 7)
        {
            timer = 0;
        }

        else if (timer > 3.5f)
        {
            electricColl.enabled = false;
        }

        else 
        {

            Wave();
            electricColl.enabled = true;

        }
    }

    private void Wave()
    {
        if (transform.localEulerAngles.y==0)
        {
            if (electricParticle.transform.position.x >= target.position.x)
            {
                target = endPos;
                electricParticle.transform.position += Vector3.left * Time.deltaTime * speed;

            }
            else if (electricParticle.transform.position.x <= target.position.x)
            {
                target = startPos;
                electricParticle.transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }

        else if (transform.localEulerAngles.y == 90)
        {
            if (electricParticle.transform.position.z >= target.position.z)
            {
                target = startPos;
                electricParticle.transform.position += Vector3.back * Time.deltaTime * speed;

            }
            else if (electricParticle.transform.position.z <= target.position.z)
            {
                target = endPos;
                electricParticle.transform.position += Vector3.forward * Time.deltaTime * speed;
            }
        }
        
        else if (transform.localEulerAngles.y == 180)
        {
            if (electricParticle.transform.position.x >= target.position.x)
            {
                target = startPos;
                electricParticle.transform.position += Vector3.left * Time.deltaTime * speed;

            }
            else if (electricParticle.transform.position.x <= target.position.x)
            {
                target = endPos;
                electricParticle.transform.position += Vector3.right * Time.deltaTime * speed;
            }
        }
        
        else if (transform.localEulerAngles.y == 270)
        {
            if (electricParticle.transform.position.z >= target.position.z)
            {
                target = endPos;
                electricParticle.transform.position += Vector3.back * Time.deltaTime * speed;

            }
            else if (electricParticle.transform.position.z <= target.position.z)
            {
                target = startPos;
                electricParticle.transform.position += Vector3.forward * Time.deltaTime * speed;
            }
        }

    }
}
