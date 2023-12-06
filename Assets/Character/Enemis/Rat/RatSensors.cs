using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RatSensors : MonoBehaviour
{

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerStay(Collider other)
    {
        agent.destination = other.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
