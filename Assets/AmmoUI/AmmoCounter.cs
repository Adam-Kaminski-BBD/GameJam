using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    public GameObject bulletIconPrefab; // Reference to the bullet icon prefab
    public int maxAmmo = 6; // Maximum ammo capacity
    private int currentAmmo; // Current ammo count

    void Start()
    {
        maxAmmo = 6;
        currentAmmo = 6;
        CreateBulletIcons();
        Debug.Log("maximum occpancy " + maxAmmo);
    }

    void CreateBulletIcons()
    {
        for (int i = 0; i < maxAmmo; i++)
        {
            // Instantiate a bullet icon
            GameObject bulletIcon = Instantiate(bulletIconPrefab, transform);
            bulletIcon.SetActive(true);
            // Position bullet icons based on the loop index
            // You may need to adjust the positions depending on your UI layout
            bulletIcon.transform.localPosition = new Vector3(i * 30, 0, 0); // Adjust the X position as needed
        }
    }

    // ... (other parts of your script for updating the counter)

    // For example, you can use this method to update the bullet icons when ammo decreases
    void UpdateBulletIcons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Disable or enable bullet icons based on the remaining ammo
            transform.GetChild(i).gameObject.SetActive(i < currentAmmo);
        }
    }

    public int GetAmmo()
    {
        Debug.Log("AmmoGo" + maxAmmo);
        Debug.Log("AmmoGo2 " + currentAmmo);
        return maxAmmo;
    }

    public void AmmoGo()
    {
        Debug.Log("Ammo go is called here and will do its thiog");
        maxAmmo--;
        UpdateBulletIcons();
    }
    public void SetStupidAmmo(int ammo)
    {
        maxAmmo = ammo;
    }
}
