using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    //Kod prijimaci informace a pouziti zbrane

    [Header("Weapon Options")]
    [SerializeField] private float damage;
    [SerializeField] private float reservoirSize;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float amo;
    [SerializeField] private bool automatic;

    [Header("Transform")]
    [SerializeField] private Transform playerCamera;

    private float fireCooldown;

    private bool isEquipped = false;
    private bool needReload = false;
    private bool reloading = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && isEquipped && automatic && !needReload)
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = fireSpeed;
                amo -= 1;
                Debug.Log("Fire");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && isEquipped && !automatic && !needReload)
        {
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = fireSpeed;
                amo -= 1;
                Debug.Log("Fire");
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !reloading) { Debug.Log("Reloading"); Invoke(nameof(Reload),1); reloading = true; }
    
        if (Input.GetKeyDown(KeyCode.Q) && isEquipped)
        {
            DropWeapon();
        }

        if (fireCooldown > 0) { fireCooldown -= Time.deltaTime; }

        if (amo <= 0) { needReload = true; }
        else { needReload = false; }
    }

    private void Shoot()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.Hit(damage);
            }
        }
    }

    private void Reload()
    {
        amo = reservoirSize;
        reloading = false;
    }

    public void GetWeaponData()
    {
        isEquipped = true;
        WeaponManager weaponManager = GetComponentInChildren<WeaponManager>();
        damage = weaponManager.damage;
        reservoirSize = weaponManager.reservoirSize;
        fireSpeed = 1/weaponManager.fireSpeed;
        automatic = weaponManager.automatic;
        weaponManager.enabled = false;

        fireCooldown = fireSpeed;
        amo = reservoirSize;
    }

    private void DropWeapon()
    {
        isEquipped = false;
        WeaponManager weaponManager1 = GetComponentInChildren<WeaponManager>();
        damage = 0;
        reservoirSize = 0;
        fireSpeed = 0;
        weaponManager1.enabled = true;
        weaponManager1.WeaponDropped();
    }
}