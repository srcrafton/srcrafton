using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //The moveSpeed variable controls how fast the character moves.
    //The jumpForce variable controls how high the character jumps.
    //The crouchScale variable controls how much the character's height scales down when crouching.

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float crouchScale = 0.5f;

    //The isGrounded and isCrouching variables keep track of whether the character is on the ground or crouching respectively.
    //The inputDirection vector stores the horizontal and vertical input from the player.

    private bool isGrounded;
    private bool isCrouching;
    private Rigidbody rb;
    private Vector3 inputDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //In the Update function, the script checks for keyboard input for jumping and crouching. 
        //It also updates the inputDirection vector based on the player's horizontal and vertical input.

        inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopCrouch();
        }
    }

    private void FixedUpdate()
    {
        //In the FixedUpdate function, the script checks if the character is on the ground, 
        //and if so, moves the character based on the inputDirection vector using the rigidbody's MovePosition function.

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f);

        Move();
    }

    private void Move()
    {
        Vector3 movement = inputDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);
    }

    private void Jump()
    {
        //The Jump function adds an upward force to the character's rigidbody to make it jump.

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Crouch()
    {
        //The Crouch and StopCrouch functions adjust the character's scale to simulate crouching.
        
        isCrouching = true;
        transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
    }

    private void StopCrouch()
    {
        isCrouching = false;
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
    }
}