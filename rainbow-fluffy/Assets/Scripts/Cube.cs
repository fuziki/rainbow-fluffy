using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var y = Mathf.Max(this.transform.position.y - 0.01f, 0);
        this.transform.position = new Vector3(0, y, 0);
    }

    public void Jump()
    {
        this.transform.position += new Vector3(0, 3, 0);
    }
}
