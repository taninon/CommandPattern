using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed;

	private void Update()
	{
		if (CommandManager.Instance.Locked) {
			return;
		}

		float h = Input.GetAxis("Horizontal") * _speed;
		float v = Input.GetAxis("Vertical") * _speed;

		var move = new MoveCommand(this.transform,h,v);
		move.Execute();

		CommandManager.Instance.AddCommand(move);
	}

}
