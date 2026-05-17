using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Game;//game manager singleton
    [SerializeField] public Player player;
    [SerializeField] public Bear bear;//the bear is subjective.

    [Header("-----Music-----")]
    [SerializeField] AudioSource menuMusic;//music that plays on specifically the main menu
    [SerializeField] AudioSource dayMusic;//music that loops during the day
    [SerializeField] AudioSource nightMusic;//music that loops at night

    [Header("-----Timers-----")]
    [SerializeField] int dayTimer;
    [SerializeField] int nightTimer;
    float cycleTimer;
    bool tutorialActive = false;
    //bool DON;//whether it's day or night. true if day, false if night.

    private void Awake()
    {
        //maybe tutorial can be a button? and there can be a complete tutorial button to trigger the start of the day night cycle
        Game = this;
        //DON = true;
        menuMusic.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialActive == false)//time only passes if the tutorial is done
        {
            cycleTimer += Time.deltaTime;
            DayNightCycle();
        }
    }

    void DayNightCycle()
    {

        //run a timer that starts on day
        //every time the timer reaches the correct integer in seconds(nightTimer if its night, opposite if day), switch the active day
        if (cycleTimer == dayTimer)//if the daytime has ended
        {
            //when day is over
            //we want night music to play
            //we also want to change the skybox
        }
        if (cycleTimer == nightTimer)//if the nighttime has ended
        {
            //when night is over
            //we want day music to play
            //we also want to change the skybox back
        }
    }
    public void DayActive()
    {
        dayMusic.Play();
    }

    public void NightActive()
    {
        nightMusic.Play();
    }
    public void MenuActive()
    {
        menuMusic.Play();
    }
}
