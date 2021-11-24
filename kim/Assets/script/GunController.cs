using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Gun startingGun;
    Gun equippedGun;
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        if (startingGun != null)
            EquipGun(startingGun);
    }
    public void EquipGun(Gun gunToEquip)
    {
        if(equippedGun !=null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.localRotation);
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot(txt);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
