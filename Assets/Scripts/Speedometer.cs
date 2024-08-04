using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody playerToKeepTrackOf;
    [SerializeField] private bool isPhysicalSpeedometer;

    [Space(10)]
    [SerializeField] private float needleZeroRotation;
    [SerializeField] private float needleMaxRotation;

    private Player player;
    private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        player = playerToKeepTrackOf.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPhysicalSpeedometer){
            float rot = Map(playerToKeepTrackOf.velocity.magnitude, 0, player.maxSpeed, needleZeroRotation, needleMaxRotation);
            gameObject.transform.Find("Needle").localEulerAngles = new Vector3(0, 0, rot);
        }
        
        else
            text.text = Mathf.Floor(playerToKeepTrackOf.velocity.magnitude).ToString() + " mps      |       " + Mathf.Floor(playerToKeepTrackOf.velocity.magnitude * 3.6f).ToString() + " kmph";
    }

    public float Map (float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
