using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<string> inventory = new List<string>();

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
	// MOVEMENT
	// Player moves around the world to explore and interact.
	// Movement affects animation.
	// Movement paused when reading dialogue/pop ups.
	#region

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
	#endregion

	// INVENTORY
    // Player can collect items and add to inventory
    // Only one of each item
	#region
    public void addToInventory(string itemName)
    {
        if (!inventory.Contains(itemName))
        {
            // item isn't already in array
            // so add to inventory
            inventory.Add(itemName);
        }
        else
        {
            //item is already in inventory
            //can't collect it
        }
    }

    public bool checkIfHolding(string itemName)
    {
		if (inventory.Contains(itemName))
		{
            // item isn't already in array
            return true;
		}
		else
		{
            //item is already in inventory
            return false;
		}
	}
	#endregion

	// EXPLORATION
	// Player can explore the forest
	// As they hit colliders associated with areas
	// Their exploration score goes up 
	// When exploration score is high enough, their plan is set as good
	#region

	#endregion

}
