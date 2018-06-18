using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackText : MonoBehaviour {

    public float fadeTime = 0.5f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        fadeTime -= Time.deltaTime;
        if (fadeTime <= 0f) Destroy(gameObject);
	}

    public void Init(BlockRating rating, float position)
    {
        transform.Translate(0f, 0f, position);
        TextMesh mesh = GetComponentInChildren<TextMesh>();
        if (rating == BlockRating.MISS)
        {
            mesh.text = "Miss!";
            mesh.color = Color.red;
        } else if (rating == BlockRating.BAD)
        {
            mesh.text = "Bad!";
            mesh.color = Color.gray;
        } else if (rating == BlockRating.GOOD)
        {
            mesh.text = "Good!";
            mesh.color = Color.green;
        } else if (rating == BlockRating.PERFECT)
        {
            mesh.text = "Perfect!";
            mesh.color = Color.blue;
        }
    }
}
