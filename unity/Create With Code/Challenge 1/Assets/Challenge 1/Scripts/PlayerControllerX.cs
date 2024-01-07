using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 1;
    public float rotationSpeed = 10;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var propellerObject = transform.Find("Propeller").gameObject;

        if (propellerObject != null)
        {
            propellerObject.transform.Rotate(Vector3.forward, rotationSpeed);
            //MeshFilter propellerMeshFilter = propellerObject.GetComponent<MeshFilter>();
        }
        else
        {
            Debug.Log("Propeller object not found");
        }

        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed);

        float angle = verticalInput * rotationSpeed * Time.deltaTime;
        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(Vector3.right, angle);
    }
}
