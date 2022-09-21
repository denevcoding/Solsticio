using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float platformSpeed;
    public bool moving;
    public Rigidbody platformRB;

    public Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        platformRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            MovePlatform();
        }
    }


    void MovePlatform()
    {
        platformRB.MovePosition(Vector3.MoveTowards(platformRB.position, newPosition, platformSpeed * Time.deltaTime));

        if (Vector3.Distance(transform.position, newPosition) <= 0)
        {
            moving = false;
        }
    }


    public void SetNewPosition(Vector3 newPos)
    {
        newPosition = newPos;
    }

}
