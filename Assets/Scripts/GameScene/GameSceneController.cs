using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSceneController : MonoBehaviour
{
    private static readonly int MAX_DEGREE = 360;
    private static readonly float RADIUS = 0.35f;

    private GameObject[] moons;
    private GameObject satellite;
    private GameData data;
    private LevelData levelData;

    public float currentSpeed;
    private int currentLevel;
    private int angle;
    private float radius;

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
        currentLevel = PlayerPrefHelper.GetCurrentStage();
        levelData = data.levelData[currentLevel];
        radius = Camera.main.ViewportToWorldPoint(new Vector3(RADIUS, 0, 0)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        currentSpeed = levelData.speed;
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
        int angle = levelData.angle;
        int moonNumbers = MAX_DEGREE / angle;
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
        if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            satellite.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        }

    }

    private void FixedUpdate()
    {
        RotateMoons();
    }

    private void RotateMoons()
    {
        for (int i = 0; i < moons.Length; i++)
        {
            moons[i].transform.RotateAround(planet.transform.position, new Vector3(0f, 0f, 1f), levelData.speed * Time.deltaTime);
        }
    }

}
