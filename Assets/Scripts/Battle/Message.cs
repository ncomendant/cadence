﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

    public float duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration <= 0f)
        {
            Destroy(gameObject);
        }
	}
}
