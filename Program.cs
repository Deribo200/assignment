using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Enter a date (DD/MM/YYYY) or type 'exit' to quit: ");
            string userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "exit")
            {
                Console.WriteLine("Exiting the program.");
                break;
            }

            if (DateTime.TryParse(userInput, out DateTime inputDate))
            {
                HandleValidDate(inputDate);
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a date in DD/MM/YYYY format.");
            }
        }
    }

    static void HandleValidDate(DateTime date)
    {
        DateTime currentDate = DateTime.Today;

        if (date < currentDate)
        {
            TimeSpan daysPassed = currentDate - date;
            Console.WriteLine($"{daysPassed.Days} days have passed since {date:d}.");
        }
        else if (date > currentDate)
        {
            TimeSpan daysRemaining = date - currentDate;
            Console.WriteLine($"{daysRemaining.Days} days remaining until {date:d}.");
        }
        else
        {
            Console.WriteLine($"The date {date:d} is today!");
        }
    }
}

