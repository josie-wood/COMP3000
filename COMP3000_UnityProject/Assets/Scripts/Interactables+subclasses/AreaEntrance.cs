using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : Interactable
{
    public string scenePointedTo;
    public SceneManagement sceneManagement;
    public bool areaAccessible;
    public SpriteRenderer closedSign;

	private void Start()
	{
        areaAccessible = yarnManager.canExploreWoods;
        if(!areaAccessible)
        {
            if (closedSign) { closedSign.enabled = true; }
        }
        else
        {
            if (closedSign) { closedSign.enabled = false; }
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (withinRange)
		{
			if (Input.GetKeyUp(interactKey) && areaAccessible)
			{
				goToNewArea();
			}
            else if (Input.GetKeyUp(interactKey) && !areaAccessible)
            {
                Debug.Log("area is locked");
            }
		}
	}

    public void goToNewArea()
    {
        try
        {
            sceneManagement.loadGivenScene(scenePointedTo);
        }
        catch
        {

        }

    }

    public void setAreaAccessibility(bool newState)
    {
        areaAccessible = newState;
    }
}
