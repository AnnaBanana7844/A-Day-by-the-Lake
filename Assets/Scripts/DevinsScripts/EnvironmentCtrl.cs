using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnvironmentCtrl : MonoBehaviour
{
    public static EnvironmentCtrl Game;//game manager singleton
    [SerializeField] public Player player;
    [SerializeField] public Bear bear;

    [Header("-----Music-----")]
    [SerializeField] AudioSource menuMusic;//music that plays on specifically the main menu
    [SerializeField] AudioSource dayMusic;//music that loops during the day
    [SerializeField] AudioSource nightMusic;//music that loops at night
    AudioSource currentMusic;

    [Header("-----Timers-----")]
    [SerializeField] int dayTimer;
    [SerializeField] int nightTimer;
    float cycleTimer;
    bool tutorialActive = false;
    bool DON;//whether it's day or night. true if day, false if night.

    private void Awake()
    {
        //maybe tutorial can be a button? and there can be a complete tutorial button to trigger the start of the day night cycle
        Game = this;
        //DON = true;
        currentMusic = menuMusic;
        currentMusic.Play();
        DON = true;
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
            Debug.Log("updating");
            cycleTimer += Time.deltaTime;
            DayNightCycle();
        }
        //Debug.Log(cycleTimer);
    }

    void DayNightCycle()
    {
        
        //run a timer that starts on day
        //every time the timer reaches the correct integer in seconds(nightTimer if its night, opposite if day), switch the active day
        if (cycleTimer == dayTimer && DON == true)//if it's currently day and the daytime has ended
        {
            //Debug.Log("Night");
            //when day is over
            //we want night music to play
            //we also want to change the skybox
            NightActive();
        }
        if (cycleTimer == nightTimer && DON == false)//if it's currently night and if the nighttime has ended
        {
            //Debug.Log("Day");
            //when night is over
            //we want day music to play
            //we also want to change the skybox back
            DayActive();
        }
    }
    public void DayActive()
    {
        Debug.Log("Day Active");
        cycleTimer = 0;
        currentMusic = dayMusic;
        DON = true;
    }

    public void NightActive()
    {
        Debug.Log("Night Active");
        cycleTimer = 0;
        currentMusic = nightMusic;
        DON = false;
    }
    public void MenuActive()
    {
        menuMusic.Play();
    }
}
