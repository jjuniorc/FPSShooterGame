using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private float resetTimer = 3f; //Seconds before go back to menu

    private bool gameOver = false;

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
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You win!\nGood Job!";
        }

        if(player.Killed == true)
        {
            gameOver = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "You lose!\nTry Again!";
        }

        if(gameOver == true)
        {
            resetTimer -= Time.deltaTime;
            if(resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }

    }
}
