using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class speed1 : PhysicsObject {

    public float maxSpeed;
    public float jumpTakeOffSpeed = 7;
    public AudioSource collectSource;

    public Text counttext;
    public Text winText;

       
    private int count;
    private SpriteRenderer spriteRenderer;
    


    private void Start()
     {
        count = 0;
        SetCountText();
        winText.text = "";
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectSource = GetComponent<AudioSource>();

        
     }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        targetVelocity = move * maxSpeed;
    }


    

     void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag ("Pick Up"))
         {
             other.gameObject.SetActive(false);
             count = count + 1;
             SetCountText();
             collectSource.Play();
            
         }
     }

     void SetCountText()
     {
         counttext.text = "Pick Ups: " + count.ToString();
         if (count >= 6)
         {
            // winText.text = "You Win";
         }
     }
}

