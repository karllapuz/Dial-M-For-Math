using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int easyQuestions = 10;
    public int mediumMin = 10;
    public int mediumMax = 30;
    public int mediumMulDivMax = 7;
    public int mediumQuestions = 10;
    public int hardMin = 30;
    public int hardMax = 50;
    public int hardMulDivMax = 12;

    public Text scoreText;
    public Text formula;
    public GameObject gameOverText;
    public GameObject starterText;
    public bool gameIsOver;

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

    public AudioSource audioManager;
    public AudioClip gameOverClip;

    int winningNumber;

    int score;

    int operandChooser;
    int first;
    int second;
    int answer;
    char[] operands = new char[] { '+', '-', 'x', '/' };
    char operand;
    int max = 10;
    int min = 1;
    int mulDivMax = 5;


	// Use this for initialization
	void Start () {

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

        Starter();
    }

    void Starter() {
        gameOverText.SetActive(false);
        starterText.SetActive(true);
        gameIsOver = true;
    }

    void RestartGame() {

        max = 10;
        min = 1;
        mulDivMax = 5;
        operandChooser = 2;
        score = 0;
        scoreText.text = (" " + score);
        answerIndicator.GetComponent<SpriteRenderer>().color = Color.white;

        starterText.SetActive(false);
        gameOverText.SetActive(false);
        gameIsOver = false;

        newRound();

    }
	
	// Update is called once per frame
	void Update () {
        if (score > mediumQuestions + easyQuestions)
        {
            max = hardMax;
            min = hardMin;
            mulDivMax = hardMulDivMax;
        }
        else if (score > easyQuestions)
        {
            max = mediumMax;
            min = mediumMin;
            mulDivMax = mediumMulDivMax;
        }
        else if (score > 5) operandChooser = 4;

        if (gameIsOver && Input.GetKeyDown(KeyCode.Space)) RestartGame();
    }

    public bool isCorrect() {
        return dial.GetComponent<CircleCollider2D>().IsTouching(colors[winningNumber].GetComponent<BoxCollider2D>());
    }

    public void correct() {
        answerIndicator.GetComponent<SpriteRenderer>().color = new Color32(33, 154, 54, 255);
    }

    public void wrong() {
        answerIndicator.GetComponent<SpriteRenderer>().color = new Color32(227, 27, 38, 255);
    }


    public void newRound() {

        int operandIndex = Random.Range(0, operandChooser);
        operand = operands[operandIndex];
        int rangeMax = 0;
        int rangeMin = 0;
        switch (operand) {
            case '+':
                first = Random.Range(min, max);
                second = Random.Range(min, max);
                answer = first + second;
                rangeMax = max + max + 1;
                break;
            case '-':
                first = Random.Range(min, max);
                second = Random.Range(min, max);
                answer = first - second;
                rangeMax = max + 1;
                if (answer < 0) rangeMin = answer - 5;
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
            colors[i].GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.8f, 1f), Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f));
            int otherIntAnswer = Random.Range(rangeMin, rangeMax);
            while (!intAnswers.Add(otherIntAnswer)) otherIntAnswer = Random.Range(rangeMin, rangeMax);
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

    public void gameOver() {
        gameOverText.SetActive(true);
        gameIsOver = true;

        audioManager.PlayOneShot(gameOverClip);
    }

}
