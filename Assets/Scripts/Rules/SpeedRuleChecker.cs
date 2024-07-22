using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRuleChecker : MonoBehaviour
{
    private GameObject player;
    private int currentMaxSpeed;
    private float currentSpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = RuleChecker.Instance.player;
        currentSpeed = player.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        currentMaxSpeed = RuleChecker.Instance.currentMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = player.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        currentSpeed = (float)Math.Floor(currentSpeed);
        currentMaxSpeed = RuleChecker.Instance.currentMaxSpeed;
        if (currentSpeed > currentMaxSpeed)
            Debug.LogWarning("SPEED LIMIT EXCEEDED" + currentSpeed);
    }
}
