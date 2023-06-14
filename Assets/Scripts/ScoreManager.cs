using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public int[] scores;
    public static ScoreManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScoreManager>();
                _instance.scores = new int[GameManager.Instance.teams.Length];
            }
            return _instance;
        }
    }
    // Start is called before the first frame update

}
