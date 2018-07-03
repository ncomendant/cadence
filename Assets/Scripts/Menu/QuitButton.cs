using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class QuitButton : MonoBehaviour {

    private Menu menu;
    private bool selected = false;

    // Use this for initialization
    void Start()
    {
        menu = GameObject.Find("Main").GetComponent<Menu>();
        menu.On(ClickEvent.PRIMARY, new ClickedHandler(this));
    }

    // Update is called once per frame
    void Update()
    {

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
        private QuitButton quitButton;

        public ClickedHandler(QuitButton quitButton)
        {
            this.quitButton = quitButton;
        }

        public void OnEvent(params object[] data)
        {
            if (XRSettings.enabled)
            {
                if (quitButton.selected)
                {
                    Application.Quit();
                }
            }
            else
            {
                GameObject hoveredObject = (GameObject)data[0];
                if (hoveredObject == this.quitButton.gameObject)
                {
                    Application.Quit();
                }
            }
        }
    }
}
