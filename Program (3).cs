using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Setup directory and file paths
        string directoryPath = "FileCollection";
        string resultsFilePath = "results.txt";

        // Create directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"Directory '{directoryPath}' created.");
        }

        // Initialize counters and variables
        int xlsxCount = 0, docxCount = 0, pptxCount = 0;
        long xlsxSize = 0, docxSize = 0, pptxSize = 0;
        int totalCount = 0;
        long totalSize = 0;

        // Directory info object to access files
        DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

        // Enumerate files
        foreach (FileInfo fileInfo in directoryInfo.GetFiles())
        {
            if (IsOfficeFile(fileInfo.Extension))
            {
                totalCount++;
                totalSize += fileInfo.Length;

                if (fileInfo.Extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    xlsxCount++;
                    xlsxSize += fileInfo.Length;
                }
                else if (fileInfo.Extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    docxCount++;
                    docxSize += fileInfo.Length;
                }
                else if (fileInfo.Extension.Equals(".pptx", StringComparison.OrdinalIgnoreCase))
                {
                    pptxCount++;
                    pptxSize += fileInfo.Length;
                }
            }
        }

        // Write results to file
        using (StreamWriter writer = new StreamWriter(resultsFilePath))
        {
            writer.WriteLine("Summary of Microsoft Office Files:");
            writer.WriteLine();
            writer.WriteLine($"Excel Files (.xlsx): {xlsxCount} files, Total Size: {FormatSize(xlsxSize)}");
            writer.WriteLine($"Word Files (.docx): {docxCount} files, Total Size: {FormatSize(docxSize)}");
            writer.WriteLine($"PowerPoint Files (.pptx): {pptxCount} files, Total Size: {FormatSize(pptxSize)}");
            writer.WriteLine();
            writer.WriteLine($"Total Office Files: {totalCount} files, Total Size: {FormatSize(totalSize)}");
        }

        Console.WriteLine("Summary generated and saved to results.txt.");
    }

    static bool IsOfficeFile(string fileName)
    {
        string ext = Path.GetExtension(fileName).ToLower();
        return ext == ".xlsx" || ext == ".docx" || ext == ".pptx";
    }

    static string FormatSize(long size)
    {
        const int kb = 1024;
        const int mb = kb * 1024;
        const int gb = mb * 1024;

        if (size >= gb)
            return $"{(double)size / gb:F2} GB";
        if (size >= mb)
            return $"{(double)size / mb:F2} MB";
        if (size >= kb)
            return $"{(double)size / kb:F2} KB";

        return $"{size} bytes";
    }
}
