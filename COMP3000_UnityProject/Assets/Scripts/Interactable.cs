using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //primary interactable class
    //specific npc and item classes inherit from here

    public Collider2D range;
    public GameObject uiPrompt;
	public string interactKey;
	private bool withinRange;

	private void Start()
	{
		if(interactKey == "")
		{
			interactKey = "space";
		}
	}
	private void Update()
	{
		Debug.Log("blahblah");
		if (Input.GetKeyDown("space"))
		{
			Debug.Log("interactionKeyPressed " + interactKey);
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
}
