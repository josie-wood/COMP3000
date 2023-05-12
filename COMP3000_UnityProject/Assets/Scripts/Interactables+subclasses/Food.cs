using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Food : Interactable
{
    // class used to add foragable food in the world the player can interact with
    public string foodName;
    public GameObject player;
    public PlayerController playerController;
    public Sprite postForageSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        dialogueRunner = GameObject.FindObjectOfType<DialogueRunner>();
    }

	// Update is called once per frame
	private void Update()
	{
		if (withinRange)
		{
			if (Input.GetKeyUp(interactKey))
				if (!yarnManager.IsDialogueRunning())
				{
					Debug.Log("interactionKeyPressed " + interactKey);

					//set this as currently interacting object in yarnMan

					yarnManager.setCurrentInteractingWith(this.gameObject);
					yarnManager.startingDialogueItem(foodName);
				}
			
		}
	}

	public void Forage()
    {
        //collect food if player isn't already holding one
        bool playerHolding = playerController.checkIfHolding(foodName);

        // add to inventory
        if (!playerHolding)
        {
            playerController.addToInventory(foodName);

            // remove visual element from scene/replace art
            this.GetComponent<SpriteRenderer>().sprite = postForageSprite;

            // turn off collider so can't interact with it anymore
            this.GetComponent<Collider2D>().enabled = false;

            // run dialogue
		}
        else
        {

			yarnManager.startingDialogueItem("alreadyForaged");
			Debug.Log("already one in inventory");
        }
    }
}
