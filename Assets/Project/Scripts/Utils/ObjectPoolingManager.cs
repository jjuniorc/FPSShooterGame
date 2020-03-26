using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    
    public static ObjectPoolingManager Instance 
    {
        get
        {
            return instance;
        }
    }

    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    private List<GameObject> bullets;

    // Initialization (Awake run before Start method)
    void Awake()
    {
        instance = this;

        //Peload bullets
        bullets = new List<GameObject>();

        for(int i = 0; i < bulletAmount; i++)
        {
            GameObject prefabInstance = GetNewBullet(true);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if(bullet.activeInHierarchy == false)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        //All bullets in use, create a new just for this moment
        GameObject prefabInstance = GetNewBullet(false);
        return prefabInstance;
    }

    private GameObject GetNewBullet(bool sleeping)
    {
            GameObject prefabInstance = Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(this.transform);
            if(sleeping)
            {
                prefabInstance.SetActive(false);
            }

            bullets.Add(prefabInstance); //Add new bullet to pool

            return prefabInstance;
            
    }
}
