using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private GameInput gameInput;
    
    private bool isWalking;
    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than one Player instance!");
        }

        Instance = this;
    }
    
    void Update() {
        var inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * (moveDistance * Time.deltaTime);

        isWalking = moveDir != Vector3.zero;
        
    }
}
