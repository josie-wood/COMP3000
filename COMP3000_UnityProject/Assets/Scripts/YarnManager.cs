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

	public void Awake()
	{
        //create new commands here

        //create advance entry node command
        dialogueRunner.AddCommandHandler<string>
            (
            "advanceEntryNode",     // name of new yarn command
            AdvanceEntryNode        //name of c# method to run
            );
	}


    public void AdvanceEntryNode(string newEntryNode)
    {
        NPC.updateStartNode(newEntryNode);

    }

}
