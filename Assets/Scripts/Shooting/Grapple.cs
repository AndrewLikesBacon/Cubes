using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D playerRB;
    public Movement playerMovement;
    [SerializeField] private GameObject grappleShotPrefab;
    [SerializeField] private float grappleShotSpeed;
    [SerializeField] private Transform gunOffset;
    private static bool hasShotGrapple = false;

    public bool GetHasShotGrapple() {

        return hasShotGrapple;

    }

    public void SetHasShotGrapple(bool b) {

        hasShotGrapple = b;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1) && !hasShotGrapple) {

            ShootGrapple();
            hasShotGrapple = true;

        }
    }

    public void ShootGrapple() {
        
        GameObject grappleShot = Instantiate(grappleShotPrefab, gunOffset.position, transform.rotation);
        Rigidbody2D rb = grappleShot.GetComponent<Rigidbody2D>();
        rb.velocity = grappleShotSpeed * transform.up;
        grappleShot.GetComponent<GrapplePull>().player = this.player;
        grappleShot.GetComponent<GrapplePull>().playerRB = this.playerRB;
        grappleShot.GetComponent<GrapplePull>().playerMovement = this.playerMovement;
        grappleShot.GetComponent<GrapplePull>().grapple = this;
        
    }
}
