using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveXPosition : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] float speed;
    void Start()
    {

    }

    void FixedUpdate()
    {
        transform.localPosition += (Vector3.right * speed * Time.deltaTime);
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }

    public void MultiplicateSpeed(float m)
    {
        speed *= m;
    }
}
