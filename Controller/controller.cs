using System;
using System.Collections.Generic;

public class FileController
{
    private readonly FileModel model;
    private readonly FileView view;

    public FileController()
    {
        model = new FileModel();
        view = new FileView();
    }

    public void Run()
    {
        string fullText = view.GetUserParagraph();
        /*model.SaveInputText(fullText);
        view.ShowWriteSuccess(model.InputFileName);*/
        try
        {
            model.SaveInputText(fullText);
            view.ShowWriteSuccess(model.InputFileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving input file: {ex.Message}");
        }


        int charsPerSplit = view.GetCharactersPerSplit();

        int numberOfParts = (fullText.Length + charsPerSplit - 1) / charsPerSplit;
        int numberLength = numberOfParts < 10 ? 1 : (numberOfParts < 100 ? 2 : 3);

        model.DeleteOldSplitFiles();

        for (int i = 0; i < numberOfParts; i++)
        {
            int index = i * charsPerSplit;
            int length = Math.Min(charsPerSplit, fullText.Length - index);
            string part = fullText.Substring(index, length);
            string label = GetFileLabel(i + 1, numberLength);
            model.WriteSplitFile(label, part);
        }

        view.ShowSplitSuccess(numberOfParts);

        while (true)
        {
            view.DisplayMenu();
            string choice = view.GetMenuChoice();

            /*if (choice == "1")
            {
                int fileIndex = view.GetFileIndex(numberOfParts);
                if (fileIndex >= 1 && fileIndex <= numberOfParts)
                {
                    string label = GetFileLabel(fileIndex, numberLength);
                    string content = model.ReadFileContent(label);
                    view.ShowFileContent(label, content);
                }
                else
                {
                    view.ShowInvalidFileNumber();
                }
            }*/
            if (choice == "1")
            {
                int fileIndex = view.GetFileIndex(numberOfParts);
                if (fileIndex >= 1 && fileIndex <= numberOfParts)
                {
                    string label = GetFileLabel(fileIndex, numberLength);

                    try
                    {
                        string content = model.ReadFileContent(label);
                        view.ShowFileContent(label, content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading file: {ex.Message}");
                    }
                }
                else
                {
                    view.ShowInvalidFileNumber();
                }
            }

            else if (choice == "2")
            {
                List<string> labels = new List<string>();
                for (int i = 0; i < numberOfParts; i++)
                    labels.Add(GetFileLabel(i + 1, numberLength));

                model.MergeFiles(labels.ToArray());
                view.ShowMergeSuccess();
            }
            else if (choice == "3")
            {
                string compare = view.GetComparisonChoice();
                if (compare == "yes")
                {
                    if (!System.IO.File.Exists(model.InputFileName) || !System.IO.File.Exists(model.OutputFileName))
                    {
                        view.ShowFileMissing();
                    }
                    else
                    {
                        bool success = model.CompareInputAndOutput();
                        view.ShowCompareResult(success);
                    }
                }
                else
                {
                    view.ShowComparisonSkipped();
                }
            }
            else if (choice == "4")
            {
                model.DeleteOldSplitFiles();
                view.ShowDeletionCompleted();
            }
            else if (choice == "5")
            {
                view.ShowExitMessage();
                break;
            }

            else
            {
                view.ShowInvalidChoice();
            }
        }
    }

    private string GetFileLabel(int index, int numberLength)
    {
        return numberLength == 3 ? $"{index.ToString("D3")}.txt" :
               numberLength == 2 ? $"{index.ToString("D2")}.txt" :
               $"{index}.txt";
    }
}
