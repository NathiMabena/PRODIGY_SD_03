using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        ContactManager contactManager = new ContactManager();
        contactManager.LoadContacts();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Contact Management System");
            Console.WriteLine("\n1. Add Contact");
            Console.WriteLine("2. View Contacts");
            Console.WriteLine("3. Edit Contact");
            Console.WriteLine("4. Delete Contact");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    contactManager.AddContact();
                    break;
                case "2":
                    contactManager.ViewContacts();
                    break;
                case "3":
                    contactManager.EditContact();
                    break;
                case "4":
                    contactManager.DeleteContact();
                    break;
                case "5":
                    contactManager.SaveContacts();
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}

class Contact
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public override string ToString()
    {
        return $"{Name}, {PhoneNumber}, {Email}";
    }
}

class ContactManager
{
    private List<Contact> contacts = new List<Contact>();
    private const string filePath = "contacts.txt";

    public void LoadContacts()
    {
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    contacts.Add(new Contact
                    {
                        Name = parts[0],
                        PhoneNumber = parts[1],
                        Email = parts[2]
                    });
                }
            }
        }
    }

    public void SaveContacts()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var contact in contacts)
            {
                writer.WriteLine(contact.ToString());
            }
        }
    }

    public void AddContact()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Phone Number: ");
        string phoneNumber = Console.ReadLine();
        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        contacts.Add(new Contact { Name = name, PhoneNumber = phoneNumber, Email = email });
        Console.WriteLine("Contact added! Press any key to continue...");
        Console.ReadKey();
    }

    public void ViewContacts()
    {
        Console.WriteLine("Contacts:");
        foreach (var contact in contacts)
        {
            Console.WriteLine(contact);
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void EditContact()
    {
        Console.Write("Enter the name of the contact to edit: ");
        string name = Console.ReadLine();
        var contact = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (contact != null)
        {
            Console.Write("Enter new Phone Number (leave blank to keep current): ");
            string newPhoneNumber = Console.ReadLine();
            Console.Write("Enter new Email (leave blank to keep current): ");
            string newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newPhoneNumber))
            {
                contact.PhoneNumber = newPhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                contact.Email = newEmail;
            }
            Console.WriteLine("Contact updated! Press any key to continue...");
        }
        else
        {
            Console.WriteLine("Contact not found. Press any key to continue...");
        }
        Console.ReadKey();
    }

    public void DeleteContact()
    {
        Console.Write("Enter the name of the contact to delete: ");
        string name = Console.ReadLine();
        var contact = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (contact != null)
        {
            contacts.Remove(contact);
            Console.WriteLine("Contact deleted! Press any key to continue...");
        }
        else
        {
            Console.WriteLine("Contact not found. Press any key to continue...");
        }
        Console.ReadKey();
    }
}