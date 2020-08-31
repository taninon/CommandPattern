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

	private List<ICommand> _commandBuffer = new List<ICommand>();

	public bool Locked;

	private void Awake()
	{
		_instance = this;
	}

	public void AddCommand(ICommand command)
	{
		_commandBuffer.Add(command);
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
		foreach (var command in _commandBuffer)
		{
			command.Execute();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Playback End");
		Locked = false;
	}

	private IEnumerator RewindCoroutine()
	{
		Debug.Log("Rewind Start");
		///Listを逆からとる。List.Reverse()はListの中身を変えてしまうのでEnumerable.Reversを使う
		foreach (var command in Enumerable.Reverse(_commandBuffer))
		{
			command.Undo();
			yield return new WaitForEndOfFrame();
		}
		Debug.Log("Rewind End");
		Locked = false;
	}
}


