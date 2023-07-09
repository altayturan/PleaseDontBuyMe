using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private bool isRandom;
    [SerializeField] private Transform target;

    [SerializeField] private List<Transform> targetNodes;
    private int _currentNodeIndex;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (isRandom)
            {
                case true:
                    MoveTargetToRandomNode(); break;
                case false:
                    MoveTargetToNextNode(); break;
            }
        }
    }

    private void MoveTargetToNextNode()
    {
        _agent.SetDestination(targetNodes[_currentNodeIndex].position);

        _currentNodeIndex++;
        if (_currentNodeIndex >= targetNodes.Count)
            _currentNodeIndex = 0;

    }

    private void MoveTargetToRandomNode()
    {
        int index = UnityEngine.Random.Range(0, targetNodes.Count);

        _agent.SetDestination(targetNodes[index].position);
    }
}
