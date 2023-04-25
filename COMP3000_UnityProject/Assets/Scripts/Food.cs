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

    public DialogueRunner dialogueRunner;
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
			if (Input.GetKeyUp("space"))
				if (dialogueRunner.IsDialogueRunning == false)
				{
					Debug.Log("interactionKeyPressed " + interactKey);

					// play node
					Debug.Log("trying to start the dialogue now");
					dialogueRunner.StartDialogue(startNode);

					//set this as currently interacting object in yarnMan

					yarnManager.setCurrentInteractingWith(this.gameObject);

					yarnManager.startingDialogue();
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

            // run dialogue
            updateStartNode("alreadyForaged");
		}
        else
        {

			updateStartNode("alreadyForaged");
			Debug.Log("already one in inventory");
        }
    }
}
