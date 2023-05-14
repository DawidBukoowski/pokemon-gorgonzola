using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private float moveDistance = 0.1f;
    [SerializeField] private GameInput gameInput;
    
    
    private bool isRunning;
    private Vector3 lastDirection;
    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than one Player instance!");
        }

        Instance = this;
    }
    
    void Update() {
        var inputVector = gameInput.GetMovementVector();
        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        const float playerRadius = .5f;
        const float playerHeight = 2f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir
            // Attempt only X movement
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            }
            else {
                // Cannot move only X movement
                // Attempt only Z movement
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }
        
        isRunning = moveDir != Vector3.zero;
        if (isRunning) {
            lastDirection = moveDir;
        }

    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetLastDirection() {
        return lastDirection;
    }
    
}
