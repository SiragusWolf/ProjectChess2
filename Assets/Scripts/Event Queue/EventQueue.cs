using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueue : MonoBehaviour
{
    private List<ICommand> currentCommands = new();
    private Stack<IUndoableCommand> undoableCommands = new();
    public static EventQueue Instance { get; private set; }
    private static EventQueue _instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EnqueueCommand(ICommand command)
    {
        currentCommands.Add(command);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoLatest();
        }
    }

    private void LateUpdate()
    {
        if (currentCommands.Count == 0)
            return;

        foreach (var command in currentCommands)
        {
            command.Execute();
            if (command is IUndoableCommand undoableCommand)
            {
                undoableCommands.Push(undoableCommand);
            }
        }

        currentCommands.Clear();
    }

    public void UndoLatest()
    {
        if (undoableCommands.Count == 0)
        {
            return;
        }

        IUndoableCommand command = undoableCommands.Pop();
        command.Undo();
    }
}
