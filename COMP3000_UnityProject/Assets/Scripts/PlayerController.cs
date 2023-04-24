using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    //variables
    public float moveSpeed = 1f;
    public Rigidbody2D playerRB;
    public Vector2 currentPos;
    public Animator playerAnim;
    public SpriteRenderer playerSprite;

    public bool movementLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRB= GetComponent<Rigidbody2D>();
        currentPos = GetComponent<Transform>().position;
        playerAnim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
        if (!movementLocked)
        {
            currentPos = GetComponent<Transform>().position;
            Vector2 playerPos = playerRB.position;
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horInput, vertInput);
            if (playerAnim) setWalkAnim(inputVector);
            Vector2 movement = inputVector * moveSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            playerRB.MovePosition(newPos);
        }

	}

    public void lockMovement()
    {
        //used to lock movement when talking to NPC
        Vector2 still = new Vector2(0, 0);
        setWalkAnim(still);
        movementLocked = true;
    }

    public void unlockMovement()
    {
        movementLocked = false;
    }    

    private void setWalkAnim(Vector2 newInputVector)
    {
        //walk vs idle
        if(newInputVector.x != 0 || newInputVector.y != 0) 
        {
			playerAnim.SetBool("walking", true);
		}
        else
        {
			playerAnim.SetBool("walking", false);
		}

        //set fw or bw anim
        if(newInputVector.y <= 0)
        {
            playerAnim.SetBool("facingFront", true);
        }
        else if(newInputVector.y > 0)
        {
			playerAnim.SetBool("facingFront", false);
		}

        //flip if needed
        if(newInputVector.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if(newInputVector.x < 0)
        {
            playerSprite.flipX = true;
        }

    }
}
