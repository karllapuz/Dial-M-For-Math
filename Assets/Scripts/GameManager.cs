using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text scoreText;
    public Text redText;
    public Text greenText;
    public Text blueText;

    public GameObject dial;
    public GameObject color1;
    public GameObject color2;
    public GameObject color3;
    public GameObject color4;
    public GameObject color5;
    public GameObject color6;
    public GameObject color7;
    public GameObject color8;
    List<GameObject> colors;

    int winningNumber;

    int score;
    int redValue;
    int greenValue;
    int blueValue;

	// Use this for initialization
	void Start () {

        score = 0;
        scoreText.text = ("Score: " + score);

        colors = new List<GameObject>();
        colors.Add(color1);
        colors.Add(color2);
        colors.Add(color3);
        colors.Add(color4);
        colors.Add(color5);
        colors.Add(color6);
        colors.Add(color7);
        colors.Add(color8);

        newColors();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public bool isCorrect() {
        return dial.GetComponent<CircleCollider2D>().IsTouching(colors[winningNumber].GetComponent<BoxCollider2D>());
    }

    public void newColors() {
        redValue = Random.Range(0, 255);
        greenValue = Random.Range(0, 255);
        blueValue = Random.Range(0, 255);

        redText.text = ("R: " + redValue);
        greenText.text = ("G: " + greenValue);
        blueText.text = ("B: " + blueValue);

        for (int i = 0; i < colors.Count; i++)
        {
            colors[i].GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        winningNumber = Random.Range(1, 8);
        colors[winningNumber].GetComponent<SpriteRenderer>().color = new Color32((byte)redValue, (byte)greenValue, (byte)blueValue, 255);
    }

    public void scoreUp() {
        score++;
        scoreText.text = ("Score: " + score);
    }

}
