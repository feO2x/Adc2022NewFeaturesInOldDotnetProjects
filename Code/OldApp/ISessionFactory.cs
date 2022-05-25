using System.Threading.Tasks;

namespace OldApp;

#nullable enable

public interface ISessionFactory<T>
{
    Task<T> OpenSessionAsync();
}