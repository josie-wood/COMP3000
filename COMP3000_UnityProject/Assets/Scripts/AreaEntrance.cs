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
			if (Input.GetKeyUp("space"))
			{
                goToNewArea();
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
}
