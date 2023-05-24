using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class OutPost : MonoBehaviour
{
    [SerializeField]
    private float flagTop;
    [SerializeField] 
    private float flagBottom;

    private SkinnedMeshRenderer flag;

    internal int team = -1;


    //when someone enter thr area we need it to start collecting points
    private float timer;
    [SerializeField]
    private float scoreinterval = 5.0f;
    [SerializeField]
    float valueIncrease = 0.005f;
    internal float currentValue = 0; 

    // Start is called before the first frame update
    void Start()
    {
        flag = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if (team != -1)
       {
            flag.transform.parent.localPosition = new Vector3(0, Mathf.Lerp(flagBottom,flagTop,currentValue));
       } 
    }
    private void OnTriggerStay(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null)
        {
            if (unit.Team == team)
            {
                //this is for when our current team is staying there
                currentValue += valueIncrease;
                if (currentValue >=1)
                {
                    currentValue = 1;
                }
            }
            else
            {
                //this is for when a new team enters. They immediately decrease the currentValue
                currentValue -= valueIncrease;
                if(currentValue <= 0)
                {
                    team = unit.Team;
                    currentValue = 0;
                }
            }
        }
    }
}
