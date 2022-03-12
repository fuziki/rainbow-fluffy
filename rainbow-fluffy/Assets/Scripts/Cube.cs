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

    private string prevHitTag = "";

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
        float distance = 0.1f;
        var origin = transform.position + new Vector3(0, -1.45f, 0);
        var ray = new Ray(origin, transform.up * -1);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))        {
            var prev = prevHitTag;
            prevHitTag = hit.collider.tag;
            if (prev == hit.collider.tag) return;
            switch(hit.collider.tag)
            {
                case "Floor":
                    jumping = 0;
                    break;
                case "Jumper":
                    jumping = 0;
                    break;
                case "Goal":
                    jumping = 0;
                    _onGameClear.OnNext(Unit.Default);
                    break;
                default:
                    break;
            }
        }
        else
        {
            prevHitTag = "";
        }

        if (rb.velocity.y < -50)
        {
            Debug.Log($"v: {rb.velocity.y}");
        }
        else if (rb.useGravity)
        {
            rb.AddForce(new Vector3(0, gravityPower, 0));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Jumper")
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.up * jumpPower * 1.5f, ForceMode.Impulse);
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
