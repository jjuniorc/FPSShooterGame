using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 5;
    public int damage = 5;

    private bool killed = false;
    public bool Killed
    {
        get
        {
            return killed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.GetComponent<Bullet>() != null)
        {
            Bullet bullet = hit.collider.GetComponent<Bullet>();

            if(bullet.ShotByPlayer == true)
            {
                health -= bullet.damage;

                bullet.gameObject.SetActive(false);

                if(health <= 0)
                {
                    if(killed == false)
                    {
                        killed = true;
                        OnKill();
                        //Destroy(this.gameObject);
                    }
                }
            }
        }
    }
    */

    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.GetComponent<Bullet>() != null)
        {
            Bullet bullet = otherCollider.GetComponent<Bullet>();

            if(bullet.ShotByPlayer == true)
            {
                health -= bullet.damage;

                bullet.gameObject.SetActive(false);

                if(health <= 0)
                {
                    if(killed == false)
                    {
                        killed = true;
                        OnKill();
                        //Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    protected virtual void OnKill()
    {

    }
}
