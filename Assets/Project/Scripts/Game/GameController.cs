using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enemyContainer;

    [Header("UI")]
    public Text ammoText;
    public Text healthText;
    public Text enemyText;
    public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + player.Ammo;
        healthText.text = "Health: " + player.Health;

        int aliveEnemies = 0;
        foreach(Enemy enemy in enemyContainer.GetComponentsInChildren<ShootingEnemy>())
        {
            if(enemy.Killed == false)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Enemies: " + aliveEnemies;

        if(aliveEnemies == 0)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You win!\nGood Job!";
        }

    }
}
