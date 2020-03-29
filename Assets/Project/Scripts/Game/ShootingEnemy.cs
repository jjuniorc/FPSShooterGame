using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public float shootingInterval = 4f;
    public float shootingDistance = 10f;
    public float shootSpeed = 30;
    public float chasingInterval = 2f; //Every 2 seconds enemy will check the distance for chase player
    public float chasingDistance = 12f;
    
    private Player player;
    private float shootingTimer;
    private float chasingTimer;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        agent = GetComponent<NavMeshAgent>();
        shootingTimer = Random.Range(0, shootingInterval);

        agent.SetDestination(player.transform.position); //Enemy chase the player
    }

    // Update is called once per frame
    void Update()
    {
        if(player.Killed == true)
        {
            //Stop enemy, don't need to keep chasing
            StopShootingEnemyMovement();
        }

        //*** SHOOTING LOGIC
        shootingTimer -= Time.deltaTime;

        //Only shooting if timer allows and is closer enough to the player
        if(shootingTimer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= shootingDistance) 
        {
            //Debug.Log("Time to shoot");
            shootingTimer = shootingInterval;

            GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false, shootSpeed);
            bullet.transform.position = this.transform.position;
            bullet.transform.forward = (player.transform.position - this.transform.position).normalized;
        }

        //*** CHASING LOGIC
        //Chase player check
        chasingTimer -= Time.deltaTime;
        if(chasingTimer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;

            agent.SetDestination(player.transform.position); //Enemy chase the player
        }
    }

    private void StopShootingEnemyMovement()
    {
        //agent.Stop(); //Stop Nav Mesh
        agent.enabled = false; //Disable Nav Mesh
        this.enabled = false; //Disable ShootingEnemy
    }

    protected override void OnKill()
    {
        base.OnKill();

        StopShootingEnemyMovement();

        //Down to the floor
        this.transform.localEulerAngles  = new Vector3(10, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
    }
}
