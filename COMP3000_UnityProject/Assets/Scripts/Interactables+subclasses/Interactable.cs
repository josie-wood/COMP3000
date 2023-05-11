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
	public GameObject secondUiPrompt;
	public KeyCode interactKey;
	public bool withinRange;
	public DialogueRunner dialogueRunner;
	public string startNode;
	public YarnManager yarnManager;

	private void Awake()
	{
		yarnManager = GameObject.FindGameObjectWithTag("YarnManager").GetComponent<YarnManager>();
		dialogueRunner = yarnManager.dialogueRunner;
	}


	private void Update()
	{
		if (withinRange)
		{
			if (Input.GetKeyUp(interactKey))
			{
				if(!yarnManager.IsDialogueRunning())
				{
					Debug.Log("interactionKeyPressed " + interactKey);

					yarnManager.startingDialogue(startNode);
				}
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

	public void updateUIPrompt()
	{
		uiPrompt.SetActive(false);
		//in a case where ui prompt changes once and that's it
		uiPrompt = secondUiPrompt;
		uiPrompt.SetActive(true);
	}
}
