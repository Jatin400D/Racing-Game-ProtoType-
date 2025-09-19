using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject PlayerCars;
    private GameObject[] AllCars;
    private int currentIndex = 0;



    private void Start()
    {
         AllCars = new GameObject[PlayerCars.transform.childCount];


        for(int i = 0; i < PlayerCars.transform.childCount; i++)
        {
            AllCars[i] =  PlayerCars.transform.GetChild(i).gameObject;

            AllCars[i].SetActive(false);
        }
        if (PlayerPrefs.HasKey("SelectedCarIndex"))
        {
            currentIndex = PlayerPrefs.GetInt("SelectedCarIndex");

        }
       ShowCurrentCar();
    }
    void ShowCurrentCar() {


        foreach (GameObject car in AllCars) { 
        car.SetActive(false);
        }
        AllCars[currentIndex].SetActive(true);
    
    
    }
    public void NextCar()
    {
        currentIndex = (currentIndex + 1) % AllCars.Length;
        ShowCurrentCar();
    }
    public void PreviousCar()
    {
        currentIndex = (currentIndex -  1) % AllCars.Length;
        ShowCurrentCar();
    }
    public void CarSelected(string SceneName)
    {
        PlayerPrefs.SetInt("SelectedCarIndes", currentIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("complete_track_demo");
    }
}
