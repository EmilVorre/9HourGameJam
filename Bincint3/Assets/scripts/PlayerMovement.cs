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


    void Awake()
    {
        _playerActions = new Ghost();

        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null)
            Debug.LogError("Rigidbody2D is NULL!");
    }

    // Update is called once per frame
    void Update()
    {
        
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
            _moveInput = _playerActions.Player.Ghost_Movement.ReadValue<Vector2>();
        else
            _moveInput = _playerActions.Player.Ghoul_Movement.ReadValue<Vector2>();

        _rbody.velocity = _moveInput * _speed;
    }
}
