using UnityEngine;

public class OpponantCar : MonoBehaviour
{

    public float maxspeed;
    public float accelaration = 1f;
     public float Turningspeed = 30f;
     public float Breakspeed = 12f;
     public float Currentspeed;



    public Vector3 destination;
    public bool destiationReached;
    private Rigidbody rb;
    public int Maxlaps;
    public int Currentlap;
    public float respawntimer = 0f;
    public const float respawnTimeThreshold = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        Maxlaps = FindAnyObjectByType<LapSystem>().MaxLaps;
    }

    private void FixedUpdate()
    {
        Drive();

        if (!destiationReached)
        {

            respawntimer += Time.deltaTime;

            if (respawntimer >= respawnTimeThreshold) { RespawnAtdestination(); }


        }
        else { respawntimer = 0f; }
    }
   
    public void Drive()
    {
        if (!destiationReached)
        {
            Vector3 DestinationDirection = destination - transform.position;
            DestinationDirection.y = 0;
            float destinationDistance = DestinationDirection.magnitude;

            if (destinationDistance >= Breakspeed)
            {
                Quaternion targetRotation = Quaternion.LookRotation(DestinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Turningspeed * Time.deltaTime);
                Currentspeed = Mathf.MoveTowards(Currentspeed, maxspeed, accelaration * Time.deltaTime);
                rb.linearVelocity = transform.forward * Currentspeed;

            }
            else
            { destiationReached = true;
                rb.linearVelocity = Vector3.zero;
            
            
            }
        }
    }
    private void RespawnAtdestination()
    {
        respawntimer = 0f;
        Currentspeed = 5f;
        transform.position = destination;
        destiationReached = false;
    }
    public void LocateDestination(Vector3 Destination)
    {
        this.destination = Destination;
        destiationReached = false;
    }


    public void ResetAcceleration()
    {
        Currentspeed = Random.Range(25f, 32f);
        accelaration = Random.Range(3.5f,5f);
    }

public void IncreaseLaps()
    {
        Currentlap++;
        Debug.Log(gameObject.name + "lap " + Currentlap);
    }

}