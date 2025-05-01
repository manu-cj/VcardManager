namespace ContactManager.Services;
using ContactManager.Models;
public class FileService(string filePath)
{
    public string FilePath = filePath;

    // Create a method to save contacts to a file
    public void SaveContacts(List<Contact> contacts)
    {
        using (StreamWriter writer = new StreamWriter(FilePath))
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
}