using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Light.GuardClauses;

namespace OldApp;

#nullable enable

public abstract class AsyncReadOnlySession : IDisposable, IInitializeAsync
{
    private bool _isInitialized;

    protected AsyncReadOnlySession(SqlConnection connection, IsolationLevel transactionLevel = IsolationLevel.Unspecified)
    {
        Connection = connection.MustNotBeNull();
        TransactionLevel = transactionLevel;
    }

    protected IsolationLevel TransactionLevel { get; }
    protected SqlConnection Connection { get; }
    protected SqlTransaction? Transaction { get; private set; }

    public void Dispose()
    {
        Transaction?.Dispose();
        Connection.Dispose();
    }

    protected SqlCommand CreateCommand()
    {
        var command = Connection.CreateCommand();
        if (Transaction is not null)
            command.Transaction = Transaction;
        return command;
    }
    
    protected SqlCommand CreateCommand(string sql)
    {
        var command = CreateCommand();
        command.CommandText = sql;
        return command;
    }

    bool IInitializeAsync.IsInitialized => _isInitialized;

    async Task IInitializeAsync.InitializeAsync()
    {
        if (_isInitialized)
            return;

        await Connection.OpenAsync();
        if (TransactionLevel is not IsolationLevel.Unspecified)
            Transaction = Connection.BeginTransaction(TransactionLevel);
        _isInitialized = true;
    }
}