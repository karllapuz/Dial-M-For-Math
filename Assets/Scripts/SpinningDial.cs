using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningDial : MonoBehaviour {

    public GameManager game;

    int speed = 100;
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
                lastRotation = transform.rotation.z;
                if (rotatingLeft)
                {
                    direction = lastRotation - rotationSpeed;
                }
                else
                {
                    direction = lastRotation + rotationSpeed;
                }
                rotatingLeft = !rotatingLeft;
                game.scoreUp();
                game.newColors();
            }
            else {
                Debug.Log("Wrong answer");
            }
        }
        transform.Rotate(0, 0, direction);
    }
}
