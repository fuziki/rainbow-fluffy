using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;

    float jumpPower = 3000;
    float gravityPower = -700;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody.velocity.y < -50) {
            Debug.Log($"v: {rigidbody.velocity.y}");
        }
        else
        {
            rigidbody.AddForce(new Vector3(0, gravityPower, 0));
        }
    }

    public void Jump()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
    }
}
