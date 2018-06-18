using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public float linePosition = -9f;
    public float triggerRange = 0.5f;
    public float blockInterval;

    private float blockCooldown;
    private List<Block> blocks;


	// Use this for initialization
	void Start () {
        blockCooldown = 0f;
        blocks = new List<Block>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
        UpdateBlocks(Time.deltaTime);
	}

    private void CheckInput()
    {
        for (int i = blocks.Count-1; i >= 0; i--) {
            Block block = blocks[i];
            float distance = Mathf.Abs(block.transform.position.x - linePosition);

            if (distance <= triggerRange && Input.GetKeyDown(block.type.keyCode))
            {
                if (distance <= triggerRange * 0.25) MakeFeedbackText(BlockRating.PERFECT, block.type.position);
                else if (distance <= triggerRange * 0.75) MakeFeedbackText(BlockRating.GOOD, block.type.position);
                else MakeFeedbackText(BlockRating.BAD, block.type.position);
                blocks.RemoveAt(i);
                Destroy(block.gameObject);
            }
        }
    }

    private void UpdateBlocks(float passedTime)
    {
        blockCooldown -= Time.deltaTime;
        if (blockCooldown <= 0f)
        {
            MakeRandomBlock();
            blockCooldown = blockInterval;
        }
        RemovePassedBlocks();
    }

    private void RemovePassedBlocks()
    {
        for (int i = blocks.Count-1; i >= 0; i--)
        {
            Block block = blocks[i];
            if (block.transform.position.x <= linePosition-triggerRange)
            {
                MakeFeedbackText(BlockRating.MISS, block.type.position);
                blocks.RemoveAt(i);
                Destroy(block.gameObject);
            }
        }
    }

    private void MakeFeedbackText(BlockRating rating, float position)
    {
        GameObject textObj = (GameObject)Instantiate(Resources.Load("Feedback Label"));
        FeedbackText text = textObj.GetComponent<FeedbackText>();
        text.Init(rating, position);
    }

    private void MakeRandomBlock()
    {
        int index = Random.Range(0, 8);
        GameObject blockObj = (GameObject)Instantiate(Resources.Load("Block"));
        Block block = blockObj.GetComponent<Block>();
        blocks.Add(block);
        block.Init(index);
    }
}
