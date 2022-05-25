using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Light.EmbeddedResources;

namespace OldApp;

#nullable enable

public sealed class SqlGetContactsSession : AsyncReadOnlySession, IGetContactsSession
{
    public SqlGetContactsSession(SqlConnection connection) : base(connection) { }

    public async Task<List<Contact>> GetContactsAsync()
    {
        using var command = CreateCommand(this.GetEmbeddedResource("GetContacts.sql"));
        using var reader = await Task.Factory.FromAsync(command.BeginExecuteReader, command.EndExecuteReader, null);
        return reader.DeserializeContacts();
    }
}