using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    private Vector3 _startPosition;
    private float _moveSpeed = -0.3f;
    private bool gaming = false;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (!gaming) return;
        this.transform.Translate(_moveSpeed, 0, 0);
    }

    public void StartGame()
    {
        gaming = true;
    }

    public void StopGame()
    {
        gaming = false;
    }

    public void ResetToStart()
    {
        this.transform.position = _startPosition;
    }
}
