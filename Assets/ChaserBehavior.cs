using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserBehavior : MonoBehaviour {
    private NavMeshAgent _agent;

    private Transform _player;
    // Start is called before the first frame update
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        Chase();
    }

    void Chase() {
        _agent.SetDestination(_player.position);
    }
}
