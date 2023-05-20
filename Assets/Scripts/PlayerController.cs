using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _player;
    private bool _isGrounded;
    private bool _oldIsGrounded = false;
    private Vector3 _oldVelocity;
    public List<GameObject> feet;
    public float groundSpeed = 5f;
    public float jumpForce = 5f;
    public float airSpeed = 5f;
    private BoxCollider _hitbox;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _player = gameObject;
        _rigidbody = _player.GetComponent<Rigidbody>();
        _hitbox = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        
        if (_isGrounded)
        {
            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
            
        // Move
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        if (!_oldIsGrounded && _isGrounded)
            _rigidbody.velocity = Vector3.zero;
        if (_isGrounded)
        {                
            var position = _player.transform.position;
            var oldPosition = position;
            position += move * ((_isGrounded ? groundSpeed : airSpeed) * Time.deltaTime);
            // Check if move made the player go through a wall
            if (Physics.Raycast(position, Vector3.right, out var hit, _hitbox.size.x / 2f * _player.transform.localScale.x))
            {
                Debug.DrawRay(position, Vector3.right * hit.distance, Color.red);
                if (hit.collider.gameObject.CompareTag("Terrain"))
                {
                    position.x = hit.point.x - _hitbox.size.x / 2f * _player.transform.localScale.x;
                }
            }
            else if (Physics.Raycast(   position, Vector3.left, out hit, _hitbox.size.x / 2f * _player.transform.localScale.x))
            {
                Debug.DrawRay(position, Vector3.left * hit.distance, Color.red);
                if (hit.collider.gameObject.CompareTag("Terrain"))
                {
                    position.x = hit.point.x + _hitbox.size.x / 2f * _player.transform.localScale.x;
                }
            }
            _player.transform.position = position;
            _oldVelocity = (position - oldPosition) / Time.deltaTime;
        }
        else if (_oldIsGrounded)
        {
            _rigidbody.AddForce(_oldVelocity, ForceMode.Impulse);
        }
        
        _oldIsGrounded = _isGrounded;
    }

    private bool IsGrounded()
    {
        // Check if the player is on the ground
        bool isGrounded = false;
        const float raycastDistance = 0.05f;
        
        for (int i = 0; i < feet.Count && !isGrounded; i++)
        {
            var foot = feet[i];
            var position = foot.transform.position;
            Physics.Raycast(position, Vector3.down, out var hit, raycastDistance);
            Debug.DrawRay(position, Vector3.down * raycastDistance, Color.red);
            isGrounded = hit.collider != null;
        }
        return isGrounded;
    }
}
