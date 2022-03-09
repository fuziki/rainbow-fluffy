using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField]
    Transform target;

    private float startHeight = 0;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = this.transform.position.y - target.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        var x = Mathf.Sin(count / 30) / 90;
        var current = this.transform.position.y;
        var tgt = this.target.position.y + startHeight;
        var diff = tgt - current;
        this.transform.Translate(new Vector3(x, diff * 0.04f, 0f));
    }
}
