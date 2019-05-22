using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    Text highscore;

    void OnEnable() {
	highscore = GetComponent<Text>();
        highscore.text = "Wins: " + GameObject.Find("Main Camera").GetComponent<GameManager>().savedScore.ToString();
            //savedScore.ToString();
    }
}
