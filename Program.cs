using System;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the original text:");
        string originalString = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter the modified text:");
        string modifiedString = Console.ReadLine() ?? string.Empty;

        // instance of SideBySideDiffBuilder class to watch difference between two strings
        SideBySideDiffBuilder diffBuilder = new SideBySideDiffBuilder(new Differ());
        SideBySideDiffModel diffModel = diffBuilder.BuildDiffModel(originalString, modifiedString);

        Console.WriteLine("\nOriginal text:");
        foreach (var line in diffModel.OldText.Lines)
        {
            Console.WriteLine(EscapeText(line.Text));
        }

        Console.WriteLine("\nModified text:");
        foreach (var line in diffModel.NewText.Lines)
        {
            if (line.Type == ChangeType.Inserted || line.Type == ChangeType.Modified)
            {
                foreach (var word in line.SubPieces)
                {
                    if (word.Type == ChangeType.Inserted)
                    {
                        Console.Write("\u001b[32m" + EscapeText(word.Text) + "\u001b[0m"); // Green text
                    }
                    else
                    {
                        Console.Write(EscapeText(word.Text));
                    }
                }
            }
            else
            {
                Console.WriteLine(EscapeText(line.Text));
            }
        }
    }

    private static string EscapeText(string text)
    {
        if (text == null)
        {
            return string.Empty;
        }
        return text.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
    }
}
