using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDial : MonoBehaviour {

    public TimeManager timeObj;
    public GameManager game;
    public int speed = 200;

    float lastRotation = 0;
    float rotationSpeed;
    float direction;
    bool rotatingLeft = true;

    public AudioSource audioManager;
    public AudioClip wrong;
    public AudioClip correct;

    // Use this for initialization
    void Start () {

        transform.Rotate(0, 0, direction);
    }
	
	// Update is called once per frame
	void Update () {
        rotationSpeed = speed * Time.deltaTime;
        direction = lastRotation + rotationSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && !game.gameIsOver) {
            if (game.isCorrect())
            {
                timeObj.time = timeObj.maxTime;
                game.correct();
                lastRotation = transform.rotation.z;

                rotatingLeft = !rotatingLeft;

                game.scoreUp();
                game.newRound();

                audioManager.PlayOneShot(correct);
            }
            else {
                timeObj.time--;
                game.scoreDown();
                game.wrong();
                audioManager.PlayOneShot(wrong);
            }
        }

        if (rotatingLeft)
        {
            direction = lastRotation - rotationSpeed;
        }
        else
        {
            direction = lastRotation + rotationSpeed;
        }

        if (!game.gameIsOver) transform.Rotate(0, 0, direction);
    }
}
