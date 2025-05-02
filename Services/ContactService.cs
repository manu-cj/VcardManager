
namespace ContactManager.Services;
using ContactManager.Models;
using Spectre.Console;


public class ContactService
{

    List<Contact> contacts = new List<Contact>();
    //Crate a menu to manage contacts
    public void ShowMenu()
    {
        bool cancel = false;

        while (!cancel)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold blue]=== CONTACT MANAGER ===[/]\nChoisissez une option :")
                    .AddChoices(new[]
                    {
                    "Afficher les contacts",
                    "Ajouter un contact",
                    "Rechercher un contact",
                    "Exporter un contact",
                    "Supprimer un contact",
                    "Quitter"
                    }));

            switch (choice)
            {
                case "Afficher les contacts":
                    getContact();
                    break;
                case "Ajouter un contact":
                    addContact();
                    break;
                case "Rechercher un contact":
                    searchContact();
                    break;
                case "Exporter un contact":
                    exportContact();
                    break;
                case "Supprimer un contact":
                    deleteContact();
                    break;
                case "Quitter":
                    cancel = true;
                    AnsiConsole.MarkupLine("[green]Au revoir ![/]");
                    break;
            }

            if (!cancel)
            {
                AnsiConsole.MarkupLine("\n[grey]Appuyez sur une touche pour revenir au menu...[/]");
                Console.ReadKey();
            }
        }
    }


    // Create a method to display the list of contacts
    public void getContact()
    {
        var filename = "Data/contacts.vcf";
        var fileService = new FileService(filename);
        contacts = fileService.LoadContacts();

        if (contacts == null || contacts.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]❌ Aucun contact trouvé.[/]");
            return;
        }

        var table = new Table();

        table.Border = TableBorder.Rounded;
        table.BorderColor(Color.Cyan1);
        table.Expand();

        table.AddColumn(new TableColumn("[yellow bold]👤 Nom[/]").Centered());
        table.AddColumn(new TableColumn("[green bold]📧 Email[/]").Centered());
        table.AddColumn(new TableColumn("[blue bold]📞 Téléphone[/]").Centered());

        foreach (var contact in contacts)
        {
            table.AddRow(
                $"[yellow]{contact.Name}[/]",
                $"[green]{contact.Email}[/]",
                $"[blue]{contact.Phone}[/]"
            );
        }

        var panel = new Panel(table)
            .Header("[bold underline deepskyblue1]📋 Liste des contacts[/]", Justify.Center)
            .Border(BoxBorder.Double)
            .BorderStyle(new Style(Color.Purple))
            .Padding(1, 1);

        AnsiConsole.Write(panel);
    }



    // Create a method to add a contact
    public void addContact()
    {
        Console.WriteLine("=== Ajouter un contact ===");
        Console.Write("Nom: ");
        string name = Console.ReadLine();
        // Validate the name
        InputValidator.ValidateName(name);
        Console.Write("Email: ");
        string email = Console.ReadLine();
        // Validate the email
        InputValidator.ValidateEmail(email);
        Console.Write("Téléphone: ");
        string phone = Console.ReadLine();
        // Validate the phone number
        InputValidator.ValidatePhone(phone);

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
        // Validate the email
        InputValidator.ValidateEmail(email);

        Console.Write("Nom du fichier (avec extension .vcf): ");
        string filename = Console.ReadLine();
        // Validate the filename
        InputValidator.ValidateFilename(filename);

        FileService fileService = new FileService("Data" + filename);
        fileService.DeleteContact(email);
        contacts = fileService.LoadContacts();
        Console.WriteLine("Contact supprimé avec succès.");
    }

    // Create a method to search for a contact
    public void searchContact()
    {
        Console.WriteLine("=== Rechercher un contact ===");
        Console.Write("Nom ou email: ");
        string searchTerm = Console.ReadLine();
        // Validate the search term
        InputValidator.ValidateSearchTerm(searchTerm);

        FileService fileService = new FileService("Data/contacts.vcf");
        contacts = fileService.SearchContacts(searchTerm);
        if (contacts == null || contacts.Count == 0)
        {
            Console.WriteLine("Aucun contact trouvé.");
            return;
        }
        Console.WriteLine("=== Résultats de la recherche ===");
        Console.WriteLine($"Nombre de contacts trouvés : {contacts.Count}");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Nom: {contact.Name}, Email: {contact.Email}, Téléphone: {contact.Phone}");
        }
        Console.WriteLine("=== Fin de la recherche ===");
    }

    // Create a method to export contacts to a file
    public void exportContact()
    {
        Console.WriteLine("=== Exporter le contacts ===");
        Console.Write("Email du contact à exporter: ");
        string email = Console.ReadLine();
        // Validate the email
        InputValidator.ValidateEmail(email);

        Console.Write("Nom du fichier: ");
        string filename = Console.ReadLine();

        // Save the contacts to a file
        FileService fileService = new FileService("Data/contacts.vcf");
        fileService.ExportContact(filename, email);
        Console.WriteLine("Contacts exportés avec succès.");
    }
}
