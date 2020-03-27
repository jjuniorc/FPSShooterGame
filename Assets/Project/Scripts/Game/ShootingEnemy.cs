using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public float shootingInterval = 4f;
    public float shootingDistance = 10f;
    
    private Player player;
    private float shootingTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        shootingTimer = Random.Range(0, shootingInterval);
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer -= Time.deltaTime;

        //Only shooting if timer allows and is closer enough to the player
        if(shootingTimer <= 0 && Vector3.Distance(this.transform.position, player.transform.position) <= shootingDistance) 
        {
            Debug.Log("Time to shoot");
            shootingTimer = shootingInterval;

            GameObject bullet = ObjectPoolingManager.Instance.GetBullet();
            bullet.transform.position = this.transform.position;
            bullet.transform.forward = (player.transform.position - this.transform.position).normalized;
        }
    }
}
