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
	public SceneManagement sceneManagement;

    public GameObject currentlyInteractingWith;
	private bool exploredWoods { get; set; }

	public AreaEntrance forestEntrance;

	public void Awake()
	{
        //create new commands here

        //create advance entry node command
        dialogueRunner.AddCommandHandler<string>
            (
            "advanceEntryNode",     // name of new yarn command
            AdvanceEntryNode        // name of c# method to run
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

		//create function to check if player found blackberries
		dialogueRunner.AddFunction<bool>
		  (
		  "foundBlackberries",     // name of new yarn command
		  CheckFoundBlackberries       //name of c# method to run
		  );

		//command to unlock woods area
		dialogueRunner.AddCommandHandler
			(
			"unlockWoods",     // name of new yarn command
			UnlockWoods        //name of c# method to run
			);

		//function to check if player has entered the woods yet
		dialogueRunner.AddFunction<bool>
		  (
		  "exploredWoods",     // name of new yarn command
		  CheckExploredWoods       //name of c# method to run
		  );

		//function check route plan quality
		dialogueRunner.AddFunction<bool>
		  (
		  "walkIsGood",     // name of new yarn command
		  CheckWalkIsGood       //name of c# method to run
		  );

		dialogueRunner.AddCommandHandler
		 (
			"showDiaryEntry",
			showDiaryEntry
		 ); 
		
		dialogueRunner.AddCommandHandler
		 (
			"loadEndCredits",
			loadEndCredits
		 );

		//make endingDialogue listen to the dialoguerunner
		dialogueRunner.onDialogueComplete.AddListener(endingDialogue);

        //get player
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerController>();

		//get scene management
		sceneManagement = GameObject.FindGameObjectWithTag("SceneManagement").GetComponent<SceneManagement>();
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

    private bool CheckFoundBlackberries()
    {
		return playerControl.checkIfHolding("blackberry");
	}

	private void UnlockWoods()
	{
		forestEntrance.setAreaAccessibility(true);
	}

	private bool CheckExploredWoods()
	{
		return exploredWoods;
	}

	private bool CheckWalkIsGood() 
	{
		int numOfFoodsFound = playerControl.getInventorySize();

		if(numOfFoodsFound >= 4)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void showDiaryEntry()
	{
		//show the diary entry pop up 
	}

	private void loadEndCredits()
	{
		sceneManagement.loadEnd();
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
