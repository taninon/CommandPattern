using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed;

	// Update is called once per frame
	private void Update()
	{
		float h = Input.GetAxis("Horizontal") * _speed;
		float v = Input.GetAxis("Vertical") * _speed;

		var move = new MoveCommand(this.transform,h,v);
		move.Execute();
	}
}
