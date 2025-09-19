using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class SpeedBraker : MonoBehaviour
{
    public float durationOfreduction = 3f;



    private void OnTriggerEnter(Collider other)
    {
        OpponantCar opponantCar = other.GetComponent<OpponantCar>();
        if (opponantCar != null)
        {
            opponantCar.accelaration = Random.Range(0.5f, 1f);
            opponantCar.Currentspeed = Random.Range(10f, 20f);
            StartCoroutine(ResetAcceleration(opponantCar));             
        }

    }


    IEnumerator ResetAcceleration(OpponantCar opponantCar)
    {
        yield return new WaitForSeconds(durationOfreduction);
        opponantCar.ResetAcceleration();

    }
}
