using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickSound : MonoBehaviour
{
    public List<Button> targetButtons;
    public AK.Wwise.Event buttonClickSound;

    void Start()
    {
        foreach (Button button in targetButtons)
        {
            if (button != null)
                button.onClick.AddListener(() => OnButtonClick(button));
            else
                Debug.LogWarning("One of the target buttons is not assigned in the Inspector.");
        }
    }

    void OnButtonClick(Button button)
    {
        // Debug.Log(button.name + " clicked.");
        buttonClickSound.Post(gameObject);
    }
}
