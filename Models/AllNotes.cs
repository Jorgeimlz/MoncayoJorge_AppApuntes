using MoncayoJorge_AppApuntes.Views;
using System.Collections.ObjectModel;

namespace MoncayoJorge_AppApuntes.Models;

internal class AllNotes
{
    public ObservableCollection<NoteJorge> Notes { get; set; } = new ObservableCollection<NoteJorge>();

    public AllNotes() =>
        LoadNotes();

    public void LoadNotes()
    {
        Notes.Clear();

        // Get the folder where the notes are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.notes.txt files.
        IEnumerable<NoteJorge> notes = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.notes.txt")

                                    // Each file name is used to create a new Note
                                    .Select(filename => new NoteJorge()
                                    {
                                        Filename = filename,
                                        Text = File.ReadAllText(filename),
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of notes, order them by date
                                    .OrderBy(note => note.Date);

        // Add each note into the ObservableCollection
        foreach (NoteJorge note in notes)
            Notes.Add(note);
    }
}