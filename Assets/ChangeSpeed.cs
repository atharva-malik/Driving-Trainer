using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    [SerializeField] private int newSpeed = 60;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            RuleChecker.Instance.currentMaxSpeed = newSpeed;
        }
    }
}
