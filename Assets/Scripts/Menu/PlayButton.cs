using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayButton : MonoBehaviour {

    private Menu menu;
    private bool selected = false;


    // Use this for initialization
    void Start () {
        menu = GameObject.Find("Main").GetComponent<Menu>();
        menu.On(ClickEvent.PRIMARY, new ClickedHandler(this));
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

    private sealed class ClickedHandler : EventHandler
    {
        private PlayButton playButton;

        public ClickedHandler(PlayButton playButton)
        {
            this.playButton = playButton;
        }

        public void OnEvent(params object[] data)
        {
            if (XRSettings.enabled)
            {
                if (playButton.selected)
                {
                    SceneManager.LoadScene("Battle");
                }
            } else
            {
                GameObject hoveredObject = (GameObject)data[0];
                if (hoveredObject == this.playButton.gameObject)
                {
                    SceneManager.LoadScene("Battle");
                }
            }
        }
    }
}
