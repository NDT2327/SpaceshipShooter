using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //speed
    public float speeed = 5f;
    //Rigidbody
    private Rigidbody2D rb;
    //move input
    private Vector2 moveInput;
    //main camera
    private Camera mainCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Ensure Rigibody2D exists
        if (rb == null)
        {
            Debug.Log("Rigidbody2D component not found on this GameObject");
            enabled = false;
            return;
        }

        //RESTRICT the player to the main camera's boundaries
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene");
            enabled = false;
            return;
        }

        //set Rigidbody2D properties for 2D movement
        rb.freezeRotation = true;


    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    //Function to move the character
    void MoveCharacter()
    {
        //Calculate the movement vector
        Vector2 movement = moveInput * speeed * Time.fixedDeltaTime;
        //Apply the movement to the rigidbody2D
        rb.MovePosition(rb.position + movement);





    }
}
