using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Kod terce

    [SerializeField] private float HP = 1;

    public void Hit(float receavedDamage)
    {
        HP -= receavedDamage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
