using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private float yaw = 0.0f;
	private float pitch = 0.0f;
	private float speedRot = 1.0f;
	private float speedMov = 3.0f;

    private bool swap = false;

	// See https://answers.unity.com/questions/827834/click-and-drag-camera.html for idea
	private void Update()
	{

		if (Input.GetMouseButton(1))
		{
			yaw += speedRot * Input.GetAxis("Mouse X");
			pitch += speedRot * Input.GetAxis("Mouse Y");
			if(pitch > 90f) {
				pitch = 90;
			}
			if (pitch < 45f)
			{
				pitch = 45f;
			}

			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);                                        
		}

		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(0, 0, speedMov * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate((-1) * speedMov * Time.deltaTime, 0, 0);
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.Translate(0, 0, (-1) * speedMov * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(speedMov * Time.deltaTime, 0, 0);
		}
	}

    public void SpinCamera()
    {
        float z = 12;
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);

        if (swap)
        {
            z *= -1;
        }
        transform.Translate(0.0f, 0.0f, z, Space.World);
        swap = !swap;
    }
}
