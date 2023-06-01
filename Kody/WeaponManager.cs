using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Kod/informace zbrane (udelany tak aby fungoval na vice zbrani)

    [Header("Weapon Options")]
    public float damage;
    public float reservoirSize;
    public float fireSpeed;
    public bool automatic;

    private GameObject hand;

    bool isEquipped = false;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hand = GameObject.Find("Hand");
        if (fireSpeed == 0 || !automatic)
        {
            fireSpeed = 10f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player") && Input.GetKey(KeyCode.E) && !isEquipped)
        {
            isEquipped = true;
            rb.isKinematic = true;
            transform.SetParent(hand.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            Physics.IgnoreLayerCollision(3, 6);

            // ---------------------------------------------------
            WeaponUser weaponUser = hand.GetComponent<WeaponUser>();
            weaponUser.GetWeaponData();
        }
    }

    public void WeaponDropped()
    {
        isEquipped = false;
        rb.isKinematic = false;
        transform.SetParent(null);
        Physics.IgnoreLayerCollision(3, 6, false);

        rb.AddForce(hand.transform.forward * 16f, ForceMode.Impulse);
    }
}
