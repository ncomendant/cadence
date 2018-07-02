using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DifficultyButton : MonoBehaviour {

    public float difficulty;
    public bool selected = false;

    private bool hovered = false;
    private EventManager eventManager;

	// Use this for initialization
	void Start () {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        if (selected)
        {
            Settings.difficulty = difficulty;
            textMesh.color = Color.yellow;
        } else
        {
            textMesh.color = Color.gray;
        }

        eventManager = GameObject.Find("Main").GetComponent<EventManager>();
        eventManager.On(MenuEvent.PRIMARY_CLICKED, new TriggerClickedHandler(this, textMesh));
        eventManager.On(MenuEvent.DIFFICULTY_SET, new DifficultySetHandler(textMesh));
	}

    private void OnTriggerEnter(Collider other)
    {
        hovered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hovered = false;
    }

    private sealed class DifficultySetHandler : EventHandler
    {
        private TextMesh textMesh;

        public DifficultySetHandler(TextMesh textMesh)
        {
            this.textMesh = textMesh;
        }

        public void OnEvent(params object[] data)
        {
            textMesh.color = Color.gray;
        }
    }

    private sealed class TriggerClickedHandler : EventHandler
    {
        private DifficultyButton difficultyButton;
        private TextMesh textMesh;

        public TriggerClickedHandler(DifficultyButton difficultyButton, TextMesh textMesh)
        {
            this.difficultyButton = difficultyButton;
            this.textMesh = textMesh;
        }

        public void OnEvent(params object[] data)
        {
            if (XRSettings.enabled)
            {
                if (difficultyButton.hovered)
                {
                    difficultyButton.eventManager.Emit(MenuEvent.DIFFICULTY_SET);
                    Settings.difficulty = difficultyButton.difficulty;
                    textMesh.color = Color.yellow;
                }
            } else
            {
                GameObject hoveredObject = (GameObject)data[0];
                if (hoveredObject == this.difficultyButton.gameObject)
                {
                    difficultyButton.eventManager.Emit(MenuEvent.DIFFICULTY_SET);
                    Settings.difficulty = difficultyButton.difficulty;
                    textMesh.color = Color.yellow;
                }
            }
        }
    }
}
