using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemis_Sensor : MonoBehaviour
{

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
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
 