using UnityEngine;

public class OpcarWaypoint : MonoBehaviour
{
    public OpponantCar opponantCar;
    public waypoint Cureentwaypoint;



    private void Start()
    {
        opponantCar.LocateDestination(Cureentwaypoint.GetPosition());
    }

    private void Update()
    {
        if (opponantCar.destiationReached) {

            Cureentwaypoint = Cureentwaypoint.NextWaypoint;
            opponantCar.LocateDestination(Cureentwaypoint.GetPosition());
        
        
        
        
        }
    }











}
