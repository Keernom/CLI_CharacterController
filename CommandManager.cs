using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TMP_Text _commandBlock;

    private Dictionary<string, Action<string>> _commands = new Dictionary<string, Action<string>>();
    private Queue<string[]> _commandExecutionOrder = new Queue<string[]>();

    private void Awake()
    {
        _commands.Add("Jump", (settings) => _playerController.Jump(settings));
        _commands.Add("Run", (settings) => _playerController.Run(settings));
        _commands.Add("Wait", (settings) => _playerController.Wait(settings));
    }

    public async void CompileCode()
    {
        string[] dividedText = _commandBlock.text.Split(';');

        foreach(var e in dividedText)
        {
            string element = e.Replace("\n", "");
            string[] commandText = element.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (_commands.ContainsKey(commandText[0]))
                _commandExecutionOrder.Enqueue(commandText);
        }

        while (_commandExecutionOrder.Count > 0)
        {
            string[] command = _commandExecutionOrder.Dequeue();
            if (command[0] == "Wait")
                await Task.Run(() => _commands[command[0]].Invoke(command[1]));
            else
                _commands[command[0]].Invoke(command[1]);
        }
    }    
}
