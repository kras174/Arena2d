using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    [SerializeField]
    Text livesFiled;

    public static int lives;

    void Update()
    {
        livesFiled.text = lives.ToString();
    }
}
