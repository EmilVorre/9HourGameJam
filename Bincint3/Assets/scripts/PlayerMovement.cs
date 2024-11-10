using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _isGhost;
    private Ghost _playerActions;
    private Rigidbody2D _rbody;
    private Vector2 _moveInput;

    Direction newestDirection = Direction.left;

    void Awake()
    {
        _playerActions = new Ghost();

        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null)
            Debug.LogError("Rigidbody2D is NULL!");
    }

    private void OnEnable()
    {
        _playerActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (_isGhost)
        {
            _moveInput = _playerActions.Player.Ghost_Movement.ReadValue<Vector2>();
        }
        else
        {
            _moveInput = _playerActions.Player.Ghoul_Movement.ReadValue<Vector2>();
        }

        _rbody.velocity = _moveInput * _speed;

        SetDirection();
    }

    private void SetDirection()
    {
        float deadZone = 0.2f;
        if (_moveInput.x > deadZone)
        {
            newestDirection = Direction.right;
        }
        else if (_moveInput.x < deadZone)
        {
            newestDirection = Direction.left;
        }
        else if (_moveInput.y  > deadZone)
        {
            newestDirection = Direction.up;
        }
        else if (_moveInput.y  < deadZone)
        {
            newestDirection = Direction.down;
        }
    }

    private void Update()
    {

    }
}
