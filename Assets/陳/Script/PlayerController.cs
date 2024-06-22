using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float speedÅ@=1.0f;

    [SerializeField]
    GameObject vcm;

    Animator animator;
    public int itemCount = 0;



    Transform playerTransform;
    Rigidbody rigidbody;


    float jumpVelociy;
    float jumpHash;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        jumpHash = Animator.StringToHash("HorizantaVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        animator.speed = speed;
       // playerTransform.position += new Vector3(0.0f,0.0f, -speed * Time.deltaTime);
        PlayerInput();

    }

    void ItemCheck()
    {
    
    }



    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(playerTransform.position.x >= 5.0f)
            {
                return;
            }
            playerTransform.position += new Vector3(5.0f, 0.0f,0.0f);
        } 
        else if (Input.GetKeyDown(KeyCode.D))
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
            animator.SetBool("Win",true);
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector3(0.0f, 5.0f, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("isGround");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("mid air");
        }

    }

}
