using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class Resurrection : MonoBehaviour
{
    public GameObject enemyPref;
    public GameObject playerPref;
    public GameObject[] resurectionPlace;

    public Button restart;

    private void Start()
    {
        for(int i = 0; i < resurectionPlace.Length; i++)
        {
            Instantiate(enemyPref, resurectionPlace[i].transform.position, Quaternion.identity);
        }

    }

    public void Resurrect()
    {
        Instantiate(enemyPref, resurectionPlace[Random.Range(0, 2)].transform.position, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    

    public void WaitRes()
    {
        Invoke("Resurrect", 7f);
    }


}
