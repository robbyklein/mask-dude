using System;
using UnityEngine;

public class MovesWithInput : MonoBehaviour {
    private Rigidbody2D rb;
    private MovementHelpers movementHelpers;
    private Jumps jumps;
    private Moves moves;
    [SerializeField] private InputHandlerSO input;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float accelerationTime = 0.1f;
    private Vector2 movement;

    private void OnEnable() {
        input.OnGameMovement += HandleMove;
        input.OnGameJump += HandleJump;
    }

    private void OnDisable() {
        input.OnGameMovement -= HandleMove;
        input.OnGameJump -= HandleJump;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        movementHelpers = GetComponent<MovementHelpers>();
        jumps = GetComponent<Jumps>();
        moves = GetComponent<Moves>();
    }

    private void FixedUpdate() {
        moves.Move(movement.x);
    }

    private void HandleMove(Vector2 newMovement) {
        movement = newMovement;
    }

    private void HandleJump() {
        jumps.Jump();
    }
}
