
namespace ContactManager.Services;
using ContactManager.Models;
public class ContactService
{

    List<Contact> contacts = new List<Contact>();
    //Crate a menu to manage contacts
    public void ShowMenu()
    {
        bool cancel = false;

        while (!cancel)
        {
            Console.Clear();
            Console.WriteLine("=== CONTACT MANAGER ===");
            Console.WriteLine("1. Afficher les contacts");
            Console.WriteLine("2. Ajouter un contact");
            Console.WriteLine("3. Supprimer un contact");
            Console.WriteLine("4. Quitter");
            Console.Write("Choix : ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    getContact();
                    break;
                case "2":
                    addContact();
                    break;
                case "3":
                    searchContact();
                    break;
                case "4":
                    exportContact();
                    break;
                case "5":
                    deleteContact();
                    break;
                case "6":
                    cancel = true;
                    Console.WriteLine("Au revoir !");
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }

            if (!cancel)
            {
                Console.WriteLine("\nAppuyez sur une touche pour revenir au menu...");
                Console.ReadKey();
            }
        }
    }

    // Create a method to display the list of contacts
    public void getContact()
    {
        Console.WriteLine("=== Liste des contacts ===");
        string filename = "Data/contacts.vcf";
        FileService fileService = new FileService(filename);
        contacts = fileService.LoadContacts();
        if (contacts == null || contacts.Count == 0)
        {
            Console.WriteLine("Aucun contact trouvé.");
            return;
        }
        
        Console.WriteLine("=== Résultats de la recherche ===");
        Console.WriteLine($"Nombre de contacts trouvés : {contacts.Count}");
        Console.WriteLine("=== Liste des contacts ===");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Nom: {contact.Name}, Email: {contact.Email}, Téléphone: {contact.Phone}");
        }
        Console.WriteLine("=== Fin de la liste ===");
    }

    // Create a method to add a contact
    public void addContact()
    {
        Console.WriteLine("=== Ajouter un contact ===");
        Console.Write("Nom: ");
        string name = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Téléphone: ");
        string phone = Console.ReadLine();

        // Check if the contact already exists
        if (contacts.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Un contact avec cet email existe déjà.");
            return;
        }


        contacts.Add(new Contact(name, email, phone));
        // Save the contact to a file (optional)
        string filename = "Data/contacts.vcf";
        FileService fileService = new FileService(filename);
        fileService.SaveContacts(contacts);
        Console.WriteLine("Contact ajouté avec succès.");
    }

    // Create a method to delete a contact
    public void deleteContact()
    {
        Console.WriteLine("=== Supprimer un contact ===");
        Console.Write("email du contact à supprimer: ");
        string email = Console.ReadLine();

        Contact contactToRemove = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Contact supprimé avec succès.");
        }
        else
        {
            Console.WriteLine("Contact non trouvé.");
        }
    }

    // Create a method to search for a contact
    public void searchContact()
    {
        Console.WriteLine("=== Rechercher un contact ===");
        Console.Write("Nom ou email: ");
        string searchTerm = Console.ReadLine();

        var foundContacts = contacts.Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if (foundContacts.Count > 0)
        {
            Console.WriteLine("=== Résultats de la recherche ===");
            foreach (var contact in foundContacts)
            {
                Console.WriteLine($"Nom: {contact.Name}, Email: {contact.Email}, Téléphone: {contact.Phone}");
            }
        }
        else
        {
            Console.WriteLine("Aucun contact trouvé.");
        }
    }

    // Create a method to export contacts to a file
    public void exportContact()
    {
        Console.WriteLine("=== Exporter les contacts ===");
        Console.Write("Nom du fichier (avec extension .vcf): ");
         string filename = Console.ReadLine();
        if (string.IsNullOrEmpty(filename) || !filename.EndsWith(".vcf"))
        {
            Console.WriteLine("Nom de fichier invalide. Veuillez entrer un nom de fichier valide avec l'extension .vcf.");
            return;
        }
        // Save the contacts to a file
        FileService fileService = new FileService("Data" + filename);
        fileService.SaveContacts(contacts);
        Console.WriteLine("Contacts exportés avec succès.");
    }
}
