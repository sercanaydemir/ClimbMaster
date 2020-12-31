using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    public int climbSpeed;
    public float xValue;
    public float zValue;
    public float rightLeftSpeed;


    private bool isClimbing;
    private bool isMoved = true;
    private bool xSide;

    private Vector3 touchPos;
    private Vector3 deltaPos;
    private Vector3 moveVec;

    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        moveVec = Vector3.right;
        xSide = true;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        
        if (!isClimbing && isMoved)
        {
            anim.SetFloat("Yspeed", rb.velocity.y);
            rb.velocity = Vector3.forward * speed * Time.deltaTime;
            anim.SetFloat("Zspeed" ,rb.velocity.z);
            
        }

        else if (isClimbing && isMoved)
        {
            anim.SetFloat("Zspeed", rb.velocity.z);
            
            if (Input.GetMouseButtonDown(0))
            {
                anim.enabled = true ;
                touchPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 movePos = Input.mousePosition;

                deltaPos = movePos - touchPos;
                if (xSide)
                {
                    if (deltaPos.x < 40 && deltaPos.x > -40)
                    {
                        rb.velocity = Vector3.up * climbSpeed * Time.deltaTime;
                    }
                    else if (deltaPos.x >= 40)
                    {

                        rb.velocity = (moveVec / 2 + Vector3.up/2) * rightLeftSpeed * Time.deltaTime;
                    }

                    else if (deltaPos.x <= -40)
                    {
                        rb.velocity = (-moveVec / 2 + Vector3.up/2 ) * rightLeftSpeed * Time.deltaTime;
                    }

                    anim.SetFloat("Yspeed", rb.velocity.y);

                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xValue, xValue),
                        transform.position.y, transform.position.z);
                }

                else if (!xSide)
                {
                    if (deltaPos.x < 40 && deltaPos.x > -40)
                    {
                        rb.velocity = Vector3.up * climbSpeed * Time.deltaTime;
                    }
                    else if (deltaPos.x >= 40)
                    {

                        rb.velocity = (moveVec / 2 + Vector3.up) * climbSpeed * Time.deltaTime;
                    }

                    else if (deltaPos.x <= -40)
                    {
                        rb.velocity = (-moveVec / 2 + Vector3.up) * climbSpeed * Time.deltaTime;
                    }

                    anim.SetFloat("Yspeed", rb.velocity.y);

                    transform.position = new Vector3(transform.position.x,
                        transform.position.y, Mathf.Clamp(transform.position.z, -zValue, zValue));
                }

            }

            else if (isClimbing && Input.GetMouseButtonUp(0))
            {
                anim.enabled = false;
            }

            else
            {
                anim.SetFloat("Yspeed", rb.velocity.y);
                rb.velocity = Vector3.zero;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && !(collision.gameObject.tag == "Road")) isClimbing = true;

        if (collision.gameObject.tag == "Finish")
        {
            GameObject.FindGameObjectWithTag("enemyManager").SetActive(false);
            isClimbing = false;
            isMoved = false;
            anim.enabled = true;
            anim.SetBool("climbEnd", true);
            StartCoroutine(PlayerWin());

        }

        if (collision.gameObject.tag == "Enemy")
        {
            PlayerDie();
        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "runGround")
        {
            if (Input.GetMouseButton(0))
                return;
            else
                rb.AddForce(Vector3.down * 45);

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            PlayerDie();
        }
        if (other.gameObject.tag == "rotateObstacle")
        {
            RotatePlayer();
            
        }

    }

   

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void PlayerDie()
    {
        anim.enabled = true;
        anim.SetBool("Die", true);
        rb.useGravity = true;
        GetComponent<CapsuleCollider>().isTrigger = false;
        isMoved = false;
    }

    private void RotatePlayer()
    {
        xSide = !xSide;
        if (deltaPos.x > 40)
        {
            var transformRotation = transform.rotation;
            transformRotation.eulerAngles -= Vector3.up * 90;

            transform.rotation = transformRotation;
        }

        else if (deltaPos.x < -40)
        {
            var transformRotation = transform.rotation;
            transformRotation.eulerAngles += Vector3.up * 90;

            transform.rotation = transformRotation;
        }

        if (transform.rotation.eulerAngles.y>=0 && transform.rotation.eulerAngles.y < 90)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
            moveVec = Vector3.left;
            
        }

        else if (transform.rotation.eulerAngles.y >= 90 && transform.rotation.eulerAngles.y < 180)
        {

            transform.position = new Vector3(zValue, transform.position.y, transform.position.z);
            moveVec = Vector3.forward;
            
        }

        else if (transform.rotation.eulerAngles.y >= 180 && transform.rotation.eulerAngles.y < 270)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y, -zValue);
            moveVec = Vector3.right;
        }

        else if (transform.rotation.eulerAngles.y >= 270 && transform.rotation.eulerAngles.y < 360)
        {
            transform.position = new Vector3(-zValue, transform.position.y, transform.position.z);
            moveVec = Vector3.back;
            
        }
    }
    
    IEnumerator PlayerWin()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(3.8f);

        GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
        transform.position = transform.position + Vector3.up*2;
        anim.SetBool("Win",true);
        
    }
}
