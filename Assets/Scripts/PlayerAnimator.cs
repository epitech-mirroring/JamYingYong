using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private GameObject _player;
    public Animator animator;
    public PlayerController playerController;
    private Rigidbody _rigidbody;
    private bool _isOrientedRight = true;

    void Start()
    {
        _player = gameObject;
        _rigidbody = _player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_rigidbody.velocity.x > 1 || _rigidbody.velocity.x < -1) {
            if ((!_isOrientedRight && _rigidbody.velocity.x > 0) ||
            (_isOrientedRight && _rigidbody.velocity.x < 0)) {
                _player.transform.Rotate(0, 180, 0);
                _isOrientedRight = !_isOrientedRight;
            }
            animator.SetBool("isRunning", true);
        }
        if (_rigidbody.velocity.x <= 1 && _rigidbody.velocity.x >= -1)
            animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", !playerController.IsGrounded());
    }
}
