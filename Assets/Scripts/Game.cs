using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public float blockInterval;
    private float blockCooldown;

	// Use this for initialization
	void Start () {
        blockCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        blockCooldown -= Time.deltaTime;
        if (blockCooldown <= 0f)
        {
            MakeRandomBlock();
            blockCooldown = blockInterval;
        }
	}

    private void MakeRandomBlock()
    {
        int index = Random.Range(0, 8);
        GameObject blockObj = (GameObject)Instantiate(Resources.Load("Block"));
        Block block = blockObj.GetComponent<Block>();
        block.Init(index);
    }
}
