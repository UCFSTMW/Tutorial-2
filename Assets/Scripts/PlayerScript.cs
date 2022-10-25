using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    public Text winText;

    private int livesValue = 3;

    public Text lives;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
    
    public AudioSource musicSource;

   private bool facingRight = true;

     Animator anim;
      private bool isJumping = false;

    

     
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives.text = livesValue.ToString();
        Textscore();
        winText.text = "";
        musicSource.clip = musicClipOne;
          musicSource.Play();
          musicSource.loop = false;
          anim = GetComponent<Animator>();
          
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
         

         if (isJumping == false && vertMovement > 0)
         {
          Start();
         }
         else if (isJumping == true && vertMovement < 0)
         {
          Start();
        }
        
    }   
    

    

    
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            Textscore();
            
        }

        else if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
            Textscore();

        }

       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
       
       void Textscore() 

    { 
        if (scoreValue == 4 )
                
       {
           transform.position = new Vector2(50.0f, 0.0f);

           livesValue = 3;
           lives.text = livesValue.ToString();
           Textscore();

       } 

        if (scoreValue == 8)
       {
         winText.text = "You Win! game made by Max Willis";
         musicSource.clip = musicClipTwo;
          musicSource.Play();
          musicSource.loop = false;              
            
         
       }

       else if (livesValue == 0)

       {
          winText.text = "You Lose!";
          Destroy(gameObject);         
          
       }
               
       }

       void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

       
    }
