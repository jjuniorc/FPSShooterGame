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
    
    
    private int ammo;
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ammo = initialAmmo;
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
        Debug.Log(hit.collider.name);
        //if(collision.gameObject.GetComponent<AmmoCrate>() != null)
        if(hit.collider.GetComponent<AmmoCrate>() != null)
        {
            //AmmoCrate ammoCrate = collision.gameObject.GetComponent<AmmoCrate>();
            AmmoCrate ammoCrate = hit.collider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo;

            Destroy(ammoCrate.gameObject);
        }
    }
}
