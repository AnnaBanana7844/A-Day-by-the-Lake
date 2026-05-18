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
        DON = true;
        //currentMusic = menuMusic;
        //currentMusic.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cycleTimer = 0;
        DayActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialActive == false)//time only passes if the tutorial is done
        {
            cycleTimer += Time.deltaTime;
            DayNightCycle();
        }
        //Debug.Log(cycleTimer);
    }

    void DayNightCycle()
    {
        //Debug.Log("Cycling");
        //run a timer that starts on day
        //every time the timer reaches the correct integer in seconds(nightTimer if its night, opposite if day), switch the active day
        //if((int)cycleTimer == dayTimer || cycleTimer == nightTimer)
        //{
        //    Debug.Log("Time Ticking");
        //}
        if ((int)cycleTimer == dayTimer && DON == true)//if its currently day the daytime has ended
        {
            //Debug.Log("Night");
            //when day is over
            //we want night music to play
            //we also want to change the skybox
            NightActive();
        }
        else if ((int)cycleTimer == nightTimer && DON == false)//if its currently night the nighttime has ended
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
        //Debug.Log("cycle changing");
        cycleTimer = 0;
        if (currentMusic != null) { currentMusic.Stop(); }
        currentMusic = dayMusic;
        currentMusic.Play();
        DON = !DON;
    }

    public void NightActive()
    {
        //Debug.Log("cycle changing");
        cycleTimer = 0;
        if(currentMusic!=null){ currentMusic.Stop(); }
        currentMusic = nightMusic;
        currentMusic.Play();
        DON = !DON;
    }
    public void MenuActive()
    {
        menuMusic.Play();
    }
}
