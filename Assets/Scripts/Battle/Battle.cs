using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour {

    public float startTime;

    private EventManager eventManager;

    private float projectileCooldown;
    private float projectileFrequency;
    private bool active;

    private float endGameCountdown;

    private void Awake()
    {
        eventManager = GameObject.Find("Main").GetComponent<EventManager>();
    }

    // Use this for initialization
    void Start () {

        projectileCooldown = startTime;
        projectileFrequency = 1.1f - (float)Settings.difficulty;

        endGameCountdown = 5;

        if (XRSettings.enabled)
        {
            GameObject.Find("FPS Character").SetActive(false);
            GameObject rig = GameObject.Find("[CameraRig]");
            DisplayMessage("Directions:\nLeft controller for blue blocks.\nRight controller for red orbs.", startTime);
        }
        else
        {
            GameObject.Find("VR Character").SetActive(false);
            DisplayMessage("Directions:\nLeft click for blue blocks.\nRight click for red orbs.", startTime);
        }

        eventManager.On(BattleEvent.GOOD_HIT, new GoodHitHandler(this));
        eventManager.On(BattleEvent.BAD_HIT, new BadHitHandler(this));
        eventManager.On(BattleEvent.MISS, new MissHandler(this));
        eventManager.On(BattleEvent.PLAYER_WIN, new WinHandler(this));
        eventManager.On(BattleEvent.PLAYER_LOSE, new LoseHandler(this));

        active = true;
    }


    void Update()
    {
        if (active)
        {
            projectileCooldown -= Time.deltaTime;
            if (projectileCooldown <= 0f)
            {
                string resourceName = (Random.value < 0.5f) ? "Block" : "Orb";
                Instantiate(Resources.Load(resourceName));
                projectileCooldown = projectileFrequency;
            }
        } else
        {
            print(endGameCountdown);
            endGameCountdown -= Time.deltaTime;
            if (endGameCountdown <= 0f)
            {
                print("done");
                SceneManager.LoadScene("Menu");
            }
        }
    }

    private void DisplayMessage(string text, float duration)
    {
        GameObject message = (GameObject)Instantiate(Resources.Load("Message"));
        message.GetComponent<Message>().duration = duration;
        message.GetComponentInChildren<TextMesh>().text = text;
    }

    private sealed class GoodHitHandler : EventHandler
    {
        private Battle battle;

        public GoodHitHandler(Battle battle)
        {
            this.battle = battle;
        }

        public void OnEvent(params object[] data)
        {
            battle.eventManager.Emit(BattleEvent.DAMAGE_ENEMY);
        }
    }

    private sealed class BadHitHandler : EventHandler
    {
        private Battle battle;

        public BadHitHandler(Battle battle)
        {
            this.battle = battle;
        }

        public void OnEvent(params object[] data)
        {
            battle.eventManager.Emit(BattleEvent.DAMAGE_PLAYER);
        }
    }

    private sealed class MissHandler : EventHandler
    {
        private Battle battle;

        public MissHandler(Battle battle)
        {
            this.battle = battle;
        }

        public void OnEvent(params object[] data)
        {
            battle.eventManager.Emit(BattleEvent.DAMAGE_PLAYER);
        }
    }

    private sealed class WinHandler : EventHandler
    {
        private Battle battle;

        public WinHandler(Battle battle)
        {
            this.battle = battle;
        }

        public void OnEvent(params object[] data)
        {
            battle.active = false;
            battle.DisplayMessage("You win!", battle.endGameCountdown);
        }
    }

    private sealed class LoseHandler : EventHandler
    {
        private Battle battle;

        public LoseHandler(Battle battle)
        {
            this.battle = battle;
        }

        public void OnEvent(params object[] data)
        {
            battle.active = false;
            battle.DisplayMessage("You lose.", battle.endGameCountdown);
        }
    }
}
