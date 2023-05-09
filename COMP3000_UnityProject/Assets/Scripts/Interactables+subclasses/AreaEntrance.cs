using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : Interactable
{
    public string scenePointedTo;
    public SceneManagement sceneManagement;
    public bool areaAccessible;

	private void Start()
	{
        areaAccessible = yarnManager.canExploreWoods;
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
