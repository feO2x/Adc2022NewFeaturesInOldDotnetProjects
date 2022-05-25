using System;
using System.Threading.Tasks;

namespace OldApp;

#nullable enable

public sealed class DefaultSessionFactory<T> : ISessionFactory<T>
{
    public DefaultSessionFactory(Func<T> createSession) =>
        CreateSession = createSession;

    private Func<T> CreateSession { get; }

    public async Task<T> OpenSessionAsync()
    {
        var session = CreateSession();
        if (session is IInitializeAsync { IsInitialized: false } toBeInitialized)
            await toBeInitialized.InitializeAsync();
        return session;
    }
}