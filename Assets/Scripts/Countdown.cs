using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(5);
    }
}
