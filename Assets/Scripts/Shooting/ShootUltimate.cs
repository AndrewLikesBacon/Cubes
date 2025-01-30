using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShootUltimate : MonoBehaviour
{
    
    private int ultCharge;
    [SerializeField] private int ultChargeRequirement;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform gunOffset;
    private bool ultimateEquiped = false;
    [SerializeField] private int rocketSpeed;
    [SerializeField] private Image ultChargeBar;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    [SerializeField] private AudioSource explosionSound;

    void Update()
    {
        if (ultCharge >= ultChargeRequirement) {

            if (Input.GetKeyDown(KeyCode.Q)) {

                EquipUltimate();

            }

        }

        if (ultimateEquiped && Input.GetKeyDown(KeyCode.Mouse0)) {
            
            UseUltimate();

        }
    }

    private void EquipUltimate() {

        GetComponent<SwapWeapons>().UnequipAllWeapons();
        ultimateEquiped = true;

    }

    private void UseUltimate() {

        GameObject rocket = Instantiate(rocketPrefab, gunOffset.position, transform.rotation);
        rocket.GetComponent<Explosive>().explosionSound = explosionSound;
        Rigidbody2D rb = rocket.GetComponent<Rigidbody2D>();
        rb.velocity = rocketSpeed * transform.up;
        GetComponent<SwapWeapons>().EquipCurrentWeapon();
        ultimateEquiped = false;
        ultCharge = 0;
        ultChargeBar.fillAmount = (float)ultCharge / ultChargeRequirement;
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

    }

    public void AddUltCharge(int amount){

        ultCharge += amount;
        ultChargeBar.fillAmount = (float)ultCharge / ultChargeRequirement;

    }

    public void SetUltChargeRequirement(int n) {
        
        ultChargeRequirement = n;

    }
}
