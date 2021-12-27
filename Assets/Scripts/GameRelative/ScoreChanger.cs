using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Viking_Controller.bestScore = PlayerPrefs.GetInt("BestScore");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Score").GetComponent<Text>().text = "Score : " + Viking_Controller.score;
        if (Viking_Controller.score > Viking_Controller.bestScore)
            Viking_Controller.bestScore = Viking_Controller.score;
        GameObject.Find("Best").GetComponent<Text>().text = "Best Score : " + Viking_Controller.bestScore;
    }
}
