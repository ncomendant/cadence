using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    private Menu menu;
    private bool selected = false;


    // Use this for initialization
    void Start () {
        menu = GameObject.Find("Main").GetComponent<Menu>();
        menu.On(MenuEvent.TRIGGER_CLICKED, new TriggerClickedHandler(this));
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter(Collider other)
    {
        selected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        selected = false;
    }

    private sealed class TriggerClickedHandler : EventHandler
    {
        private PlayButton playButton;

        public TriggerClickedHandler(PlayButton playButton)
        {
            this.playButton = playButton;
        }

        public void OnEvent(params object[] data)
        {
            if (playButton.selected)
            {
                SceneManager.LoadScene("Battle");
            }
        }
    }
}
