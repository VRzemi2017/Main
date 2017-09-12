﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CreatureScript: MonoBehaviour {

	public LayerMask enemyLayer;

	//Basic parametrs
	public float moveSpeed = 15.0f;     // move speed

	//Self components
	private Transform myTransform;
	Animator anim;

	//Targeting
	[SerializeField]
	private Transform[] targets; // target array
	private int nowTarget; // current target

	// Time manager
	private float timer;
	private float stopTimer = 0f;

	private float timer2;

	[Range(1f, 10f)]
	public float attackTimer = 4f;

	[Range(1f, 100f)]
	public float damage = 1f;

	public bool patrolLoop; 			// path loop
	private bool playerSpotted = false; // Player spotted

	private bool onGround = false;
	private bool isJumping = false;

	//Players
	//private Transform player1;
	//private Transform player2;

	private Transform targetPlayer = null;

	private Transform mainCamera;

	private void Start( ){
		enemyLayer = gameObject.layer;
		myTransform = transform; 										 // Transform set
		//player1 = GameObject.FindGameObjectWithTag("Player").transform; // find Player1 position
//		player2 = GameObject.FindGameObjectWithTag ("Player2").transform;	// find Player2 position
		mainCamera = GameObject.FindGameObjectWithTag ( "MainCamera" ).transform;
		anim = GetComponent<Animator> ();
	}
		
	private void Update(){
		// Move along array of targets
		if (nowTarget < targets.Length) {
			Move ();
		} else if (patrolLoop) {
			nowTarget = 0; // loop
		} 

		Animate ();
	}
		
	// Move towards target
	private void Move(){

		// Path patrol
		Vector3 target = targets[nowTarget].position;
		Vector3 dir = targets[nowTarget].position - myTransform.position;
		myTransform.position = Vector3.MoveTowards (myTransform.position, target, moveSpeed * Time.deltaTime);

		// Targeting
		if (dir.magnitude < 0.5f) {
			if (timer == 0) {
				timer = Time.time;
			}
			if ((Time.time - timer) >= stopTimer ) {
				nowTarget++;
				timer = 0; 
			}
		} else if (playerSpotted) {

			if (targetPlayer) 
			{
				MainManager.EventTriggered(new MainManager.EventData() { gameEvent = MainManager.GameEvent.EVENT_DAMAGE });
				target = targetPlayer.position;
				dir = targetPlayer.position - myTransform.position;
				myTransform.position = Vector3.MoveTowards (myTransform.position, target, moveSpeed * Time.deltaTime);
				myTransform.LookAt (mainCamera);
				timer2 += Time.deltaTime;
				Debug.Log ("player spotted");			}

			//leave player alone after 4 seconds
			if (targetPlayer == null || timer2 >= attackTimer) {
				timer2 = 0;
				playerSpotted = false;
				targetPlayer = null;
			}
		}
	}

	//ENTER COLLIDER
	private void OnTriggerEnter( Collider other ) {

		if (enemyLayer != LayerMask.NameToLayer ("EnemyLayer")) {
			return;
		} else {
			Debug.Log ("Layers match");
		}

		if (other.CompareTag("SpeedUp")) { 			 // speed boost to simulate jumping ( blue spheres )
			moveSpeed = 15.0f;
			stopTimer = 0f;
			myTransform.localRotation = Quaternion.identity;
			isJumping = true;
			onGround = false;
		} /*else if (other.tag == "SpeedNormal") { // reset speed to normal ( yellow spheres ) !! buggy, try not to use
			stopTimer = 3.0f;
			moveSpeed = 5.0f;
		}*/ else if (other.CompareTag("Wait") || other.CompareTag("Spawn")) {		 // wait before moving / jumping ( orange spheres )
			stopTimer = 3.0f;
			moveSpeed = 15.0f;
			myTransform.localRotation = Quaternion.identity;
			isJumping = false;
			onGround = true;
		} 
			
		// Rotate transform towards next target
		if (other.CompareTag ("SpeedUp") || other.CompareTag ("Wait")) {
			myTransform.LookAt (targets [nowTarget + 1].position);
		} else if (other.CompareTag ("Spawn")) {
			myTransform.LookAt (targets [nowTarget + 1].position);
		}

		MainManager.GetPlayers ().ToList ().ForEach (p => {
			if (p == other.gameObject)
			{
				playerSpotted = true;
				moveSpeed = 15.0f;
				Debug.Log("Player spotted");

				targetPlayer = p.transform;
			}
		});
	}

	// EXIT COLLIDER
	private void OnTriggerExit ( Collider other ) {
		// reset timer to normal
		if (other.CompareTag("Wait") || other.CompareTag("SpeedUp")) {
			stopTimer = 0.5f;
		}
			
		// Destroy game object if it leaves Playzone ( DEBUG )
//		if (other.CompareTag("Playzone")) {
//			Destroy (gameObject);
//		}
	}

	private void Animate(){
		if (isJumping == true || playerSpotted == true ) {
			anim.SetTrigger ("Wait");
		} else if (isJumping == false) {
			anim.SetTrigger ("Jump");
		}
	}
		
}

