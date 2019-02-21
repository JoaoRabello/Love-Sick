using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private float timer = 0f;
    public Text timerTxt;

    [SerializeField] private Transform arenaTransform;
    [SerializeField] private GameObject player;
    private bool hasStarted = false;
	
	void Start () {

        
        StartCoroutine(InitialCounter());
	}

	void Update () {

        if (hasStarted)
        {
            timer += Time.deltaTime;
            timerTxt.text = timer.ToString("F0");
        }
	}

    IEnumerator InitialCounter()
    {
        timerTxt.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        timerTxt.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        timerTxt.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        timerTxt.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        player.GetComponent<Player>().canMove = true;
        hasStarted = true;
        timer = 0f;
        StartCoroutine(ExplorationCounter());
    }

    IEnumerator ExplorationCounter()
    {
        yield return new WaitForSecondsRealtime(10f);
        player.transform.position = arenaTransform.position;
        player.GetComponent<Player>().canAttack = true;
    }
}
