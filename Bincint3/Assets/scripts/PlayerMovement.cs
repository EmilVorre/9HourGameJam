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

    public SpriteRenderer holdingSpriteRenderer;
    public AudioSource audioSource;

    Direction newestDirection = Direction.left;

    TileSystemManager tileSystemManager;
    UtilManager utilManager;

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

    private void Start()
    {
        tileSystemManager = TileSystemManager.Instance;
        utilManager = UtilManager.Instance;
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
        else if (_moveInput.x < -deadZone)
        {
            newestDirection = Direction.left;
        }
        else if (_moveInput.y  > deadZone)
        {
            newestDirection = Direction.up;
        }
        else if (_moveInput.y  < -deadZone)
        {
            newestDirection = Direction.down;
        }
    }

    private void Update()
    {        
        var (cellPos, tileData, valid) = tileSystemManager.GetNeighboringUtilInDirection(transform.position, newestDirection);

        if (valid == false)
        {
            return;
        }

        bool ghostInteracted = _playerActions.Player.Ghost_Interaction.WasPressedThisFrame();
        bool ghoulInteracted = _playerActions.Player.Ghoul_Interaction.WasPressedThisFrame();

        if (_isGhost && ghostInteracted
            || _isGhost == false && ghoulInteracted)
        {
            utilManager.TryInteract(cellPos, tileData.sprite, this);
        }

    }
}
