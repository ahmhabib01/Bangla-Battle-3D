using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [Header("AI Settings")]
    public BotType botType;
    public float detectionRange = 10f;
    public float attackRange = 3f;
    public int health = 100;
    
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    
    private bool isPlayerDetected = false;
    private float attackCooldown = 2f;
    private float lastAttackTime = 0f;
    
    public enum BotType
    {
        Aggressive,
        Defensive,
        Sneaky,
        Support
    }
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        InitializeBotBehavior();
    }
    
    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerDetected = true;
            
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            isPlayerDetected = false;
            Patrol();
        }
        
        UpdateAnimations();
    }
    
    void InitializeBotBehavior()
    {
        switch (botType)
        {
            case BotType.Aggressive:
                detectionRange = 15f;
                attackRange = 4f;
                agent.speed = 3.5f;
                break;
            case BotType.Defensive:
                detectionRange = 8f;
                attackRange = 2.5f;
                agent.speed = 2.5f;
                break;
            case BotType.Sneaky:
                detectionRange = 12f;
                attackRange = 2f;
                agent.speed = 4f;
                break;
        }
    }
    
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    void AttackPlayer()
    {
        // Stop moving when attacking
        agent.SetDestination(transform.position);
        
        // Look at player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
        // Attack cooldown
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }
    
    void Patrol()
    {
        // Simple patrol behavior - move to random points
        if (!agent.hasPath || agent.remainingDistance < 1f)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10f;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
    }
    
    void PerformAttack()
    {
        animator.SetTrigger("Attack");
        
        // Raycast to check if player is in front
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                PlayerController playerController = hit.collider.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(10);
                }
            }
        }
    }
    
    void UpdateAnimations()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetBool("IsAttacking", Time.time - lastAttackTime < 1f);
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        // Drop coins or items
        GameManager.Instance.AddCoins(50);
        
        // Death animation
        animator.SetTrigger("Die");
        
        // Disable collider and AI
        GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        
        // Destroy after animation
        Destroy(gameObject, 3f);
    }
}
