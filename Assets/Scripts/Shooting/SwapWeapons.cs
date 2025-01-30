using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapons : MonoBehaviour
{
    
    [SerializeField] private int weaponSlotCount;
    [SerializeField] private float specialWeaponTimer;
    private int weaponSlot = 1;
    private int specailWeaponSlot = 1;
    private float deactivateSpecialWeaponTime;
    private bool specialWeaponInUse = false;
    
    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {

            if (specialWeaponInUse) {

                EquipCurrentWeapon();

            } else {

                EquipNextWeapon();

            }

        }

        if (Time.time > deactivateSpecialWeaponTime && specialWeaponInUse) {

            EquipCurrentWeapon();

        }

        if (Input.GetKeyDown(KeyCode.R)) {

            EquipSpecialWeapon(specailWeaponSlot);

        }

    }

    public void EquipWeapon(int weapon) {

        weaponSlot = weapon;
        UnequipAllWeapons();

        switch(weaponSlot) {

            case 1:

                GetComponent<Shoot>().UpdateLastFireTime();
                GetComponent<Shoot>().SetFireSingle(false);
                GetComponent<Shoot>().enabled = true;
                break;

            case 2:

                GetComponent<ShootShotgun>().UpdateLastFireTime();
                GetComponent<ShootShotgun>().SetFireSingle(false);
                GetComponent<ShootShotgun>().enabled = true;
                break;

            case 3:

                GetComponent<ShootSniper>().UpdateLastFireTime();
                GetComponent<ShootSniper>().SetFireSingle(false);
                GetComponent<ShootSniper>().enabled = true;
                break;
                
        }

    }

    private void EquipNextWeapon() {

        if (weaponSlot < weaponSlotCount) {

            weaponSlot++;

        } else {

            weaponSlot = 1;

        }

        EquipWeapon(weaponSlot);

    }

    public void EquipCurrentWeapon() {

        UnequipAllWeapons();

        switch(weaponSlot) {

            case 1:

                GetComponent<Shoot>().UpdateLastFireTime();
                GetComponent<Shoot>().SetFireSingle(false);
                GetComponent<Shoot>().enabled = true;
                break;

            case 2:

                GetComponent<ShootShotgun>().UpdateLastFireTime();
                GetComponent<ShootShotgun>().SetFireSingle(false);
                GetComponent<ShootShotgun>().enabled = true;
                break;
            
            case 3:

                GetComponent<ShootSniper>().UpdateLastFireTime();
                GetComponent<ShootSniper>().SetFireSingle(false);
                GetComponent<ShootSniper>().enabled = true;
                break;

        }
        
    }

    public void UnequipAllWeapons() {

        GetComponent<Shoot>().enabled = false;
        GetComponent<ShootShotgun>().enabled = false;
        GetComponent<ShootSniper>().DisableLineRenderer();
        GetComponent<ShootSniper>().enabled = false;
        GetComponent<ShootMinigun>().EndShotSound();
        GetComponent<ShootMinigun>().enabled = false;
        specialWeaponInUse = false;
        
    }

    private void EquipSpecialWeapon(int weapon) {

        specailWeaponSlot = weapon;
        UnequipAllWeapons();

        switch(specailWeaponSlot) {

            case 1:

                GetComponent<ShootMinigun>().UpdateLastFireTime();
                GetComponent<ShootMinigun>().SetFireSingle(false);
                GetComponent<ShootMinigun>().enabled = true;
                deactivateSpecialWeaponTime = Time.time + specialWeaponTimer;
                break;

        }

        specialWeaponInUse = true;

    }
}
