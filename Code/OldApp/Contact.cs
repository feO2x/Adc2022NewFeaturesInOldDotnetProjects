using System.Collections.Generic;
using System.Data.SqlClient;

namespace OldApp;

#nullable enable

public record Contact(int Id, string FirstName, string LastName);

public static class ContactExtensions
{
    public static List<Contact> DeserializeContacts(this SqlDataReader reader)
    {
        var contacts = new List<Contact>();
        if (!reader.HasRows)
            return contacts;

        var idOrdinal = reader.GetOrdinal(nameof(Contact.Id));
        var firstNameOrdinal = reader.GetOrdinal(nameof(Contact.FirstName));
        var lastNameOrdinal = reader.GetOrdinal(nameof(Contact.LastName));

        while (reader.Read()) // No way to read async ¯\_(ツ)_/¯
        {
            var id = reader.GetInt32(idOrdinal);
            var firstName = reader.GetString(firstNameOrdinal);
            var lastName = reader.GetString(lastNameOrdinal);
            var contact = new Contact(id, firstName, lastName);
            contacts.Add(contact);
        }

        return contacts;
    }
}