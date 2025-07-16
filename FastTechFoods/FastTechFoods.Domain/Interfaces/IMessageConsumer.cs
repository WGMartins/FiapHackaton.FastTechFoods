namespace Domain.Interfaces;

public interface IMessageConsumer<T> where T : class
{
    event Func<T, Task>? OnMessageReceived;
    Task ConsumeAsync();
}

