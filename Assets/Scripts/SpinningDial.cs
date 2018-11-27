using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDial : MonoBehaviour {

    public GameManager game;
    public int speed = 200;

    float lastRotation = 0;
    float rotationSpeed;
    float direction;
    bool rotatingLeft = true;

    // Use this for initialization
    void Start () {
    
        transform.Rotate(0, 0, direction);
    }
	
	// Update is called once per frame
	void Update () {
        rotationSpeed = speed * Time.deltaTime;
        direction = lastRotation + rotationSpeed;
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (game.isCorrect())
            {
                game.correct();
                lastRotation = transform.rotation.z;

                rotatingLeft = !rotatingLeft;

                game.scoreUp();
                game.newRound();
            }
            else {
                game.scoreDown();
                game.wrong();
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

        transform.Rotate(0, 0, direction);
    }
}
