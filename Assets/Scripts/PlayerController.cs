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
    public float blackJumpForce = 2f;
    public float whiteJumpForce = 5f;
    private BoxCollider _hitbox;
    private Rigidbody _rigidbody;
    private Vector2 _move;
    public bool isWhite = false;
    public GameObject[] eyes;
    public GameObject mesh;
    public Material whiteMaterial;
    public Material blackMaterial;
    public BoxCollider groundCollider;

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
            jumpForce = isWhite ? whiteJumpForce : blackJumpForce;
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
        if (!isWhite)
        {
            if (_isGrounded && _oldIsGrounded)
            {
                _rigidbody.velocity = new Vector3(_move.x * groundSpeed, _rigidbody.velocity.y, _move.y * groundSpeed);
                _oldVelocity = _rigidbody.velocity;
            }

            if (!_oldIsGrounded && _isGrounded)
                _rigidbody.velocity = Vector3.zero;
            if (_oldIsGrounded && !_isGrounded)
                _rigidbody.velocity = new Vector3(_oldVelocity.x, _rigidbody.velocity.y, _oldVelocity.z);
        }
        else
        {
            _rigidbody.velocity = new Vector3(_move.x * groundSpeed, _rigidbody.velocity.y, _move.y * groundSpeed);
            _oldVelocity = _rigidbody.velocity;
        }
        

        _oldIsGrounded = _isGrounded; 
    }

    public bool IsGrounded()
    {
        // Check if the player is on the ground
        var bounds = groundCollider.bounds;
        var iterations = 5;
        for (var i = 0; i < iterations; i++)
        {
            var ray = new Ray(new Vector3(bounds.min.x + bounds.size.x / iterations * i, bounds.min.y, bounds.min.z), Vector3.down);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject != gameObject && hit.distance < 0.3f)
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
                    return true;
                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                continue;
            }
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
        }
        return false;
    }
}
