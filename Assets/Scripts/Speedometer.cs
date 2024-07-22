using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody playerToKeepTrackOf;

    private TMP_Text text;
    private float last_speed;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        last_speed = playerToKeepTrackOf.velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.Floor(playerToKeepTrackOf.velocity.magnitude).ToString() + " mps      |       " + Mathf.Floor(playerToKeepTrackOf.velocity.magnitude * 3.6f).ToString() + " kmph";
    }
}
