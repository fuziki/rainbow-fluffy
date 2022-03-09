using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;

    float jumpPower = 18;
    float gravityPower = -36;

    int jumping = 0;

    public IObservable<Unit> OnGameClear => _onGameClear;
    private readonly Subject<Unit> _onGameClear = new Subject<Unit>();
    public IObservable<Unit> OnGameOver => _onGameOver;
    private readonly Subject<Unit> _onGameOver = new Subject<Unit>();

    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && jumping < 2)
        {
            jumping++;
            Jump();
        }

        if (this.transform.position.y < -15)
        {
            _onGameOver.OnNext(Unit.Default);
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < -50)
        {
            Debug.Log($"v: {rb.velocity.y}");
        }
        else if (rb.useGravity)
        {
            rb.AddForce(new Vector3(0, gravityPower, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            jumping = 0;
        }
        if (collision.collider.tag == "Goal")
        {
            jumping = 0;
            _onGameClear.OnNext(Unit.Default);
        }
    }

    private void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }

    public void ResetToStart()
    {
        this.transform.position = _startPosition;
    }

    public void Pause(bool isPaused)
    {
        Debug.Log($"Pause ${isPaused}");
        rb.useGravity = !isPaused;
        rb.velocity = Vector3.zero;
    }
}
