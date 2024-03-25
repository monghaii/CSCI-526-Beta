using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class FirstPersonManager : MonoBehaviour
{
    public static FirstPersonManager instance;
    
    public GameManager gm;
    public Enemy enemyInstance;
    
    [Header("Health")] 
    public bool isDead = false;

    public static bool isFpsPaused = true;
    // also need to connect this to the UI
    private HealthBar healthBar;

    public DialogueRunner dialogueRunnerInstance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFpsPaused = false;
        //Time.timeScale = 0;
        // Todo: have it read from the game manager instead??
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyInstance = GameObject.Find("Enemy").GetComponent<Enemy>();
        /*else
        {
            currentHealth = maxHealth;
        }*/
        healthBar = GameManager.instance.healthBar;

    }

    // Update is called once per frame
    void Update()
    {
        //Determine if is dead (enemy or player)
        if (gm.currentHealth <= 0.0f)
        {
            //This is temporary - preventing infinite call
            gm.currentHealth = 0.01f;
            GameManager.instance.ExitDialogue();
        }
        if (enemyInstance.currentHealth <= 0.0f)
        {
            isDead = true;
        }
        
        if (isDead)
        {
            //TODO: different death behavior for enemy dead and player dead
            gm.EndFPS(true);
        }
        healthBar.SetHealthPercentage(gm.maxHealth, gm.currentHealth);
    }
    
    public void TakeDamage(float dmg)
    {
        gm.currentHealth -= dmg;
        // DEPRECATED: Update() is taking over control of this part
        // if (currentHealth <= 0f)
        // {
        //     isDead = true;
        // }
    }

    public void RefreshHealth()
    {
        gm.currentHealth = gm.currentHealth;
    }
  
}
