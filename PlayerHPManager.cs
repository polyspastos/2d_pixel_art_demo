using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPManager : MonoBehaviour
{
    public static PlayerHPManager instance;
    public float playerMaxHealth;
    public float playerCurrentHealth;
    public float healthBarLength;
    //public deathmenu theDeathScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth < 0)
        {
            gameObject.SetActive(false);
            // activate deathmenu
        }
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void DmgPlayer(float damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        playerCurrentHealth -= playerMaxHealth;
    }
}
