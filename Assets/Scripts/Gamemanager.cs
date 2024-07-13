using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TMP_Text countdown;
    [SerializeField] private TMP_Text LeaderBoard;
    [SerializeField] private List<PlayerAgent> playerAgents;
    [SerializeField] private Player player;

    private List<string> ranks = new List<string>();
    private string leaderboard = "";

    public bool playerFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        leaderboard = "";
        for (int i = 0; i < ranks.Count; i++){
            leaderboard += (i+1).ToString() + ". " + ranks[i] + "\n";
        }
        if (playerFinished){
            LeaderBoard.text = leaderboard;
        }
    }

    public void AddPlayer (Transform player){
        ranks.Add(player.name);
    }

    IEnumerator Countdown()
    {
        for (int i = 0; i < playerAgents.Count; i++){
            playerAgents[i].enabled = false;
        }
        player.enabled = false;

        countdown.text = "3";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "2";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "1";
        
        yield return new WaitForSeconds(1);
        
        countdown.text = "";

        for (int i = 0; i < playerAgents.Count; i++){
            playerAgents[i].enabled = true;
        }
        player.enabled = true;
    }
}
