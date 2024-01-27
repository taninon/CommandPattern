using UnityEngine;

public class Player_test : MonoBehaviour
{
	[SerializeField] private float _speed;

	private void Update()
	{
		if (CommandManager.Instance.Locked) {
			return;
		}

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		var move = new MoveCommand(this.transform,h,v,_speed);
		move.Execute();

		CommandManager.Instance.AddCommand(move);
	}

}
