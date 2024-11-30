﻿namespace Meicrosoft.Social.Command.Infra.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _handler = [];

    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
    {
        if (_handler.ContainsKey(typeof(T)))
            throw new IndexOutOfRangeException("You cannot register the same command handler more than once");

        _handler.Add(typeof(T), x => handler((T)x));
    }

    public async Task SendAsync(BaseCommand command)
    {
        if (_handler.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
        {
            await handler(command);
        }
        else
        {
            throw new ArgumentNullException(nameof(handler), "No command handler was registered!");
        }
    }
}
