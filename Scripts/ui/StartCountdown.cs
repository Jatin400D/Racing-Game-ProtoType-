using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public Text []CountdownTexts;
    public float CoundownTime = 5f;
    private carController[] Playercars;
    private OpponantCar[] opponantCars;
    private OpcarWaypoint[] waypoints;



    private void Awake()
    {
        Playercars = FindObjectsOfType <carController>();
         opponantCars= FindObjectsOfType<OpponantCar>();
        waypoints = FindObjectsOfType<OpcarWaypoint>();
        StartCoroutine(StartCoundown());
    }
    void DisableScript()
    {
        foreach (OpponantCar opponantCar in opponantCars)
        {
            opponantCar.enabled = false;



        }
        foreach(OpcarWaypoint waypoint in waypoints)
        {
            waypoint.enabled = false ;
        }  
        foreach(carController car in Playercars)
        {
            car.enabled = false;;
        }


    }
    IEnumerator StartCoundown()
    {
        DisableScript();
        float currentTime = CoundownTime;
        while (currentTime > 0) {
            UpdateCountdownText(currentTime);
            yield return new WaitForSeconds(1f);
            currentTime--;

                }
        EnableScript();
        UpdateCountdownText("GO");
        yield return new WaitForSeconds(1f);
        SetTextActive(false);
        

    }
    void EnableScript()
    {
        foreach (OpponantCar opponantCar in opponantCars)
        {
            opponantCar.enabled = true;



        }
        foreach (OpcarWaypoint waypoint in waypoints)
        {
            waypoint.enabled = true;
        }
        foreach (carController car in Playercars)
        {
            car.enabled = true; 
        }


    }
    void UpdateCountdownText(string text)
    {
        foreach(Text coundowntext in CountdownTexts)
        {
            coundowntext.text = text;
        }
    }
    void UpdateCountdownText(float time)
    {
        foreach(Text coundownText in CountdownTexts)
        {
            coundownText.text = time.ToString("0");
        }
    }
    void SetTextActive(bool active)
    {
        foreach(Text countdownText in CountdownTexts)
        {
            countdownText.gameObject.SetActive(active);
        }
    }
}
