using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
	}

    public void Init(int typeIndex)
    {
        BlockType type = BlockType.VALUES[typeIndex];
        transform.Translate(0f, 0f, type.position);
        GetComponentInChildren<TextMesh>().text = type.text;
    }
}
