using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DeleteBullet : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private float timeCreated;

    void Start() {
        
        timeCreated = Time.time;

    }

    void Update()
    {

        if ((Time.time - timeCreated) > lifeSpan) {

            Destroy(gameObject);

        }
        
    }
}
