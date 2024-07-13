using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody playerToKeepTrackOf;

    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.Round(playerToKeepTrackOf.velocity.magnitude).ToString() + " mps      |       " + Mathf.Round(playerToKeepTrackOf.velocity.magnitude * 3.6f).ToString() + " kmph";
    }
}
