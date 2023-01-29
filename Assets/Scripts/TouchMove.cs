using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float speed = 0.1f;

    private void Start()
    {
        cam = Camera.main;
    }
    void FixedUpdate()
    {
        Vector2 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Vector3.MoveTowards(transform.position, mouse+new Vector2(0f,1f), speed);
    }
}
