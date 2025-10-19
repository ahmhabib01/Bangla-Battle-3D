using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotController : MonoBehaviour
{
    public enum Personality { Aggressive, Sneaky, Coward, Supportive }
    public Personality personality = Personality.Aggressive;
    private NavMeshAgent agent;
    public Transform[] waypoints;
    private int idx = 0;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (waypoints != null && waypoints.Length > 0) agent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if (agent.pathPending) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            PatrolNext();
        }
    }

    void PatrolNext()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        idx = (idx + 1) % waypoints.Length;
        agent.SetDestination(waypoints[idx].position);
    }
}
