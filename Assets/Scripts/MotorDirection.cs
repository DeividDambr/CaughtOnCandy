using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorDirection : MonoBehaviour
{
    private Rigidbody2D rbObject;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftAngle;
    [SerializeField] private float rightAngle;

    bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        rbObject = GetComponent<Rigidbody2D>();
        movingClockwise = true;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    public void ChangeMoveDir()
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }

    }

    public void Move()
    {
        ChangeMoveDir();

        if (movingClockwise)
        {
            rbObject.angularVelocity = moveSpeed;
        }

        if (!movingClockwise)
        {
            rbObject.angularVelocity = -1 * moveSpeed;
        }
    }
}