using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public GameObject outer;
    public GameObject inner;
    public GameManager game;
    float difference;

    public float maxTime = 7.0f;

    public float time;
    Color baseColor;
    float differenceColorRED;
    float differenceColorGREEN;
    float differenceColorBLUE;

    // Use this for initialization
    void Start () {
        time = maxTime;
        difference = outer.transform.localScale.x - inner.transform.localScale.x;
        baseColor = outer.GetComponent<SpriteRenderer>().color;
        differenceColorRED = 227 - outer.GetComponent<SpriteRenderer>().color.r;
        differenceColorGREEN = 27 - outer.GetComponent<SpriteRenderer>().color.g;
        differenceColorBLUE = 38 - outer.GetComponent<SpriteRenderer>().color.b;
    }
	
	// Update is called once per frame
	void Update () {
        if (!game.gameIsOver) {
            time -= Time.deltaTime;
            float baseScale = inner.transform.localScale.x;
            outer.transform.localScale = new Vector3(baseScale + (time / maxTime) * difference, baseScale + (time / maxTime) * difference, baseScale + (time / maxTime) * difference);
            outer.GetComponent<SpriteRenderer>().color = new Color(227 - (time / maxTime) * differenceColorRED, 27 - (time / maxTime) * differenceColorGREEN, 38 - (time / maxTime) * differenceColorBLUE);
            Debug.Log(new Color(227 - (time / maxTime) * differenceColorRED, 27 - (time / maxTime) * differenceColorGREEN, 38 - (time / maxTime) * differenceColorBLUE));
            if (time < 0)
            {
                time = maxTime;
                game.gameOver();
                outer.GetComponent<SpriteRenderer>().color = baseColor;
            }
        }
	}
}
