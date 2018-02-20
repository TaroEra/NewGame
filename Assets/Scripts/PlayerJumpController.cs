using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {

	private Animator animator;
	private CharacterController characterController;
	//速度
	private Vector3 velocity;
	//ジャンプ力
	[SerializeField]
	private float jumpPower = 5f;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		characterController = GetComponent<CharacterController> ();
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (characterController.isGrounded) {
			velocity = Vector3.zero;
			var input = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));

			//方向キーが多少押されてる
			if(input.magnitude > 0f){
				animator.SetFloat ("Speed", input.magnitude);
				transform.LookAt (transform.position + input);
				velocity += input.normalized * 2;
			}
			//キーの押しが小さすぎる場合は移動しない
			else {
				animator.SetFloat ("Speed", 0);
			}
			//ジャンプキー（defaultではSpace）を押したらY軸方向の速度にジャンプ力を足す
			if(Input.GetButtonDown("Jump")){
				velocity.y += jumpPower;
			}
		}
		velocity.y += Physics.gravity.y * Time.deltaTime;
		characterController.Move (velocity * Time.deltaTime);
	}
}
