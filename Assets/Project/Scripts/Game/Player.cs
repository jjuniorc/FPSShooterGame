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
}
