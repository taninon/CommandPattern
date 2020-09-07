using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
	private Transform _player;
	private float _h;
	private float _speed;

	public RotateCommand(Transform player, float h,float speed)
	{
		_player = player;
		_h = h;
		_speed = speed;
	}

	public void Execute()
	{
		_player.Rotate(new Vector3(0,_h * _speed, 0));
	}

	public void Undo()
	{
		_player.Rotate(new Vector3(0,-_h * _speed, 0));
	}
}