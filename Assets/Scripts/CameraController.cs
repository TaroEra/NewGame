using UnityEngine;

public class CameraController : MonoBehaviour {

	public float baseDistance = 5f;
	public float baseCameraHeight = 2f;
	public float chaseDamper = 3f;
	public Transform player;
	private Transform cam;

	void Start () {
		cam = GetComponent<Camera> ().transform;
	}

	void FixedUpdate() {

		//カメラの位置を設定
		var desiredPos = player.position - player.forward * baseDistance + Vector3.up * baseCameraHeight;
		cam.position = Vector3.Lerp (cam.position, desiredPos, Time.deltaTime * chaseDamper);

		//カメラの向きを設定
		cam.LookAt(player);
	}   
}
