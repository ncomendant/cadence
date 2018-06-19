using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public ControllerSide side;

    public float horizontalMinRange;
    public float horizontalMaxRange;

    public float verticalMinRange;
    public float verticalMaxRange;

    private Game game;

    public float speed;

    void Start()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        Vector3 position = transform.position;
        position.y = Mathf.Round(Random.Range(verticalMinRange, verticalMaxRange) * 2f) / 2f;
        position.z = Mathf.Round(Random.Range(horizontalMinRange, horizontalMaxRange) * 2f) / 2f;
        transform.position = position;
    }

    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x <= -12f)
        {
            game.Emit(Events.MISS);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string otherName = other.gameObject.name;

        if ((side == ControllerSide.LEFT && otherName == "Controller (left)") || (side == ControllerSide.RIGHT && otherName == "Controller (right)"))
        {
            game.Emit(Events.GOOD_HIT);
        } else
        {
            game.Emit(Events.BAD_HIT);
        }
        Destroy(gameObject);
    }
}
