using System.Collections.Generic;
using System.Threading.Tasks;
using OldApp.Common;

namespace OldApp;

#nullable enable

public sealed class MainFormViewModel : BaseNotifyPropertyChanged
{
    private List<Contact> _contacts = new ();
    private bool _isLoading;

    public MainFormViewModel(ISessionFactory<IGetContactsSession> sessionFactory) =>
        SessionFactory = sessionFactory;

    private ISessionFactory<IGetContactsSession> SessionFactory { get; }

    public List<Contact> Contacts
    {
        get => _contacts;
        private set => Set(out _contacts, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        private set => Set(out _isLoading, value);
    }

    public async Task LoadContactsAsync()
    {
        IsLoading = true;
        try
        {
            using var session = await SessionFactory.OpenSessionAsync();
            Contacts = await session.GetContactsAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }
}