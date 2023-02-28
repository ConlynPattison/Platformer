using System;
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
    public GameObject stonePrefab;
    public GameObject coinPrefab;

    private Rigidbody _rbody;
    private Animator _animator;
    private bool _hitHead;
    private bool _isBoosted;
    private float _maxRun;
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _startPos = transform.position;
        _maxRun = maxSpeed;
    }

    void Update()
    {
        var position = transform.position;
        float horizontalAxis = Input.GetAxis("Horizontal");
        
        _rbody.velocity += horizontalAxis * Time.deltaTime * acceleration * Vector3.right;
        _rbody.velocity = new Vector3(Mathf.Clamp(_rbody.velocity.x, -maxSpeed, maxSpeed), _rbody.velocity.y,
            _rbody.velocity.z);

        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(position, Vector3.down, 0.4f);
        _hitHead = Physics.Raycast(position, Vector3.up, out hitInfo,2.0f);
        
        SetForward();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _rbody.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        { 
            _isBoosted = true;
        }
        else
        {
            _isBoosted = false;
        }

        maxSpeed = _isBoosted ? _maxRun + 3f : _maxRun;

        Color lineColor = isGrounded ? Color.green : Color.red;
        Color headHitColor = _hitHead ? Color.green : Color.red;
        
        Debug.DrawLine(position, position + Vector3.down * 0.4f, lineColor);
        Debug.DrawLine(position, position + Vector3.up * 2.0f, headHitColor);

        if (_hitHead && _rbody.velocity.y > 0f)
            CheckHit(hitInfo);

        float speed = _rbody.velocity.magnitude;
        _animator.SetFloat("Speed", speed);
        _animator.SetBool("Jumping", !isGrounded);
    }

    private void SetForward()
    {
        var velocity = _rbody.velocity;
        transform.localRotation = (velocity.x >= 0f)
            ? Quaternion.Euler(0f, 90f, 0f)
            : Quaternion.Euler(0f, 270f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.CoinCollected();
        }
        else if (other.CompareTag("Water"))
        {
            transform.position = _startPos;
        }
        else if (other.CompareTag("Flag"))
        {
            if (UITimer.OutOfTime)
                return;
            Debug.Log("Congrats! You've won!");
        }
    }

    private void CheckHit(RaycastHit hitInfo)
    {
        var position = hitInfo.transform.position;
        if (hitInfo.transform.gameObject.CompareTag("Brick"))
        {
            GameManager.BrickDestroyed();
            Destroy(hitInfo.transform.gameObject);
        }
        else if (hitInfo.transform.CompareTag("Question"))
        {
            Instantiate(stonePrefab, position, Quaternion.identity);
            Destroy(hitInfo.transform.gameObject);
            Instantiate(coinPrefab, new Vector3(position.x, position.y + 1f, position.z), Quaternion.identity);
        }
    }
}
