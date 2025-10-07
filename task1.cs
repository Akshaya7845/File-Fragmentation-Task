using System;
using System.IO;

class FileFragmentation
{
    static void Main()
    {
        Console.WriteLine("Enter your paragraph (press ENTER twice to finish):");

        string inputLine;
        string fullTexts = "";

        while (true)
        {
            inputLine = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputLine))
                break;

            fullTexts += inputLine + Environment.NewLine;
        }
         string fullText=fullTexts.TrimEnd();

        string inputFile = "input.txt";
        File.WriteAllText(inputFile, fullText);
        Console.WriteLine($"\nText successfully written to '{inputFile}'");

        Console.Write("\nEnter the number of characters per split file: ");
        int charsPerSplit = int.Parse(Console.ReadLine());

        int numberOfParts = (fullText.Length + charsPerSplit - 1) / charsPerSplit;
        int numberLength = numberOfParts < 10 ? 1 : (numberOfParts < 100 ? 2 : 3);

        // Step 1: Remove old split files (01.txt to 999.txt)
        foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt"))
        {
            string name = Path.GetFileName(file);
            if (name != "input.txt" && name != "output.txt")
            {
                File.Delete(file);
            }
        }

        // Step 2: Write new split files
        for (int i = 0; i < numberOfParts; i++)
        {
            int index = i * charsPerSplit;
            int length = Math.Min(charsPerSplit, fullText.Length - index);
            string splitPart = fullText.Substring(index, length);

            string fileLabel = numberLength == 3 ? $"{(i + 1).ToString("D3")}.txt" :
                               numberLength == 2 ? $"{(i + 1).ToString("D2")}.txt" :
                               $"{i + 1}.txt";

            File.WriteAllText(fileLabel, splitPart);
        }

        Console.WriteLine($"\nParagraph successfully split into {numberOfParts} files!");

        
        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1 - View a split file");
            Console.WriteLine("2 - Merge all files into 'output.txt'");
            Console.WriteLine("3 - Compare input.txt and output.txt");
            Console.WriteLine("4 - Exit");
            Console.Write("Your choice: ");
            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                Console.Write($"Enter file number (1 to {numberOfParts}): ");
                int fileIndex = int.Parse(Console.ReadLine());

                if (fileIndex >= 1 && fileIndex <= numberOfParts)
                {
                    string fileLabel = numberLength == 3 ? $"{fileIndex.ToString("D3")}.txt" :
                                       numberLength == 2 ? $"{fileIndex.ToString("D2")}.txt" :
                                       $"{fileIndex}.txt";

                    string fileText = File.ReadAllText(fileLabel);
                    Console.WriteLine($"\n--- Content of {fileLabel} ---");
                    Console.WriteLine(fileText);
                    Console.WriteLine("--- End of file ---");
                }
                else
                {
                    Console.WriteLine("Invalid file number!");
                }
            }
            else if (userInput == "2")
            {
                string finalOutput = "";
                for (int i = 0; i < numberOfParts; i++)
                {
                    string fileLabel = numberLength == 3 ? $"{(i + 1).ToString("D3")}.txt" :
                                       numberLength == 2 ? $"{(i + 1).ToString("D2")}.txt" :
                                       $"{i + 1}.txt";

                    finalOutput += File.ReadAllText(fileLabel);
                }

                string outputFile = "output.txt";
                File.WriteAllText(outputFile, finalOutput);
                Console.WriteLine("\nMerged output written to 'output.txt' successfully!");
            }
            else if (userInput == "3")
            {
                Console.Write("Do you want to compare the input and output files? (yes/no): ");
                string compareChoice = Console.ReadLine()?.Trim().ToLower();

                if (compareChoice == "yes")
                {
                    if (!File.Exists("input.txt") || !File.Exists("output.txt"))
                    {
                        Console.WriteLine("Either input.txt or output.txt does not exist. Please merge first.");
                    }
                    else
                    {
                        string originalContent = File.ReadAllText("input.txt");
                        string mergedContent = File.ReadAllText("output.txt");

                        if (originalContent == mergedContent)
                        {
                            Console.WriteLine("\nSUCCESS");
                        }
                        else
                        {
                            Console.WriteLine("\nFAILURE");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Comparison skipped.");
                }
            }

            else if (userInput == "4")
            {
                Console.WriteLine("Program terminated.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

    }
}
