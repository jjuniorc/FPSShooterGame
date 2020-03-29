using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int LEFT_MOUSE_BUTTON = 0;

    [Header("Visuals")]

    public Camera playerCamera;
    public GameObject playerGun;

    [Header("Gameplay")]
    public int initialAmmo = 12;
    public int initalHealth = 100;
    public float knockbackForce = 100;
    public float hurtDuration = 0.5f;
    public float shootSpeed = 100;
    
    
    private int ammo;
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    private int health;
    public int Health
    {
        get
        {
            return health;
        }
    }

    private bool killed;
    public bool Killed
    {
        get
        {
            return killed;
        }
    }

    private bool isHurt;

    // Start is called before the first frame update
    void Start()
    {
        ammo = initialAmmo;
        health = initalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            if(ammo > 0)
            {
                if(killed == false)
                {
                    ammo--;
                    //Debug.Log("Fire");
                    //GameObject bulletObject = Instantiate(bulletPrefab);
                    //bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward * 3);
                    //bulletObject.transform.forward = playerCamera.transform.forward;
                    GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet(true, shootSpeed);
                    bulletObject.transform.position = playerGun.transform.position + (playerGun.transform.forward);
                    bulletObject.transform.forward = playerGun.transform.forward;
                }
            }
        }
    }

    //Check for collisions
    //void OnCollisionEnter(Collision collision)
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(hit.collider.name);
        //if(collision.gameObject.GetComponent<AmmoCrate>() != null)
        if(hit.collider.GetComponent<AmmoCrate>() != null)
        {
            //AmmoCrate ammoCrate = collision.gameObject.GetComponent<AmmoCrate>();
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }

        if(hit.collider.GetComponent<HealthCrate>() != null)
        {
            //AmmoCrate ammoCrate = collision.gameObject.GetComponent<AmmoCrate>();
            HealthCrate healthCrate = hit.collider.GetComponent<HealthCrate>();
            health += healthCrate.health;

            Destroy(healthCrate.gameObject);
        }
    }

    //Check for collisions by TriggerEnter
    //void OnControllerColliderHit(ControllerColliderHit hit)
    void OnTriggerEnter(Collider otherCollider)
    {
        //Debug.Log(collision.gameObject.name);
        //Debug.Log(hit.collider.name);
        //if(collision.gameObject.GetComponent<AmmoCrate>() != null)

        /*
        if(otherCollider.GetComponent<AmmoCrate>() != null) //Collect ammo crate
        {
            //AmmoCrate ammoCrate = collision.gameObject.GetComponent<AmmoCrate>();
            AmmoCrate ammoCrate = otherCollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
        */

        if(isHurt == false)
        {
            GameObject hazard = null;

            if(killed == false)
            {
                if(otherCollider.GetComponent<Enemy>() != null) //Touching enemies
                {
                    Enemy enemy = otherCollider.GetComponent<Enemy>();
                    if(enemy.Killed == false)
                    {
                        hazard = enemy.gameObject;
                        health -= enemy.damage;
                    }
                }
                else if(otherCollider.GetComponent<Bullet>() != null) //Bullet 
                {
                    Bullet bullet = otherCollider.GetComponent<Bullet>();

                    if(bullet.ShotByPlayer == false) //From Enemy
                    {
                        hazard = bullet.gameObject;

                        health -= bullet.damage;

                        bullet.gameObject.SetActive(false);
                    }
                }
            }
            

            if(hazard != null)
            {
                isHurt = true;
                
                //Perform the knockback effect
                Vector3 hurtDirection = (this.transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }

            if(health <= 0)
            {
                if(killed == false)
                {
                    killed = true;

                    OnKill();
                }
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }

    private void OnKill()
    {
        GetComponent<CharacterController>().enabled = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
    }
}
