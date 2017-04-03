﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSkill : Skill {

    public GameObject[] spears;
    // Use this for initialization
    GameObject weaponInstance;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isSkillUsing = (weaponInstance != null);

    }

    public override void stopSkill()
    {
        isSkillUsing = false;
    }

    public override void activateSkill()
    {
        CarStatus attacker = GetComponent<CarStatus>();
        int myDist = FindObjectOfType<RankingSystem>().GetCarDist(GetComponent<Car>());
        for (int i = 0; i < 10; ++i)
        {
            CarCheckPoint respawnPoint = findCheckPoint(myDist + i);
            if (respawnPoint == null)
            {
                return;
            }
            Vector3 spawnPos = new Vector3(
                respawnPoint.transform.position.x,
                respawnPoint.transform.position.y -
                respawnPoint.transform.localScale.y / 2 + 1,
                respawnPoint.transform.position.z
                );

            Quaternion spawnRotation = Quaternion.Euler(new Vector3(20*Random.value-10, 0, 20 * Random.value - 10));
            int spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], spawnPos, spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;

            Vector3 posLeft = spawnPos - respawnPoint.transform.right * 5f;
            spawnRotation = Quaternion.Euler(new Vector3(20 * Random.value - 10, 0, 20 * Random.value - 10));
            spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], posLeft, spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;

            Vector3 posRight = spawnPos + respawnPoint.transform.right * 5f;
            spawnRotation = Quaternion.Euler(new Vector3(20 * Random.value - 10, 0, 20 * Random.value - 10));
            spearIdx = Random.Range(0, spears.Length);
            weaponInstance = Instantiate(spears[spearIdx], posRight, spawnRotation);
            weaponInstance.GetComponent<TrapWeapons>().attacker = attacker;

            isSkillUsing = true;
        }
        

        if (skillAudio == null)
        {
            return;
        }
        // play once
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = skillAudio;
        source.volume = 1;
        source.loop = false;
        source.Play();
    }

    private CarCheckPoint findCheckPoint(int dist)
    {
        CarCheckPoint[] checkpoints = FindObjectsOfType<CarCheckPoint>();
        if (checkpoints.Length == 0)
        {
            return null;
        }
        dist = Mathf.Clamp(dist, 0, checkpoints.Length - 1);
        foreach (CarCheckPoint point in checkpoints)
        {
            if (point.dist == dist)
            {
                Debug.Log("there are " + checkpoints.Length + "points; respawned at " + point.name);
                return point;
            }
        }

        Debug.Log("Error in  findCheckPoint(" + dist + ")");
        return checkpoints[0];
    }
}
