using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveDistance = 3f;
    [SerializeField] private float jumpForce = 5f;
    
    private bool isRunning;
    private Vector3 lastDirection;
    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than one Player instance!");
        }
        Instance = this;
    }

    void Start()
    {
        gameInput.JumpAction += GameInput_JumpAction;
    }

    private void GameInput_JumpAction(object sender, System.EventArgs e)
    {
        Jump();
    }

    void Update() {
        HandleMovement();
    }

    void HandleMovement() {
        var inputVector = gameInput.GetMovementVector();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        const float playerRadius = .5f;
        const float playerHeight = 2f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (moveDir != Vector3.zero)
        {
            lastDirection = moveDir;

            playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        }
        else
        {
            playerRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

    }

    void Jump()
    {
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0, 0);
        playerRigidbody.velocity += Vector3.up * jumpForce;
    }

    void FixedUpdate()
    {
        var inputVector = gameInput.GetMovementVector();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        playerRigidbody.MovePosition(playerRigidbody.position + moveDir * moveDistance * Time.fixedDeltaTime);
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public Vector3 GetLastDirection()
    {
        return lastDirection;
    }
}
