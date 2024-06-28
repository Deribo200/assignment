using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a date in mm/dd/yyyy format (e.g., 03/21/2024), or 'exit' to quit:");

        while (true)
        {
            string input = Console.ReadLine().Trim();

            if (input.ToLower() == "exit")
            {
                Console.WriteLine("Exiting the program.");
                break;
            }

            try
            {
                // Validate input format
                if (!DateTime.TryParse(input, out DateTime date))
                {
                    Console.WriteLine("Invalid date format. Please enter a date in mm/dd/yyyy format.");
                    continue;
                }

                // Convert date format
                string convertedDate = ReverseDateFormat(input);
                Console.WriteLine($"Converted date: {convertedDate}");
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("Regex operation timed out. Please try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    static string ReverseDateFormat(string input)
    {
        // Define regex pattern for mm/dd/yyyy format
        string pattern = @"^(?<mon>\d{1,2})/(?<day>\d{1,2})/(?<year>\d{2,4})$";

        // Timeout setting for regex operation
        TimeSpan timeout = TimeSpan.FromSeconds(1); // 1 second timeout

        // Apply regex with timeout
        Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture);
        Match match = regex.Match(input);

        // Handle timeout
        if (!match.Success)
        {
            throw new RegexMatchTimeoutException("Regex operation timed out.");
        }

        // Extract components from regex match
        int month = int.Parse(match.Groups["mon"].Value);
        int day = int.Parse(match.Groups["day"].Value);
        int year = int.Parse(match.Groups["year"].Value);

        // Format the date as yyyy-mm-dd
        string formattedDate = $"{year:D4}-{month:D2}-{day:D2}";

        return formattedDate;
    }
}
