using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlockingAgent : MonoBehaviour
{
    public PawnController controller;
    public float cohesionWeight = 1f;
    public float alignmentWeight = 1f;
    public float separationWeight = 1.5f;
    public float neighborRadius = 5f;
    public float separationRadius = 2f;

    List<FlockingAgent> flockMates;
    private NavMeshAgent agent;
    private Vector3 currentVelocity;

    private void OnEnable()
    {
        agent = controller.Unit.Agent;
    }

    void Update()
    {
        flockMates = BoardManager.Instance.GetSpawnByColor(controller.Team).flockMates;

        Vector3 cohesion = ComputeCohesion() * cohesionWeight;
        Vector3 alignment = ComputeAlignment() * alignmentWeight;
        Vector3 separation = ComputeSeparation() * separationWeight;

        Vector3 move = cohesion + alignment + separation;
        agent.SetDestination(transform.position + move);
    }

    Vector3 ComputeCohesion()
    {
        Vector3 center = Vector3.zero;
        int count = 0;

        foreach (var mate in flockMates)
        {
            if (mate == this || !IsNeighbor(mate)) continue;

            center += mate.transform.position;
            count++;
        }

        if (count == 0) return Vector3.zero;

        center /= count;
        return (center - transform.position).normalized;
    }

    Vector3 ComputeAlignment()
    {
        Vector3 averageVelocity = Vector3.zero;
        int count = 0;

        foreach (var mate in flockMates)
        {
            if (mate == this || !IsNeighbor(mate)) continue;

            averageVelocity += mate.agent.velocity;
            count++;
        }

        if (count == 0) return Vector3.zero;

        averageVelocity /= count;
        return averageVelocity.normalized;
    }

    Vector3 ComputeSeparation()
    {
        Vector3 avoidance = Vector3.zero;
        int count = 0;

        foreach (var mate in flockMates)
        {
            if (mate == this || !IsNeighbor(mate, separationRadius)) continue;

            avoidance += transform.position - mate.transform.position;
            count++;
        }

        if (count == 0) return Vector3.zero;

        avoidance /= count;
        return avoidance.normalized;
    }

    bool IsNeighbor(FlockingAgent mate, float radius = -1)
    {
        if (radius < 0) radius = neighborRadius;
        return Vector3.Distance(transform.position, mate.transform.position) < radius;
    }
}
