namespace ContactManager.Services;
using ContactManager.Models;
public class FileService(string filePath)
{
    public string FilePath = filePath;

    // Create a method to save contacts to a file
    public void SaveContacts(List<Contact> contacts)
    {
        using (StreamWriter writer = new StreamWriter(FilePath, append: true))
        {
            foreach (var contact in contacts)
            {
                writer.WriteLine($"BEGIN:VCARD");
                writer.WriteLine($"FN:{contact.Name}");
                writer.WriteLine($"TEL:{contact.Phone}");
                writer.WriteLine($"EMAIL:{contact.Email}");
                writer.WriteLine($"END:VCARD");
            }
        }
    }

    // Create a method to load contacts from a file
    public List<Contact> LoadContacts()
    {
        List<Contact> contacts = new List<Contact>();

        if (File.Exists(FilePath))
        {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                string name = "";
                string email = "";
                string phone = "";

                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("FN:"))
                    {
                        name = line.Substring(3);
                    }
                    else if (line.StartsWith("TEL:"))
                    {
                        phone = line.Substring(4);
                    }
                    else if (line.StartsWith("EMAIL:"))
                    {
                        email = line.Substring(6);
                    }
                    else if (line.StartsWith("END:VCARD"))
                    {
                        contacts.Add(new Contact(name, email, phone));
                        name = email = phone = "";
                    }
                }
            }
        }

        return contacts;
    }

    // Create a method to delete a contact from a file
    public void DeleteContact(string email)
    {
        List<Contact> contacts = LoadContacts();
        Contact contactToDelete = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (contactToDelete != null)
        {
            contacts.Remove(contactToDelete);
            SaveContacts(contacts);
            Console.WriteLine("Contact supprimé avec succès.");
        }
        else
        {
            Console.WriteLine("Aucun contact trouvé avec cet email.");
        }
    }

    // Create a method to search for contacts by name
    public List<Contact> SearchContacts(string searchTerm)
    {
        List<Contact> contacts = LoadContacts();
        return contacts.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Create a method for exporting contact to a file 
    public void ExportContact(string filename, string email)
    {

        List<Contact> contacts = LoadContacts();
        Contact contactData = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        using (StreamWriter writer = new StreamWriter("Data/" + filename + ".vcf"))
        {
            writer.WriteLine($"BEGIN:VCARD");
            writer.WriteLine($"FN:{contactData.Name}");
            writer.WriteLine($"TEL:{contactData.Phone}");
            writer.WriteLine($"EMAIL:{contactData.Email}");
            writer.WriteLine($"END:VCARD");
        }
        Console.WriteLine("Contact exporté avec succès.");
    }
}