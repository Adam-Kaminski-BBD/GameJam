using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    private string weapon = " ";

    public string Weapon
    {
        get
        {
            return weapon;
        }
        set
        {
            // You can add validation or logic here if needed
            weapon = value;
        }
    }

    public void setWeapon(string weapon) => Weapon = weapon;

    public string getWeapon()
    {
        return Weapon;
    }
}
