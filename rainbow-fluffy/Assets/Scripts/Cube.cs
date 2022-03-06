using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody rb;

    float jumpPower = 18;
    float gravityPower = -36;

    private bool touching = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    if (touching) return;
                    touching = true;
                    Jump();
                    break;
                case TouchPhase.Ended:
                    touching = false;
                    break;
            }
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

    public void Jump()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }
}
