using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage = 10;
    public int getDamage()
    {

        return damage;
    }
    public void Hit()
    {
        if (gameObject.tag != "Player" && gameObject.tag != "Enemy")
        {

            Destroy(gameObject);
        }

    }
}
