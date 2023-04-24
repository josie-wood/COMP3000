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

	public void Awake()
	{
        //create new commands here

        //create advance entry node command
        dialogueRunner.AddCommandHandler<string>
            (
            "advanceEntryNode",     // name of new yarn command
            AdvanceEntryNode        //name of c# method to run
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

}
