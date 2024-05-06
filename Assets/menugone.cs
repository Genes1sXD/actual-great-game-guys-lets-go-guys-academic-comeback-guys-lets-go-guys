using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menu; // Reference to the menu GameObject

    private void Start()
    {
        Button button = GetComponent<Button>(); // Get the Button component attached to this GameObject
        button.onClick.AddListener(OnClick); // Add a listener for the click event
    }

    private void OnClick()
    {
        Time.timeScale = 1f;
        menu.SetActive(false); // Deactivate the menu GameObject when the button is clicked
    }
}
