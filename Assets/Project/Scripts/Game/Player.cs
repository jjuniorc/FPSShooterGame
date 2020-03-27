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
                ammo--;
                //Debug.Log("Fire");
                //GameObject bulletObject = Instantiate(bulletPrefab);
                //bulletObject.transform.position = playerCamera.transform.position + (playerCamera.transform.forward * 3);
                //bulletObject.transform.forward = playerCamera.transform.forward;
                GameObject bulletObject = ObjectPoolingManager.Instance.GetBullet();
                bulletObject.transform.position = playerGun.transform.position + (playerGun.transform.forward);
                bulletObject.transform.forward = playerGun.transform.forward;
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

        
        if(hit.collider.GetComponent<AmmoCrate>() != null) //Collect ammo crate
        {
            //AmmoCrate ammoCrate = collision.gameObject.GetComponent<AmmoCrate>();
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
        else if(hit.collider.GetComponent<Enemy>() != null) //Touching enemies
        {
            if(isHurt == false)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                health -= enemy.damage;
                isHurt = true;

                //Perform the knockback effect
                Vector3 hurtDirection = (this.transform.position - enemy.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }
        }
    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;
    }
}
