﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollision : MonoBehaviour
{
    private GameSceneController gameController;
    private TextProgress textProgress;
    private Transform pTransform;
    private PersonalSceneManager sceneManager;
    private bool spin;

    // Use this for initialization
    void Start()
    {
        spin = false;
        gameController = FindObjectOfType<GameSceneController>();
        textProgress = FindObjectOfType<TextProgress>();
        sceneManager = FindObjectOfType<PersonalSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spin && !gameController.IsStop())
        {
            transform.RotateAround(pTransform.position, new Vector3(0f, 0f, 1f), gameController.currentSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Planet"))
        {
			print ("Attach to planet");
            AttachToPlanet(collision);
            textProgress.UpdateTextScore();
			if (!gameController.IsStop()) {
				gameController.CreateOriginSattelite ();
			}
        } else
        {
			print ("Attach fail");
			MusicPlayer.getInstance ().handleExplodeSound ();
			StartCoroutine (ShowFailPopup (0.1f, collision));
        }
    }

    private void AttachToPlanet(Collider2D collision)
    {
        pTransform = collision.transform;
        float radius = (collision as CircleCollider2D).radius;
        float angle = 270f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        float newX = pTransform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float newY = pTransform.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        transform.position = new Vector3(newX, newY, transform.position.z);
        spin = true;
    }

	IEnumerator ShowFailPopup(float time, Collider2D collision)
	{
		yield return new WaitForSeconds (time);
		gameController.Stop ();
		MusicPlayer.getInstance ().handleFailSound ();
        textProgress.ShowFailPopup();
		//StartCoroutine (BackToStartScene (5));
	}

	IEnumerator BackToStartScene(float time)
	{
		yield return new WaitForSeconds (time);
		sceneManager.LoadScene ("StartScene");
	}

}
