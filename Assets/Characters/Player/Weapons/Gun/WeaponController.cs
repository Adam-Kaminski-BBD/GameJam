using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update
    private string weapon;

    public string Weapon
    {
        get
        {
            return weapon;
        }
        set
        {
            weapon = value;
        }
    }

    public void setWeapon(string weapon)
    {
        string sanitized = weapon.Replace("\"", "");
        Weapon = sanitized;
    }

    public string getWeapon()
    {
        return Weapon;
    }
}
