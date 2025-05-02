namespace ContactManager.Services;

public class InputValidator
{
    // Create a method to validate the name
    public static string ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Le nom ne peut pas être vide.");
        }
        return name;
    }

    // Create a method to validate the email
    public static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new ArgumentException("L'email est invalide.");
        }
        return email;
    }

    // Create a method to validate the phone number
    public static string ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length < 10)
        {
            throw new ArgumentException("Le numéro de téléphone est invalide.");
        }
        return phone;
    }

    // Create a method to validate the filename
    public static string ValidateFilename(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename) || !filename.EndsWith(".vcf"))
        {
            throw new ArgumentException("Nom de fichier invalide. Veuillez entrer un nom de fichier valide avec l'extension .vcf.");
        }
        return filename;
    }

    // Create a method to validate the search term
    public static string ValidateSearchTerm(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentException("Le terme de recherche ne peut pas être vide.");
        }
        return searchTerm;
    }

}