using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed;
	[SerializeField] private float _speedRotate;

	private void Update()
	{
		if (CommandManager.Instance.Locked) {
			return;
		}

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		var flameCommand = new List<ICommand>();

		var move = new MoveCommand(this.transform,h,v,_speed);
		flameCommand.Add(move);
		move.Execute();

		if (Input.GetKey(KeyCode.Z))
		{
			var rotate = new RotateCommand(this.transform, -1, _speedRotate);
			rotate.Execute();
			flameCommand.Add(rotate);
		}


		if (Input.GetKey(KeyCode.X))
		{
			var rotate = new RotateCommand(this.transform, +1, _speedRotate);
			rotate.Execute();
			flameCommand.Add(rotate);

		}
		CommandManager.Instance.AddCommand(flameCommand);
	}

}
