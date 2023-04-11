using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    //primary interactable class
    //specific npc and item classes inherit from here

    public Collider2D range;
    public GameObject uiPrompt;
	public string interactKey;
	private bool withinRange;
	public DialogueRunner dialogueRunner;
	public string startNode;

	private void Start()
	{
		if(interactKey == "")
		{
			interactKey = "space";
		}
	}
	private void Update()
	{
		//Debug.Log("blahblah");
		if (withinRange)
		{
			if (Input.GetKeyUp("space"))
			{
				Debug.Log("interactionKeyPressed " + interactKey);

				// play node
				Debug.Log("trying to start the dialogue now");
				dialogueRunner.StartDialogue(startNode);
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        inInteractionRange();
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		outInteractionRange();
	}

	private void inInteractionRange()
    {
		//set bool
		withinRange= true;

		//show prompt
		if (uiPrompt != null)
		{
			uiPrompt.SetActive(true);
		}

    }

	private void outInteractionRange()
	{
		//set bool
		withinRange= false;

		//hide prompt
		if (uiPrompt != null)
		{
			uiPrompt.SetActive(false);
		}
		
	}

	public void updateStartNode(string newNode)
	{
		startNode= newNode;
	}
}
