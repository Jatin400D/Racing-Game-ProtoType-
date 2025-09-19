using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class carController : MonoBehaviour
{
   public enum Cartype
    {
        FrontwheeldRIVE,
        RearWheelDrive,
        FourWheelDrive
    }

    public Cartype cartype = Cartype.FourWheelDrive;
    public enum ControllMode
    {
        keyboard,
        button
    };

    public ControllMode controllMode;

    [SerializeField] private GameObject FrontWheeleft;
    [SerializeField] private GameObject FrontWheeright;
    [SerializeField] private GameObject RearWheelLeft;
    [SerializeField] private GameObject RearWheelRight;

    //collider
    [SerializeField] private WheelCollider frontwheelleftcollider;
    [SerializeField]private WheelCollider frontwheelrightcollider;
    [SerializeField]private WheelCollider backwheelleftcollider;
    [SerializeField]private WheelCollider BackwheelRightcollider;


    //movement
    private float currentspeed;
    [SerializeField] float maximumtorq;
    [SerializeField] float minimumsteerAngle = 20f;
    [SerializeField] float maximumspeed;
    [SerializeField] float brakepower;
    [SerializeField] Transform com;
    [SerializeField] float carspeed;
    [SerializeField] float carspeedConverted;
    [SerializeField] float motorTprque;
    [SerializeField] float tireAngle;
    [SerializeField] float vertical = 0f;
    [SerializeField] float horizontal = 0f;
    bool Handbrake = false;
    public int Maxlaps;
    public int Currentlap;
    Rigidbody carrigidbody;
    public AudioSource EngineSound;
    public AudioClip Engineclip;







    private void Start()
    {
        carrigidbody = GetComponent<Rigidbody>();
        if (carrigidbody != null)
        {
            carrigidbody.centerOfMass = com.localPosition;
        }



        EngineSound.loop = true;
        EngineSound.volume = 0.5f;
        EngineSound.pitch = 1f;
        EngineSound.Play();
        EngineSound.Pause();
        Maxlaps = FindAnyObjectByType<LapSystem>().MaxLaps;
       
    }
    private void FixedUpdate()
    {
        GetInput();
        CalculatecarMovement();
        calculatesteering();
        applytransformtowheel();    
    }


    private void CalculatecarMovement()
    {
        carspeed = carrigidbody.linearVelocity.magnitude;
        carspeedConverted = Mathf.Round(carspeed * 3.6f);



        if (Input.GetKey(KeyCode.Space))
            Handbrake = true;
        else
            Handbrake = false;

        if (Handbrake)
        {

            motorTprque = 0;
            ApplyBrake();

        }
        else
        {
            RealeseBrake();
            if (carspeedConverted < maximumspeed)
                motorTprque = maximumtorq * vertical;
            else motorTprque = 0;



            if(carspeedConverted>0|| Handbrake)
            {
                EngineSound.UnPause();
                float gearRatio = currentspeed/maximumspeed;
                int numberofGears = 6;
                int currentgear = Mathf.Clamp(Mathf.FloorToInt(gearRatio*numberofGears) +1,1,numberofGears);

                float pitchMultiplier = 0.5f + 0.5f * (carspeedConverted/ maximumspeed);
                float Volumemultiplier = 0.2f +0.8f * (carspeedConverted/ maximumspeed);

                EngineSound.pitch = Mathf.Lerp(0.5f,1.0f,pitchMultiplier)*currentgear;
                EngineSound.volume = Volumemultiplier;  
            }
            else
            {
                EngineSound.UnPause();
                EngineSound.pitch = 0.5f;
                EngineSound.volume = 0.2f;
            }

        }
        ApplyMotorTorq();
    }

    private void ApplyMotorTorq()
    {
        if (cartype == Cartype.FrontwheeldRIVE)
        {
            frontwheelrightcollider.motorTorque = motorTprque;
            frontwheelleftcollider.motorTorque = motorTprque;
        }

        else if (cartype == Cartype.RearWheelDrive) { 
        
            backwheelleftcollider.motorTorque = motorTprque;
            BackwheelRightcollider.motorTorque = motorTprque;
        }

        else if(cartype == Cartype.FourWheelDrive) {
            frontwheelrightcollider.motorTorque = motorTprque;
            frontwheelleftcollider.motorTorque = motorTprque;
            backwheelleftcollider.motorTorque = motorTprque;
            BackwheelRightcollider.motorTorque = motorTprque;
        }


    }
    private void GetInput()
    {
        if (controllMode == ControllMode.keyboard)
        {

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");



        }


    }

 

    private void ApplyBrake()
    {
        frontwheelleftcollider.brakeTorque = brakepower;
        frontwheelrightcollider.brakeTorque = brakepower;
        backwheelleftcollider.brakeTorque= brakepower;
        BackwheelRightcollider.brakeTorque =brakepower;
    }

    private void RealeseBrake()
    {
        frontwheelleftcollider.brakeTorque =0;
        frontwheelrightcollider.brakeTorque = 0;
        backwheelleftcollider.brakeTorque = 0;
        BackwheelRightcollider.brakeTorque = 0;
    }

    private void calculatesteering()
    {
        tireAngle = minimumsteerAngle * horizontal;
        frontwheelleftcollider.steerAngle = tireAngle;
        frontwheelrightcollider.steerAngle= tireAngle;
    }
     
    public void applytransformtowheel()
    {
        Vector3 position;
        Quaternion rotation;

        frontwheelrightcollider .GetWorldPose ( out position, out rotation );
        FrontWheeright.transform.position = position;   
        FrontWheeright .transform.rotation = rotation;

        frontwheelleftcollider.GetWorldPose(out position, out rotation);
        FrontWheeleft.transform.position = position;
        FrontWheeleft.transform.rotation = rotation;

        backwheelleftcollider.GetWorldPose (out position, out rotation);
        RearWheelLeft.transform.position = position;
        RearWheelLeft .transform.rotation = rotation;

        BackwheelRightcollider.GetWorldPose(out position, out rotation);
        RearWheelRight.transform.position = position;
        RearWheelRight .transform.rotation = rotation;

    }
    public void IncreaseLaps()
    {
        Currentlap++;
        Debug.Log( gameObject.name + "lap " + Currentlap);
    }
}
