﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy
{
    public AudioSource deathSound;
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
        Debug.Log("UPUP");
        if(this.Killed == true)
        {
            this.disappearDeadTimer -= Time.deltaTime;
            if(this.disappearDeadTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if(player.Killed == true)
            {
                //Stop enemy, don't need to keep chasing
                StopShootingEnemyMovement();

                //Stop Physics for Enemy: If isKinematic is enabled, Forces, collisions or joints will not affect the rigidbody anymore
                GetComponent<Rigidbody>().isKinematic = true;
            }

            //*** SHOOTING LOGIC
            shootingTimer -= Time.deltaTime;

            //Only shooting if timer allows and is closer enough to the player
            if(shootingTimer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= shootingDistance) 
            {
                //Debug.Log("Time to shoot");
                shootingTimer = shootingInterval;

                GameObject bullet = ObjectPoolingManager.Instance.GetBullet(false, shootSpeed);

                Vector3 enemyBulletPosition = this.transform.position;
                //enemyBulletPosition.x += 2f;
                //enemyBulletPosition.z += 1f;
                bullet.transform.position = enemyBulletPosition;
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
    }

    private void StopShootingEnemyMovement()
    {
        agent.Stop(); //Stop Nav Mesh
        agent.enabled = false; //Disable Nav Mesh
        //this.enabled = false; //Disable ShootingEnemy
    }

    protected override void OnKill()
    {
        base.OnKill();

        deathSound.Play();

        StopShootingEnemyMovement();

        //Down to the floor
        this.transform.localEulerAngles  = new Vector3(10, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
    }
}
