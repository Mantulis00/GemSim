using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void CreateLine(GameObject obModel)
    {
        GameObject go;
        go = Instantiate(obModel) as GameObject;
        go.transform.SetParent(this.transform);
    }

}
