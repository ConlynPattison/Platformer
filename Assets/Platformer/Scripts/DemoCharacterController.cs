using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharacterController : MonoBehaviour
{
    public float acceleration = 150f;
    public float maxSpeed = 10f;
    public float jumpForce = 5f;
    public float jumpBoost = 5f;
    public bool isGrounded;
    
    private Rigidbody _rbody;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        _rbody.velocity += horizontalAxis * Time.deltaTime * acceleration * Vector3.right;
        _rbody.velocity = new Vector3(Mathf.Clamp(_rbody.velocity.x, -maxSpeed, maxSpeed), _rbody.velocity.y,
            _rbody.velocity.z);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.15f);
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }

        Color lineColor = isGrounded ? Color.green : Color.red;
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.15f, lineColor);

        float speed = _rbody.velocity.magnitude;
        _animator.SetFloat("Speed", speed);
    }
}
