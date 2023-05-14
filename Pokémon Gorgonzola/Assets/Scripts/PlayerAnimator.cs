using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string IS_RUNNING = "IsRunning";
    private const string X = "X";
    private const string Y = "Y";

    [SerializeField] private Player player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_RUNNING, player.IsRunning());
        animator.SetFloat(X, player.GetLastDirection().x);
        animator.SetFloat(Y, player.GetLastDirection().z);
    }
}