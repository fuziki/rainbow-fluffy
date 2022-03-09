using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;

    float jumpPower = 18;
    float gravityPower = -36;

    int jumping = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && jumping < 2)
        {
            jumping++;
            Jump();
        }

        if (this.transform.position.y < -5)
        {
            Debug.Log("Game Over");
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < -50)
        {
            Debug.Log($"v: {rb.velocity.y}");
        }
        else
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
            Debug.Log("Goal!!!");
        }
    }

    public void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }
}
