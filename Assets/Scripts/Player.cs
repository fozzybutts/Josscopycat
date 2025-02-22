using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Floating the attributes for Mario's abilities

    private float walkspeed = 3f;

    public float jumpheight = 8f;

    //groundedindicator and aboveindicator (and their respective layermasks) are both used for collision detection for jumping and hitting boxes

    //these floats are all public so that everything can be assigned to the proper gameobjects/layers in unity

    

    public Transform groundedindicator;
    public float groundindicatorspace = 0.2f;
    public Transform aboveindicator;
    public LayerMask Ground;
    public LayerMask BlockLayer;
    private Rigidbody2D RB;


    //conditional bools for the indicators

    private bool IsOnGround;
    private bool IsRightAbove;







    void Start()
    {



        //getting the rigidbody and making sure it doesnt spin around 

        RB = GetComponent<Rigidbody2D>();
        RB.freezeRotation = true;





    }

    // Update is called once per frame
    void Update()
    {

        //defining what make the player grounded or hitting a block (if the overlapcircles on the indicators overlap something within the chosen layers)

        IsOnGround = Physics2D.OverlapCircle(groundedindicator.position, groundindicatorspace, Ground | BlockLayer);
        IsRightAbove = Physics2D.OverlapCircle(aboveindicator.position, 0.2f, BlockLayer);

        //walking code

        float walk = Input.GetAxis("Horizontal");
        RB.linearVelocity = new Vector2(walk * walkspeed, RB.linearVelocity.y);

        //making jumping conditional to being grounded and calling the Blocks function

        if (IsOnGround && Input.GetButtonDown("Jump"))
        {
            RB.AddForce(Vector2.up * jumpheight, ForceMode2D.Impulse);
        }


        
        Blocks();


    }


    void Blocks()
    {


        //making the gameobject the player hit get destroyed and halting the players vertical velocity when they hit a block

        Collider2D hit = Physics2D.OverlapCircle(aboveindicator.position, 0.2f, BlockLayer);

        if (hit != null)
        {

            Destroy(hit.gameObject);
          
            RB.linearVelocityY = 0;

        }
//Made by Aaron Howlett (200609705) and Liam Johnston (200608641)
    }
}
