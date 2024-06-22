using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
   public float speedÅ@=1.0f;

    [SerializeField]
    GameObject vcm;

    Animator animator;
    public int itemCount = 0;



    Transform playerTransform;
    Rigidbody rigidbody;

    bool isGround=true;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

    
    }

    // Update is called once per frame
    void Update()
    {
        if (speed<=3.0f)
        {
            animator.speed = speed;
        }
        //Debug.Log(rigidbody.velocity.y);
        //animator.SetFloat("Velocity", rigidbody.velocity.y);

       // playerTransform.position += new Vector3(0.0f,0.0f, -speed * Time.deltaTime);
        PlayerInput();


        //animator.SetBool("isGround", isGround);
       
    }


    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if(playerTransform.position.x >= 5.0f)
            {
                return;
            }
            playerTransform.position += new Vector3(5.0f, 0.0f,0.0f);
        } 
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (playerTransform.position.x <= -5.0f)
            {
                return;
            }
            playerTransform.position += new Vector3(-5.0f, 0.0f, 0.0f);
        }

        // for test
        if(Input.GetKeyDown(KeyCode.X)){
            speed += 0.5f;
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            
            vcm.SetActive(true);
            Invoke("Win", 0.9f);
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(0.0f, 5.0f, 0.0f);
        }
    }

    void Win()
    {
        animator.SetBool("Win", true);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGround = true;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGround = false;
    //    }

    //}

}
