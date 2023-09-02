using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas; // Reference to your UI Canvas 
    public float test = 1;
    public WeaponController weaponController;
    //public bool testing = true;
    private bool testing = true;
    public bool isActive = true;

    public bool Testing
    {
        get
        {
            return testing;
        }
        set
        {
            // You can add validation or logic here if needed
            testing = value;
        }
    }

    void Start()
    {
        Debug.Log("this is shown now and can use a reset or something");
    }

    // Show the menu
    public void ShowMenu()
    {
        weaponController.setWeapon(" ");
        testing = true;
        menuCanvas.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    // Hide the menu
    public void HideMenu()
    {
        testing = false;
        isActive = false;
        menuCanvas.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    public void Disable()
    {
        menuCanvas.SetActive(false);
    }

    public bool menuIsActive()
    {
        return testing;
    }
}
