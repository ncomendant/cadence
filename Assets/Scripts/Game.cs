using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public float startTime;

    public int playerStartingHealth;
    public int enemyStartingHealth;

    private float projectileFrequency;


    private int playerHealth;
    private int enemyHealth;
    

    private EventManager eventManager;
    private float projectileCooldown;

    private void Awake()
    {
        eventManager = new EventManager();
    }


    // Use this for initialization
    void Start () {
        On(Events.GOOD_HIT, new GoodHitHandler(this));
        On(Events.BAD_HIT, new BadHitHandler(this));
        On(Events.MISS, new BadHitHandler(this));
        
        playerHealth = playerStartingHealth;
        enemyHealth = enemyStartingHealth;

        projectileCooldown = startTime;

        projectileFrequency = 1.1f - (float)Settings.difficulty;
    }   
	
	// Update is called once per frame
	void Update () {
        projectileCooldown -= Time.deltaTime;
        if (projectileCooldown <= 0f)
        {
            string resourceName = (Random.value < 0.5f) ? "Block" : "Orb";
            Instantiate(Resources.Load(resourceName));
            projectileCooldown = projectileFrequency;
        }
	}

    public void On(string eventName, EventHandler handler)
    {
        eventManager.On(eventName, handler);
    }

    public void Emit(string eventName, params object[] data)
    {
        eventManager.Emit(eventName, data);
    }

    private sealed class GoodHitHandler : EventHandler
    {
        private Game game;
        private GameObject enemyHealthBar;
        private readonly float startingScale;

        public GoodHitHandler(Game game)
        {
            this.game = game;
            enemyHealthBar = GameObject.Find("Enemy Health Bar");
            startingScale = enemyHealthBar.transform.localScale.x;
        }

        public void OnEvent(params object[] data)
        {
            game.enemyHealth--;

            float percent = (float)game.enemyHealth / (float)game.enemyStartingHealth;
            if (percent < 0f) percent = 0f;

            Vector3 scale = enemyHealthBar.transform.localScale;
            scale.x = percent * startingScale;
            enemyHealthBar.transform.localScale = scale;

            if (percent == 0f)
            {
                print("You win!");
            }
        }
    }

    private sealed class BadHitHandler : EventHandler
    {
        private Game game;
        private GameObject playerHealthBar;
        private readonly float startingScale;

        public BadHitHandler(Game game)
        {
            this.game = game;
            playerHealthBar = GameObject.Find("Player Health Bar");
            startingScale = playerHealthBar.transform.localScale.z;
        }

        public void OnEvent(params object[] data)
        {
            game.playerHealth--;
            float percent = (float)game.playerHealth / (float)game.playerStartingHealth;
            if (percent < 0f) percent = 0f;
            Vector3 scale = playerHealthBar.transform.localScale;
            scale.z = percent * startingScale;
            playerHealthBar.transform.localScale = scale;
            if (percent == 0f)
            {
                print("You lose!");
            }
        }
    }
}
