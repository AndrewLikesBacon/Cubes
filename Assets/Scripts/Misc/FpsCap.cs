using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCap : MonoBehaviour
{
    public int maxFPS;

    void Awake() {
        Application.targetFrameRate = maxFPS;
    }
}
