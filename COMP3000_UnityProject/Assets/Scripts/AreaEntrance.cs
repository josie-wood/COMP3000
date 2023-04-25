using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : Interactable
{
    public string scenePointedTo;
    public SceneManagement sceneManagement;
    public bool areaAccessible;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (withinRange)
		{
			if (Input.GetKeyUp("space") && areaAccessible)
			{
                goToNewArea();
			}
            else if (Input.GetKeyUp("space") && !areaAccessible)
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
