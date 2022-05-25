using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OldApp;

#nullable enable

public interface IGetContactsSession : IDisposable
{
    Task<List<Contact>> GetContactsAsync();
}