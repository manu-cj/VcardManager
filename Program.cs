namespace ContactManager;
using ContactManager.Services;

public class Program
{
    public static void Main(string[] args) 
    {
        ContactService contactService = new ContactService();
        contactService.ShowMenu();
    }
}
