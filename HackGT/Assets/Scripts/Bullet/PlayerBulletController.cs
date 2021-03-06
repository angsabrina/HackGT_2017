﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    public float movementSpeed;
    public float finalVelocity;
    private float timePassed;
    public float timeAlive;
    public int damage;
    private GameObject owner;
    public AudioClip clip;
    // Use this for initialization
    void Start() {
        timeAlive = 5;
        timePassed = 0f;
        damage = 10;
    }

    // Update is called once per frame
    void Update() {
        if (!GameObject.Find("TimeManager").GetComponent<TimeManager>().Stopped()) {
            movementSpeed = finalVelocity;
            timePassed = 0;
        }
        transform.position += transform.forward * GameObject.Find("TimeManager").GetComponent<TimeManager>().DeltaTime() * movementSpeed;
        timePassed += GameObject.Find("TimeManager").GetComponent<TimeManager>().DeltaTime();
        if (timePassed >= timeAlive) {
            Destroy(gameObject);
        }
    }

    public void SetOwner(GameObject obj) {
        owner = obj;
    }

    void OnCollisionEnter(Collision col) {
        Debug.Log(owner);

        if (col.gameObject.tag == "Player" && owner.tag == "Enemy") {
            AudioSource.PlayClipAtPoint(clip, GameObject.Find("Player").transform.position);
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            Destroy(gameObject);
        } else if (col.gameObject.tag == "Enemy" && owner.tag == "Player") {
            AudioSource.PlayClipAtPoint(clip, GameObject.Find("Player").transform.position);
            Debug.Log(col);
            Debug.Log(owner);

            col.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (col.gameObject.layer == 8) {

            Destroy(gameObject);
        }
    }
}
