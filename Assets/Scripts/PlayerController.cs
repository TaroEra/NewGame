using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//先ほど作成したJoystick
	[SerializeField]
	private Joystick _joystick = null;

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		movement = new Vector3 (_joystick.Position.x * speed, 0.0f, _joystick.Position.y * speed);

		rb.velocity = movement;

		//Direction of Caracter's face
		if (_joystick.Position.x != 0 || _joystick.Position.y != 0) {
			rb.rotation = Quaternion.LookRotation (transform.position + (Vector3.right * _joystick.Position.x) + (Vector3.forward * _joystick.Position.y) - transform.position);
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
