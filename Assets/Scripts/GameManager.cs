using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Color[] teams;
    internal Outpost[] outposts;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                _instance.OnCreateInstance();

            }
            return _instance;
        }
    }
    private void OnCreateInstance()
    {
        //this is where we do some initialization for the GameManager
        outposts = GetComponentsInChildren<Outpost>(); //since everything is a child of GameManager, this will give us access to all of the children
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
