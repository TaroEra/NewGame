using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public Text countText;
	public Text winText;
	private int count;

	//ジャンプ力
	[SerializeField]
	private float jumpPower = 10;

	//先ほど作成したJoystick
	[SerializeField]
	private Joystick _joystick = null;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}
		
	void FixedUpdate()
	{

		//Direction of Caracter's face
		var inputHorizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
		var inputVertical = CrossPlatformInputManager.GetAxisRaw ("Vertical");

		//カメラの方向から、x-z平面の単位のベクトルを取得
		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

		//入力値とカメラの向きから、移動方向を決定
		Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

		//移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
		rb.velocity = moveForward * speed + new Vector3 (0, rb.velocity.y, 0);

		if (moveForward != Vector3.zero) {
			transform.rotation = Quaternion.LookRotation (moveForward);
		}

		//Direction of Caracter's face
//		var horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
//		var vertical = CrossPlatformInputManager.GetAxis ("Vertical");
//
//		if (horizontal != 0 || vertical != 0) {
//			//向き
//			rb.rotation = Quaternion.LookRotation (transform.position + (Vector3.right * horizontal) + (Vector3.forward * vertical) - transform.position);
//
//			rb.velocity = new Vector3 (horizontal * speed, rb.velocity.y, vertical * speed);
//		}

//		ジャンプ
		if ( CrossPlatformInputManager.GetButtonDown("Jump")) {
//			rb.velocity = new Vector3 (_joystick.Position.x * speed, jumpPower, _joystick.Position.y * speed);
			rb.velocity = new Vector3(inputHorizontal * speed, jumpPower, inputVertical * speed);
		} 


	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();

		if (count >= 9) {
			winText.text = "You Win!";
		}
	}

	private Vector3 touchStartPos;
//	private Vector3 touchEndPos;
//
//	void Flick()
//	{
//		if(Input.GetKeyDown(KeyCode.Mouse0)){
//			touchStartPos = new Vector3 (
//				Input.mousePosition.x,
//				Input.mousePosition.y,
//				Input.mousePosition.z
//			);
//		}
//
//		if (Input.GetKeyUp (KeyCode.Mouse0)) {
//			touchEndPos = new Vector3 (
//				Input.mousePosition.x,
//				Input.mousePosition.y,
//				Input.mousePosition.z
//			);
//			GetDirection ();
//		}
//	}
//
//	void GetDirection()
//	{
//		float directionX = touchEndPos.x - touchStartPos.x;
//		float directionY = touchEndPos.y - touchStartPos.y;
//
//		if (Mathf.Abs (directionY) < Mathf.Abs (directionX)) {
//			if (30 < directionX) {
//				//右フリック
//				Direction = "right";
//			} else if (-30 > directionX) {
//				//左フリック
//				Direction = "left";
//			}
//		}
//	}
}
