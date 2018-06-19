using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour {

    private Game game;

    // Use this for initialization
    void Start () {
        game = GameObject.Find("Game").GetComponent<Game>();
        game.On(Event.BLOCK_DESPAWNED, new BlockDespawnedHandler());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private sealed class BlockDespawnedHandler : EventHandler
    {
        void EventHandler.OnEvent(params object[] data)
        {
            BlockType type = (BlockType)data[0];
            BlockRating rating = (BlockRating)data[1];

            if (rating == BlockRating.PERFECT) print("perfect!");
            else if (rating == BlockRating.GOOD) print("good!");
            else if (rating == BlockRating.OKAY) print("okay!");
            else if (rating == BlockRating.BAD) print("bad!");
            else if (rating == BlockRating.MISS) print("miss!");
        }
    }
}
