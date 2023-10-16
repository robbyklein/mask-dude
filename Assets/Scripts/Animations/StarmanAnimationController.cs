using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarmanAnimationController : MonoBehaviour {
    private Animator animator;
    private Moves moves;
    private Jumps jumps;
    private Rigidbody2D rb;

    public enum State {
        Idle = 0,
        Running = 1,
        Jumping = 2,
    }

    private State currentState = State.Idle;
    [SerializeField] private float runThreshold = 0.5f;
    private float velX = 0;
    private float velY = 0;

    private void Start() {
        moves = GetComponent<Moves>();
        jumps = GetComponent<Jumps>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Debug.Log(currentState);
        velX = Mathf.Abs(rb.velocity.x);
        velY = Mathf.Abs(rb.velocity.y);

        if (jumps.IsGrounded && velX > runThreshold) {
            ChangeState(State.Running);
        } else if (!jumps.IsGrounded && velY > 0.1f) {
            ChangeState(State.Jumping);
        } else {
            ChangeState(State.Idle);
        }

    }

    private void ChangeState(State newState) {
        switch (newState) {
            case State.Idle:
                animator.SetInteger("State", 0);
                break;
            case State.Running:
                animator.SetInteger("State", 1);
                break;
            case State.Jumping:
                animator.SetInteger("State", 2);
                break;
        }

        currentState = newState;
    }
}
