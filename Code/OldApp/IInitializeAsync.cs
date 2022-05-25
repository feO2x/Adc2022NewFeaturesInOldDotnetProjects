using System.Threading.Tasks;

namespace OldApp;

#nullable enable

public interface IInitializeAsync
{
    bool IsInitialized { get; }
    Task InitializeAsync();
}