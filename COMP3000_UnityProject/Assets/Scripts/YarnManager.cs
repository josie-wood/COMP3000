using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/// <summary>
// Functions to be called from Yarn to affect game state
/// </summary>
public class YarnManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
	public Interactable NPC;
    public GameObject player;
    public PlayerController playerControl;

    public GameObject currentlyInteractingWith;

	public void Awake()
	{
        //create new commands here

        //create advance entry node command
        dialogueRunner.AddCommandHandler<string>
            (
            "advanceEntryNode",     // name of new yarn command
            AdvanceEntryNode        //name of c# method to run
            );

		//create Forage command
		dialogueRunner.AddCommandHandler
            (
			"forage",     // name of new yarn command
			Forage        //name of c# method to run
			);

		//create CheckInventory function
		dialogueRunner.AddFunction<string, bool>
		  (
		  "checkInventory",     // name of new yarn command
		  CheckInventory        //name of c# method to run
		  );

		//make endingDialogue listen to the dialoguerunner
		dialogueRunner.onDialogueComplete.AddListener(endingDialogue);

        //get player
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerController>();
	}


	public void AdvanceEntryNode(string newEntryNode)
    {
        NPC.updateStartNode(newEntryNode);

    }

    public void Forage()
    {
        currentlyInteractingWith.GetComponent<Food>().Forage();
    }
    
    private bool CheckInventory(string itemName)
    {
        //takes an itemName in from yarn, checks status in player inventory
        return playerControl.checkIfHolding(itemName);
    }

    public void startingDialogue()
    {
		//lock movement when talking to NPC
		playerControl.lockMovement();
	}

    public void endingDialogue()
    {
		//runs when onDialogueEnd is ran from the line provider

		//return movement to normal
		playerControl.unlockMovement();

	}

    public void setCurrentInteractingWith(GameObject newInteractable)
    {
        currentlyInteractingWith= newInteractable;
    }

}
