using UnityEngine;

public class Projectile : MonoBehaviour {
    public ControllerSide side;

    public float horizontalMinRange;
    public float horizontalMaxRange;

    public float verticalMinRange;
    public float verticalMaxRange;

    private EventManager eventManager;

    private PrimaryClickHandler primaryClickHandler;
    private SecondaryClickHandler secondaryClickHandler;
    private EndGameHandler endGameHandler;

    public float speed;

    void Start()
    {
        Vector3 position = transform.position;
        position.y = Mathf.Round(Random.Range(verticalMinRange, verticalMaxRange) * 2f) / 2f;
        position.z = Mathf.Round(Random.Range(horizontalMinRange, horizontalMaxRange) * 2f) / 2f;
        transform.position = position;

        primaryClickHandler = new PrimaryClickHandler(this);
        secondaryClickHandler = new SecondaryClickHandler(this);
        endGameHandler = new EndGameHandler(this);

        eventManager = GameObject.Find("Main").GetComponent<EventManager>();
        eventManager.On(ClickEvent.PRIMARY, primaryClickHandler);
        eventManager.On(ClickEvent.SECONDARY, secondaryClickHandler);
        eventManager.On(BattleEvent.PLAYER_WIN, endGameHandler);
        eventManager.On(BattleEvent.PLAYER_LOSE, endGameHandler);
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x <= -12f)
        {
            eventManager.Emit(BattleEvent.MISS);
            Dispose();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string otherName = other.gameObject.name;

        if ((side == ControllerSide.LEFT && otherName == "Controller (left)") || (side == ControllerSide.RIGHT && otherName == "Controller (right)"))
        {
            eventManager.Emit(BattleEvent.GOOD_HIT);
        } else
        {
            eventManager.Emit(BattleEvent.BAD_HIT);
        }
        Dispose();
    }

    private void Dispose()
    {
        eventManager.Remove(ClickEvent.PRIMARY, primaryClickHandler);
        eventManager.Remove(ClickEvent.SECONDARY, secondaryClickHandler);
        eventManager.Remove(BattleEvent.PLAYER_WIN, endGameHandler);
        eventManager.Remove(BattleEvent.PLAYER_LOSE, endGameHandler);
        Destroy(gameObject);
    }

    private sealed class PrimaryClickHandler : EventHandler
    {
        private Projectile projectile;

        public PrimaryClickHandler(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public void OnEvent(params object[] data)
        {
            GameObject target = (GameObject) data[0];
            if (this.projectile.gameObject != target) return;

            if (this.projectile.side == ControllerSide.LEFT)
            {
                this.projectile.eventManager.Emit(BattleEvent.GOOD_HIT);
            } else
            {
                this.projectile.eventManager.Emit(BattleEvent.BAD_HIT);
            }
            this.projectile.Dispose();
        }
    }

    private sealed class EndGameHandler : EventHandler
    {
        private Projectile projectile;

        public EndGameHandler(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public void OnEvent(params object[] data)
        {
            this.projectile.Dispose();
        }
    }

    private sealed class SecondaryClickHandler : EventHandler
    {
        private Projectile projectile;

        public SecondaryClickHandler(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public void OnEvent(params object[] data)
        {
            GameObject target = (GameObject)data[0];
            if (this.projectile.gameObject != target) return;

            if (this.projectile.side == ControllerSide.RIGHT)
            {
                this.projectile.eventManager.Emit(BattleEvent.GOOD_HIT);
            }
            else
            {
                this.projectile.eventManager.Emit(BattleEvent.BAD_HIT);
            }
            this.projectile.Dispose();
        }
    }
}
