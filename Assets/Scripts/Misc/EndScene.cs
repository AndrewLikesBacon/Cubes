using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{

    void Update() {

        if (Time.deltaTime > 10 || Input.GetKeyDown(KeyCode.Space)) {

            SceneManager.LoadScene("Level 1");

        }
    }
}
