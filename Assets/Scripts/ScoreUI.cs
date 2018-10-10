using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    [SerializeField]
    Text scoreFiled;

    public static int score;
	
	void Update () {
        scoreFiled.text = score.ToString();
    }
}
