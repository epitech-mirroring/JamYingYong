using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject _player;
    private bool _isGrounded;
    private bool _oldIsGrounded = false;
    private Vector3 _oldVelocity;
    public float groundSpeed = 2f;
    public float jumpForce = 2f;
    private BoxCollider _hitbox;
    private Rigidbody _rigidbody;
    private Vector2 _move;
    public bool isWhite = false;
    public GameObject[] eyes;
    public GameObject mesh;
    public Material whiteMaterial;
    public Material blackMaterial;

    private void Start()
    {
        _player = gameObject;
        _rigidbody = _player.GetComponent<Rigidbody>();
        _hitbox = GetComponent<BoxCollider>();
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (!_isGrounded) return;
        
        var state = context.ReadValueAsButton();
        if (state)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }
    
    public void SwitchForm(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWhite = !isWhite;
            foreach (var eye in eyes)
            {
                eye.layer = !isWhite ? LayerMask.NameToLayer("Bloom") : LayerMask.NameToLayer("Default");
                eye.SetActive(!isWhite);
            }
            mesh.layer = isWhite ? LayerMask.NameToLayer("Bloom") : LayerMask.NameToLayer("Default");
            mesh.GetComponent<SkinnedMeshRenderer>().material = isWhite ? whiteMaterial : blackMaterial;
        }
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        
        // Update position
        if (_isGrounded && _oldIsGrounded)
        {
            _rigidbody.velocity = new Vector3(_move.x * groundSpeed, _rigidbody.velocity.y, _move.y * groundSpeed);
            _oldVelocity = _rigidbody.velocity;
        }
        if (!_oldIsGrounded && _isGrounded)
            _rigidbody.velocity = Vector3.zero;
        if (_oldIsGrounded && !_isGrounded)
            _rigidbody.velocity = new Vector3(_oldVelocity.x, _rigidbody.velocity.y, _oldVelocity.z);

        _oldIsGrounded = _isGrounded; 
    }

    public bool IsGrounded()
    {
        // Check if the player is on the ground
        var position = _player.transform.position + (Vector3.down * _hitbox.bounds.extents.y);
        position.y += 0.01f;
        var ground = Physics.Raycast(position, Vector3.down, out var hit, Mathf.Infinity);
        
        Debug.DrawRay(position, Vector3.down * hit.distance, Color.red);
        return Mathf.Abs(_rigidbody.velocity.y) <= 0.01f && (ground && hit.collider.gameObject.layer == LayerMask.NameToLayer("Terrain"));
    }
}
