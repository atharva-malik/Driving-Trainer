using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TMP_Text countdown;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Player player;
    [SerializeField] private Slider acceleratorPressureSlider;
    [SerializeField] private TMP_Text acceleratorPressureText;

    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
        playerObj = player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        acceleratorPressureText.text = Math.Round(Math.Abs(player.acceleratorPressure)).ToString() + "%";
        acceleratorPressureSlider.value = Math.Abs(player.acceleratorPressure/100);
        if (playerObj.transform.position.y < 1 || Input.GetKeyDown(KeyCode.R)){
            playerObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerObj.GetComponent<Player>().resetWheels();
            playerObj.transform.position = respawnPoint.position;
            playerObj.transform.rotation = respawnPoint.rotation;
            player.acceleratorPressure = 0;
        }
    }

    IEnumerator Countdown()
    {
        player.enabled = false;

        countdown.text = "3";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "2";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "1";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "";

        player.enabled = true;
    }
}
