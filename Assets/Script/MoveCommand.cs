using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
	private Transform _player;
	private float _v;
	private float _h;
	private int _id;

	public MoveCommand(Transform player,float h, float v)
	{
		_player = player;
		_v = v;
		_h = h;
	}

	public void Execute()
	{
		var SetPos = _player.position;
		SetPos.y += _v * Time.deltaTime;
		SetPos.x += _h * Time.deltaTime;

		_player.position = SetPos;
	}

	public void Undo()
	{
		var SetPos = _player.position;
		SetPos.y -= _v * Time.deltaTime;
		SetPos.x -= _h * Time.deltaTime;

		_player.position = SetPos;
	}
}
