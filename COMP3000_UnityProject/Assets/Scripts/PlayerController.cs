using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    //variables
    public float moveSpeed = 1f;
    public Rigidbody2D playerRB;
    public Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRB= GetComponent<Rigidbody2D>();
        currentPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void FixedUpdate()
	{
		currentPos = GetComponent<Transform>().position;
		Vector2 playerPos= playerRB.position;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horInput, vertInput);
        Vector2 movement = inputVector * moveSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        playerRB.MovePosition(newPos);

	}
}
