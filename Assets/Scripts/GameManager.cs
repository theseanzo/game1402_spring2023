using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Color[] teams;
    internal OutPost[] outposts;
    private static GameManager _instance;


    public static GameManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                _instance.OnCreateInstance();
            }
            return _instance;
        }
    }
    private void OnCreateInstance()
    {
        outposts = GetComponentsInChildren<OutPost>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
