using System;

public class FileView
{
    public string GetUserParagraph()
    {
        Console.WriteLine("Enter your paragraph (press ENTER twice to finish):");
        string inputLine, fullText = "";
        while (true)
        {
            inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
                break;
            fullText += inputLine + Environment.NewLine;
        }
        return fullText.TrimEnd();
    }

    public int GetCharactersPerSplit()
    {
        Console.Write("\nEnter the number of characters per split file: ");
        return int.Parse(Console.ReadLine());
    }

    public void ShowWriteSuccess(string fileName)
    {
        Console.WriteLine($"\nText successfully written to '{fileName}'");
    }

    public void ShowSplitSuccess(int parts)
    {
        Console.WriteLine($"\nParagraph successfully split into {parts} files!");
    }

    public void DisplayMenu()
    {
        Console.WriteLine("\nOptions:");
        Console.WriteLine("1 - View a split file");
        Console.WriteLine("2 - Merge all files into 'output.txt'");
        Console.WriteLine("3 - Compare input.txt and output.txt");
        Console.WriteLine("4 - Delete all split files");
        Console.WriteLine("5- Exit");
        Console.Write("Your choice: ");
    }
    public void ShowDeletionCompleted()
    {
        Console.WriteLine("Deletion completed.");
    }

    public string GetMenuChoice()
    {
        return Console.ReadLine();
    }

    public int GetFileIndex(int totalParts)
    {
        Console.Write($"Enter file number (1 to {totalParts}): ");
        return int.Parse(Console.ReadLine());
    }

    public void ShowFileContent(string fileLabel, string content)
    {
        Console.WriteLine($"\n--- Content of {fileLabel} ---");
        Console.WriteLine(content);
        Console.WriteLine("--- End of file ---");
    }

    public void ShowInvalidFileNumber()
    {
        Console.WriteLine("Invalid file number!");
    }

    public void ShowMergeSuccess()
    {
        Console.WriteLine("\nMerged output written to 'output.txt' successfully!");
    }

    public string GetComparisonChoice()
    {
        Console.Write("Do you want to compare the input and output files? (yes/no): ");
        return Console.ReadLine()?.Trim().ToLower();
    }

    public void ShowCompareResult(bool success)
    {
        Console.WriteLine(success ? "\nSUCCESS" : "\nFAILURE");
    }

    public void ShowComparisonSkipped()
    {
        Console.WriteLine("Comparison skipped.");
    }

    public void ShowFileMissing()
    {
        Console.WriteLine("Either input.txt or output.txt does not exist. Please merge first.");
    }

    public void ShowInvalidChoice()
    {
        Console.WriteLine("Invalid choice. Please try again.");
    }

    public void ShowExitMessage()
    {
        Console.WriteLine("Program terminated.");
    }
}
