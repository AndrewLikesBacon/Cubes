using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyTextFadeOut : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeDelayPercentage;
    [SerializeField] private float movePower;
    private float timeCreated;
    private float timeAlive;
    private Vector2 moveDirection;

    void Start() {
        
        timeCreated = Time.time;
        moveDirection = new Vector2(Random.Range(-360, 360), Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(moveDirection * movePower, ForceMode2D.Impulse);

    }

    void Update()
    {

        timeAlive = Time.time - timeCreated;

        GetComponent<TextMeshPro>().color = new Color(255, 255, 0, (1 - timeAlive / fadeTime) + fadeDelayPercentage);

        if ((Time.time - timeCreated) > fadeTime + fadeDelayPercentage) {

            Destroy(gameObject);

        }
        
    }
}
