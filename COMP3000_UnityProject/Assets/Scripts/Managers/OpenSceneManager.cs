using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSceneManager : MonoBehaviour
{
    // OpenScene Manager deals with all behaviour to do with the Opening Scene.

    private SceneManagement sceneManagement;
    // Start is called before the first frame update
    void Start()
    {
        sceneManagement = GameObject.FindObjectOfType<SceneManagement>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            sceneManagement.loadMenu();
        }
    }
}
