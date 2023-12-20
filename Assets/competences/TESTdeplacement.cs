using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    private float speed = 10.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movedirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Input.mousePosition.z));

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movedirection = new Vector3(horizontalInput, 0, verticalInput);
        transform.Translate(movedirection * speed * Time.deltaTime);
    }
}