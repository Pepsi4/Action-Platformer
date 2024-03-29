﻿using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{

    const float BalloonSpawnY = 1.2f;
    const float SpawnRangeX = 3f;
    const float SpawnTime = 0.5f;



    //borders
    //are using for the max and the min X of balloon spawn
    GameObject leftBorder;
    GameObject rightBorder;

    GameObject mainHero;

    private void Start()
    {
        //Main hero initialization 
        mainHero = GameObject.Find("MainHero");

        //initialization the borders
        leftBorder = GameObject.Find("Borders/Border (1)");
        rightBorder = GameObject.Find("Borders/Border (2)");

        //Starting the game process
        StartCoroutine(SpawnTheBalloon());
    }

    IEnumerator SpawnTheBalloon()
    {
        if (GameStatus.IsActive) //spawning the balloon if that`s true
        {
            //load balloon for future use
            GameObject balloon = (GameObject)Resources.Load("GameObjects/balloon");
            //change the balloon's position
            balloon.transform.position = new Vector2(GetRandomPositionX(), BalloonSpawnY);
            //create the prefab
            Instantiate(balloon);
        }
        //waiting some time
        yield return new WaitForSeconds(SpawnTime);
        //recursion
        StartCoroutine(SpawnTheBalloon());
    }

    float GetRandomPositionX()
    {
        float leftBorderX = leftBorder.GetComponent<Transform>().position.x;
        float rightBorderX = rightBorder.GetComponent<Transform>().position.x;

        float mainHeroPositionX = mainHero.GetComponent<Transform>().position.x;

        while (true)
        {
            float balloonPositionX = Random.Range(mainHeroPositionX - SpawnRangeX, mainHeroPositionX + SpawnRangeX);
            if (balloonPositionX >= leftBorderX && balloonPositionX <= rightBorderX)
            {
                return balloonPositionX;
            }
            else if (balloonPositionX < leftBorderX)
            {
                balloonPositionX += 0.2f;
            }
            else if (balloonPositionX > rightBorderX)
            {
                balloonPositionX -= 0.2f;
            }
        }

    }
}
