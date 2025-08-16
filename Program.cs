using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Email
{
    public string Name { get; set; }
    public string Domain { get; set; }

   
    private static readonly Regex EmailRegex = new Regex(
        @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
        RegexOptions.Compiled);

    public static bool IsValid(string email)
    {
        return EmailRegex.IsMatch(email);
    }

    public static Email FromString(string email)
    {
        email = email.ToLower();
        var parts = email.Split('@');
        return new Email { Name = parts[0], Domain = parts[1] };
    }
}


namespace _22__Email_Validator___Grouper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "John.Doe@example.com, jane_smith@Gmail.com, invalid-email, bob@Example.com, alice@yahoo.com, another.invalid@com";

            var emails = input.Split(',')
                              .Select(e => e.Trim())
                              .ToList();

            List<string> invalidEmails = new List<string>();
            List<Email> validEmails = new List<Email>();

            foreach (var e in emails)
            {
                if (Email.IsValid(e))
                {
                    validEmails.Add(Email.FromString(e));
                }
                else
                {
                    invalidEmails.Add(e);
                }
            }

            var grouped = validEmails
                .GroupBy(e => e.Domain)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Select(e => e.Name).OrderBy(n => n).ToList());

            Console.WriteLine("=== Invalid Emails ===");
            if (invalidEmails.Count > 0)
                invalidEmails.ForEach(Console.WriteLine);
            else
                Console.WriteLine("None");

            Console.WriteLine("\n=== Valid Emails Grouped by Domain ===");
            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key}: {string.Join(", ", group.Value)}");
            }
        }
    }
}




