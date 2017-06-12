using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.GameScene;

public class GameSceneController : MonoBehaviour
{
    private static readonly int MAX_DEGREE = 360;
    private static readonly float RADIUS = 0.35f;

    private GameObject[] moons;
    private GameObject satellite;
    private GameData data;
    private LevelData levelData;

    private SpeedHelper speedHelper;

    public float currentSpeed;
    private int passingStage;
    private int angle;
    private float radius;
    private bool stop = false;

    [SerializeField]
    private GameObject originSatellite;

    [SerializeField]
    private GameObject planet;

    [SerializeField]
    private GameObject moon;

    // Use this for initialization
    void Start()
    {
        InitVariables();
        CreateOriginSattelite();
        CreatePlanet();
        CreateMoons();
    }

    private void InitVariables()
    {
        data = GameData.LoadFromJSONResource();
        passingStage = PlayerPrefHelper.GetPassingStage() - 1;
        levelData = data.levelData[passingStage];
        radius = Camera.main.ViewportToWorldPoint(new Vector3(RADIUS, 0, 0)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        speedHelper = new SpeedHelper(levelData.v1, levelData.v2);
    }

    private void CreatePlanet()
    {
        planet.GetComponent<CircleCollider2D>().radius = radius;
    }

    public void CreateOriginSattelite()
    {
        satellite = Instantiate(originSatellite);
    }

    private void CreateMoons()
    {
        int moonNumbers = levelData.moon;
        angle = MAX_DEGREE / moonNumbers;
        Debug.Log("moon" + moonNumbers);
        moons = new GameObject[moonNumbers];
        Vector3 center = planet.transform.position;
        for (int i = 0; i < moonNumbers; i++)
        {
            Vector3 pos = RandomCircle(center, radius, angle * (i + 1));
            moons[i] = Instantiate(moon, pos, Quaternion.identity) as GameObject;
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius, float angle)
    {
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            return;
        }
        currentSpeed = speedHelper.GetUpdateSpeed(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            satellite.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            MusicPlayer.getInstance().handleClickSound();
        }
        for (int i = 0; i < moons.Length; i++)
        {
            moons[i].transform.RotateAround(planet.transform.position, new Vector3(0f, 0f, 1f), currentSpeed * Time.deltaTime);
        }
    }


    private void FixedUpdate()
    {
        if (stop)
        {
            return;
        }
        
    }

    public void Stop()
    {
        stop = true;
        satellite.SetActive(false);
    }

    public bool IsStop()
    {
        return stop;
    }
}
