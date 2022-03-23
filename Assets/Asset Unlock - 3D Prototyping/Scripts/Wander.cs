using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI;
public class Wander : MonoBehaviour
{
  
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent dungeonKeeper;
    private float timer;
    [SerializeField] private float rayMaxDistance;
    private Ray ray;
    private int layerMask;
    
    private RaycastHit hit;
    // Use this for initialization

    private void Start()
    {
        layerMask = 1 << 8;
   
    }

    void OnEnable()
    {
        dungeonKeeper = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            dungeonKeeper.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void FindPlayer()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, rayMaxDistance, layerMask))
        {
            Debug.Log("Game Over");
            GameManager.isGameOver = true;
           
        }
    }
}