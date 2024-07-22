using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleChecker : MonoBehaviour
{
    public static RuleChecker Instance { get; set; }
    [Header("Common")]
    [Space(2)]
    public GameObject player;
    [Header("Speed")]
    [Space(2)]
    public int currentMaxSpeed = 80;
    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
