using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
	private static CommandManager _instance;
	public static CommandManager Instance
	{
		get
		{
			if (_instance == null)
			{
				//もし_instance == nullならばシーンからとってくる
				return FindObjectOfType<CommandManager>() as CommandManager;
			}
			return _instance;
		}
	}

	public class FrameCommand
	{
		private List<ICommand> _commandBuffer = new List<ICommand>();

		public IEnumerable<ICommand> CommandBuffer
		{
			get
			{
				return _commandBuffer;
			}
		}

		public void AddCommands(List<ICommand> commands)
		{
			_commandBuffer = commands;
		}

		public void Execute()
		{
			foreach (var command in _commandBuffer)
			{
				command.Execute();
			}
		}

		public void Undo()
		{
			foreach (var command in _commandBuffer)
			{
				command.Undo();
			}
		}

	}

	//	private List<ICommand> _commandBuffer = new List<ICommand>();
	private List<FrameCommand> _frameCommandBuffer = new List<FrameCommand>();

	public bool Locked;

	private void Awake()
	{
		_instance = this;
	}

	public void AddCommand(List<ICommand> commands)
	{
		var frameCommand = new FrameCommand();
		frameCommand.AddCommands(commands);

		_frameCommandBuffer.Add(frameCommand);
	}

	public void Rewind()
	{
		// ロックされているときは何もしない
		if (Locked) return;

		Locked = true;
		StartCoroutine(RewindCoroutine());
	}

	public void PlayBack()
	{
		// ロックされているときは何もしない
		if (Locked) return;

		Locked = true;
		StartCoroutine(PlayBackCoroutine());
	}

	private IEnumerator PlayBackCoroutine()
	{
		Debug.Log("Playback Start");
		foreach (var frameCommand in _frameCommandBuffer)
		{
			frameCommand.Execute();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Playback End");
		Locked = false;
	}

	private IEnumerator RewindCoroutine()
	{
		Debug.Log("Rewind Start");
		///Listを逆からとる。List.Reverse()はListの中身を変えてしまうのでEnumerable.Reversを使う
		foreach (var frameCommand in Enumerable.Reverse(_frameCommandBuffer))
		{
			frameCommand.Undo();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Rewind End");
		Locked = false;
	}
}


