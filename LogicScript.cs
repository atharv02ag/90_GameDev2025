using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int Score = 0;
    public Text scoreText;
    // Start is called before the first frame update
    public void changeScore(int amount)
    {
        Score += amount;
        scoreText.text = Score.ToString();
    }
}
