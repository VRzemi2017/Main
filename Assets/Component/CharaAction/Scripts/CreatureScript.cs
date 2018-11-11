using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UniRx;
using UniRx.Triggers;

public class CreatureScript: MonoBehaviour {

	//Layer
	public LayerMask enemyLayer;

	//Speed
	private float moveSpeed = 15.0f;     // move speed

	//Self components
	private Transform myTransform;
	private Animator anim;

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
	private bool attacked = false;

	public bool patrolLoop; 		
	private bool playerSpotted = false; 

	//private bool onGround = false;
	private bool isJumping = false;

	private Transform targetPlayer = null;

	private Transform mainCamera;

    private System.IDisposable dis;

	private void Start( ){
		enemyLayer = gameObject.layer;
		myTransform = transform;
		mainCamera = GameObject.FindObjectOfType<VRProxy>().Camera.transform;
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
		if (myTransform == null) 
		{
			Debug.Log ("myTransform is null");
			return;
		}

		// Path patrol
		Vector3 target = targets[nowTarget].position;
		Vector3 dir = targets[nowTarget].position - myTransform.position;
		myTransform.position = Vector3.MoveTowards (myTransform.position, target, moveSpeed * Time.deltaTime);

		// Targeting
		if (dir.magnitude < 0.5f) {

			if ((Time.time - timer) >= stopTimer ) {
				nowTarget++;
			}
		} else if (playerSpotted && targetPlayer && attacked == false){
			
				MainManager.EventTriggered (new EventData () { gameEvent = GameEvent.EVENT_DAMAGE, eventObject = targetPlayer.gameObject });
                Debug.Log("Send Damage player: " + targetPlayer.gameObject.GetComponent<MonobitEngine.MonobitView>().viewID);
				target = targetPlayer.position;
				dir = targetPlayer.position - myTransform.position;
				myTransform.position = Vector3.MoveTowards (myTransform.position, target, moveSpeed * Time.deltaTime);
				myTransform.LookAt (mainCamera);
				Debug.Log ("Player damage");
				attacked = true;
		 } 
			
		//leave player alone after 4 seconds
		if (playerSpotted && (targetPlayer == null || (Time.time - timer2) >= attackTimer)) {
			
			playerSpotted = false;
			targetPlayer = null;
			Debug.Log ("Leave player:"  + attacked + "Time: " + Time.time + " timer: " + timer2);
		}

//			if (attacked) {
//			Debug.Log ("player:" + targetPlayer + " timer: " + timer2);
//			}

		if (playerSpotted == false && attacked == true) {
            if (dis == null)
            {
                dis = Observable.Timer (System.TimeSpan.FromSeconds (10)).Subscribe (_ => {
				    attacked = false;
                    dis.Dispose();
                    dis = null;
			    });
            }
		}
	}

	//ENTER COLLIDER
	private void OnTriggerEnter( Collider other ) {

		if (enemyLayer != LayerMask.NameToLayer ("EnemyLayer") && attacked) {
			return;
		} 

        if (myTransform == null)
        {
            return;
        }

		//Move self to targets and speed change
		if (other.CompareTag("SpeedUp")) { 
			moveSpeed = 15.0f;
			stopTimer = 0f;
			myTransform.localRotation = Quaternion.identity;
			isJumping = true;
			//onGround = false;
		} else if (other.CompareTag("Wait") || other.CompareTag("Spawn")) {
			stopTimer = 3.0f;
			moveSpeed = 15.0f;
			myTransform.localRotation = Quaternion.identity;
			isJumping = false;
			timer = Time.time;
			//onGround = true;
		} 
			
		//Rotate self to targets
		if (other.CompareTag ("SpeedUp") || other.CompareTag ("Wait")) {
			myTransform.LookAt (targets [(nowTarget+1)% targets.Length].position);
		} 

		//Player spotted
		MainManager.GetPlayers ().ToList ().ForEach (p => {
			if (p == other.gameObject)
			{
				playerSpotted = true;
				timer2 = Time.time;
				moveSpeed = 15.0f;
				Debug.Log("Player spotted: " + attacked + "Time: " + Time.time + " timer: " + timer2);
				targetPlayer = p.transform;
			}
		});
	}

	//Animator
	private void Animate(){
		if (isJumping == true || playerSpotted == true ) {
			anim.SetTrigger ("Wait");
            MainManager.EventTriggered(new EventData() { gameEvent = GameEvent.EVENT_ENEMY_WAIT, eventObject = gameObject });
        } else if (isJumping == false) {
			anim.SetTrigger ("Jump");
            MainManager.EventTriggered(new EventData() { gameEvent = GameEvent.EVENT_ENEMY_JUMP, eventObject = gameObject });
        }
	}
		
}

