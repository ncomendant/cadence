using UnityEngine;

public class HealthBar : MonoBehaviour {

    public float maxHealth;
    public bool ownedByPlayer;

    private EventManager eventManager;
    private float currentHealth;
    private Transform bar;
    private Transform text;
    private TextMesh textMesh;

    private DamageHandler damageHandler;

	void Start () {
        eventManager = GameObject.Find("Main").GetComponent<EventManager>();
        currentHealth = maxHealth;

        bar = transform.Find("Bar");
        text = transform.Find("Text");
        textMesh = text.GetComponent<TextMesh>();

        damageHandler = new DamageHandler(this);

        if (ownedByPlayer)
        {
            eventManager.On(BattleEvent.DAMAGE_PLAYER, damageHandler);
        } else
        {
            eventManager.On(BattleEvent.DAMAGE_ENEMY, damageHandler);
        }

        UpdateText();
	}
	
	void Update () {
        
	}

    private void Dispose()
    {
        if (ownedByPlayer)
        {
            eventManager.Remove(BattleEvent.DAMAGE_PLAYER, damageHandler);
        }
        else
        {
            eventManager.Remove(BattleEvent.DAMAGE_ENEMY, damageHandler);
        }
        Destroy(gameObject);
    }

    private void UpdateText()
    {
        textMesh.text = (int)currentHealth + " / " + (int)maxHealth;
    }

    private sealed class DamageHandler : EventHandler
    {
        private HealthBar healthBar;
        private readonly float startingScale;

        public DamageHandler(HealthBar healthBar)
        {
            this.healthBar = healthBar;
            startingScale = healthBar.bar.localScale.z;
        }

        public void OnEvent(params object[] data)
        {
            healthBar.currentHealth--;

            float percent = healthBar.currentHealth / healthBar.maxHealth;
            if (percent < 0f) percent = 0f;
            Vector3 scale = healthBar.bar.transform.localScale;
            scale.z = percent * startingScale;
            healthBar.bar.transform.localScale = scale;

            healthBar.UpdateText();

            if (percent == 0f)
            {
                if (healthBar.ownedByPlayer)
                {
                    healthBar.eventManager.Emit(BattleEvent.PLAYER_LOSE);
                } else
                {
                    healthBar.eventManager.Emit(BattleEvent.PLAYER_WIN);
                }
                this.healthBar.Dispose();
            }
        }
    }
}
