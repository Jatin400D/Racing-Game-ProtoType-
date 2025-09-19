using UnityEngine;
using UnityEngine.UI;

public class LapSystem : MonoBehaviour
{

    public int MaxLaps = 1;


    public Text Resulttext;

    private void OnTriggerEnter(Collider other)
    {
        OpponantCar opponantCar = other.GetComponent<OpponantCar>();
        carController playercar = other.GetComponent<carController>();


        if(playercar != null )
        {
            playercar.IncreaseLaps();
            CheckRaceComplete( playercar);
          

        }
        if (opponantCar != null) {

            opponantCar.IncreaseLaps();
            CheckRaceComplete( opponantCar);



}
    }

    private void CheckRaceComplete(OpponantCar opponantCar)
    {
        if (opponantCar.Currentlap == MaxLaps) { Endmission(false); }

    }
    private void CheckRaceComplete(carController playercar)
    {
        if (playercar.Currentlap == MaxLaps) { Endmission(true); }

    }
    private void Endmission(bool Success)
    {
        if (Success)
        {

            Debug.Log("Player Wins");
           

        }

        else
        {
            Debug.Log("player Lose");
           
        }



    }
}
