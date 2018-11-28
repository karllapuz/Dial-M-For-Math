using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int easyQuestions;
    public int mediumMax;
    public int mediumMulDivMax;
    public int mediumQuestions;
    public int hardMax;
    public int hardMulDivMax;

    public Text scoreText;
    public Text formula;

    public GameObject answerIndicator;
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

    public Text answer1;
    public Text answer2;
    public Text answer3;
    public Text answer4;
    public Text answer5;
    public Text answer6;
    public Text answer7;
    public Text answer8;
    List<Text> answers;

    int winningNumber;

    int score;

    int operandChooser;
    int first;
    int second;
    int answer;
    char[] operands;
    char operand;
    int max;
    int mulDivMax;


	// Use this for initialization
	void Start () {

        easyQuestions = 10;
        mediumMax = 30;
        mediumMulDivMax = 7;
        mediumQuestions = 10;
        hardMax = 50;
        hardMulDivMax = 10;

        operandChooser = 2;
        operands = new char[] { '+', '-', 'x', '/' };
        max = 10;
        mulDivMax = 5;

        score = 0;
        scoreText.text = (" " + score);

        colors = new List<GameObject>();
        colors.Add(color1);
        colors.Add(color2);
        colors.Add(color3);
        colors.Add(color4);
        colors.Add(color5);
        colors.Add(color6);
        colors.Add(color7);
        colors.Add(color8);

        answers = new List<Text>();
        answers.Add(answer1);
        answers.Add(answer2);
        answers.Add(answer3);
        answers.Add(answer4);
        answers.Add(answer5);
        answers.Add(answer6);
        answers.Add(answer7);
        answers.Add(answer8);

        newRound();
    }
	
	// Update is called once per frame
	void Update () {
        if (score > mediumQuestions + easyQuestions)
        {
            max = hardMax;
            mulDivMax = hardMulDivMax;
        }
        else if (score > easyQuestions)
        {
            max = mediumMax;
            mulDivMax = mediumMulDivMax;
        }
        else if (score > 5) operandChooser = 4;
	}

    public bool isCorrect() {
        return dial.GetComponent<CircleCollider2D>().IsTouching(colors[winningNumber].GetComponent<BoxCollider2D>());
    }

    public void correct() {
        answerIndicator.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void wrong() {
        answerIndicator.GetComponent<SpriteRenderer>().color = Color.red;
    }


    public void newRound() {

        int operandIndex = Random.Range(0, operandChooser);
        operand = operands[operandIndex];
        int rangeMax = 0;
        switch (operand) {
            case '+':
                first = Random.Range(1, max);
                second = Random.Range(1, max);
                answer = first + second;
                rangeMax = max + max + 1;
                break;
            case '-':
                first = Random.Range(1, max);
                second = Random.Range(1, max);
                answer = first - second;
                rangeMax = max + 1;
                break;
            case 'x':
                first = Random.Range(1, mulDivMax);
                second = Random.Range(1, mulDivMax);
                answer = first * second;
                rangeMax = (mulDivMax * mulDivMax) + 1;
                break;
            case '/':
                second = Random.Range(2, mulDivMax);
                answer = Random.Range(3, mulDivMax * 3);
                first = answer * second;
                rangeMax = (mulDivMax * 3) + 1;
                break;
            default:
                Debug.Log("Character not found");
                break;
        }

        formula.text = (first + " " + operand + " " + second);

        HashSet<int> intAnswers = new HashSet<int>();
        intAnswers.Add(answer);

        for (int i = 0; i < colors.Count; i++)
        {
            colors[i].GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 4f), Random.Range(0f, 0.4f), Random.Range(0f, 0.4f));
            int otherIntAnswer = Random.Range(0, rangeMax);
            while (!intAnswers.Add(otherIntAnswer)) otherIntAnswer = Random.Range(0, rangeMax);
            answers[i].text = otherIntAnswer.ToString();
        }
        winningNumber = Random.Range(1, 8);
        answers[winningNumber].text = answer.ToString();

    }

    public void scoreUp() {
        score++;
        scoreText.text = (" " + score);
    }

    public void scoreDown()
    {
        if (score > 0) {
            score--;
            scoreText.text = (" " + score);
        }
    }

}
