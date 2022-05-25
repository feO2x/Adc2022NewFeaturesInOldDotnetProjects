using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using LightInject;

namespace OldApp;

#nullable enable

/* Beware that Microsoft is not officially supporting most of the stuff that is shown in this code base.
   But it works. */

public static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var configuration = CreateConfiguration();
        var container = new ServiceContainer();
        container.Register(f => new SqlConnection(configuration.ConnectionString));
        container.Register<IGetContactsSession, SqlGetContactsSession>();
        container.Register<ISessionFactory<IGetContactsSession>, DefaultSessionFactory<IGetContactsSession>>();
        container.Register<MainFormViewModel>();
        container.Register<MainForm>();

        var mainForm = container.GetInstance<MainForm>();
        Application.Run(mainForm);
    }

    private static Configuration CreateConfiguration()
    {
        var configuration = Json.DeserializeFile<Configuration>("appsettings.json");
        if (File.Exists("appsettings.Development.json"))
            configuration = Json.DeserializeFile<Configuration>("appsettings.Development.json");
        return configuration;
    }
}