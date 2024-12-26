using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
using MusicApp.Data;
using MusicApp.SMTP;
using BCrypt.Net;
using System.Globalization;

DataContext _context = new DataContext();

void Line()
{
    Console.WriteLine("===========================================");
}

void Janitor()
{
    Console.Clear();
}

string baseRoute = @"C:\Users\kmami\OneDrive\Desktop\it\Backend Finals\Loggs";
string playlistRoute = @"playlist.txt";
string logRoute = @"music_system_log.txt";
string songPlayHistory = @"played-song-history.txt";


void AddArtist()
{
    Janitor();
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Blue;
    Line();
    Console.WriteLine("Type Artist Name:");
    string newArtistName = Console.ReadLine();
    if (newArtistName == "")
    {
        Janitor();
        Line();
        Console.WriteLine("Please Write Name!");
        Line();
    }
    else
    {
        Line();
        Console.WriteLine("Type Artist's Country:");
        string newArtistCountry = Console.ReadLine();

        if (string.IsNullOrEmpty(newArtistCountry))
        {
            Janitor();
            Line();
            Console.WriteLine("Please Write Country!");
            Line();
        }
        else
        {
            Line();
            Console.WriteLine("Type Artist's Genre:");
            string newArtistGenre = Console.ReadLine();

            if (string.IsNullOrEmpty(newArtistGenre))
            {
                Janitor();
                Line();
                Console.WriteLine("Please Write Genre!");
                Line();
            }
            else
            {
                Line();
                Console.WriteLine("Type Artist's Description:");
                string newArtistDescription = Console.ReadLine();

                if (string.IsNullOrEmpty(newArtistDescription))
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Write Description!");
                    Line();
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Nice! It's Time For More Specific Details!");

                    _context.Add(new Artist
                    {
                        Name = newArtistName,
                        Country = newArtistCountry,
                        Genre = newArtistGenre,
                        Description = newArtistDescription,
                    });
                    _context.SaveChanges();

                    string fullRoute = Path.Combine(baseRoute, logRoute);

                    using(StreamWriter wr = new StreamWriter(fullRoute, true))
                    {
                        wr.WriteLine("==========================");
                        wr.WriteLine($"[LOG] Artist {newArtistName} Is Added - {DateTime.Now}");
                        wr.WriteLine("==========================");
                    }

                    Console.WriteLine("Type Artist's Formation Year");
                    int newArtistYear;
                    bool isValidInput = int.TryParse(Console.ReadLine(), out newArtistYear);

                    if (!isValidInput)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter A Valid Number, Not Letters Or Symbols!");
                        Console.WriteLine("Valid Year Start At 1600 And Ends in 2024");
                        Line();
                    }
                    else if (newArtistYear >= 1600 && newArtistYear <= 2024)
                    {
                        Line();
                        Console.WriteLine("Type Artist's Website ");
                        string newArtistWebsite = Console.ReadLine();

                        if (newArtistWebsite == "")
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Website!");
                            Line();
                        }
                        else if (newArtistWebsite.Contains(".com") || newArtistWebsite.Contains(".net") || newArtistWebsite.Contains(".ge"))
                        {
                            Line();
                            Console.WriteLine("Is Artist Active? Y/N");
                            string newArtistActive = Console.ReadLine();

                            if (newArtistActive.ToLower() == "y")
                            {
                                Janitor();
                                Line();
                                bool ArtistAcive = true;
                                Console.WriteLine("That Was All!");
                                var newArtistId1 = _context.Artists.FirstOrDefault(a => a.Name == newArtistName);
                                var newArtistId = newArtistId1.Id;
                                _context.Add(new ArtistDetails
                                {
                                    ArtistId = newArtistId,
                                    FormationYear = newArtistYear,
                                    Website = newArtistWebsite,
                                    TotalAlbums = 0,
                                    IsActive = ArtistAcive,


                                });
                                _context.SaveChanges();

                                string fullRoute2 = Path.Combine(baseRoute, logRoute);

                                using (StreamWriter wr = new StreamWriter(fullRoute2, true))
                                {
                                    wr.WriteLine("==========================");
                                    wr.WriteLine($"[LOG] Artist {newArtistName} Details Are Filled - {DateTime.Now}");
                                    wr.WriteLine("==========================");
                                }
                                Console.WriteLine("New Artist Is Added!");
                            }
                            else if (newArtistActive.ToLower() == "n")
                            {
                                Line();
                                bool ArtistAcive = false;
                                Console.WriteLine("That Was All!");
                                var newArtistId1 = _context.Artists.FirstOrDefault(a => a.Name == newArtistName);
                                var newArtistId = newArtistId1.Id;
                                _context.Add(new ArtistDetails
                                {
                                    ArtistId = newArtistId,
                                    FormationYear = newArtistYear,
                                    Website = newArtistWebsite,
                                    TotalAlbums = 0,
                                    IsActive = ArtistAcive,
                                });
                                _context.SaveChanges();

                                string fullRoute3 = Path.Combine(baseRoute, logRoute);

                                using (StreamWriter wr = new StreamWriter(fullRoute3, true))
                                {
                                    wr.WriteLine("==========================");
                                    wr.WriteLine($"[LOG] Artist {newArtistName} Details Are Filled - {DateTime.Now}");
                                    wr.WriteLine("==========================");
                                }

                                Console.WriteLine("New Artist Is Added!"); //nooooooooo dvisho ar washalot. dvisho is good mkodavi
                            }
                            else
                            {
                                Janitor();
                                Line();
                                Console.WriteLine("Please Type Valid Answer Y or N!");
                                Line();
                            }
                        }
                        else
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Website! It Should End With: .com, .net, .ge");
                            Line();
                        }
                    }
                    else
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("That Year Is Not Valid!");
                        Console.WriteLine("Valid Year Start At 1600 And Ends in 2024");
                        Line();
                    }
                }
            }
        }
    }


}

void SeeAllArtist()
{
    Janitor();
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Red;
    Line();
    Console.WriteLine("Which Order Should I Filter All Artist");
    Console.WriteLine("1. Based On Name (Alphabet)");
    Console.WriteLine("2. Based On Name Reversed (Reversed Alphabet)");
    Console.WriteLine("3. Based On Country (Alphabet)");
    Console.WriteLine("4. Based On Country Reversed (Reversed Alphabet)");
    Console.WriteLine("5. Based On Genre (Alphabet)");
    Console.WriteLine("6. Based On Genre Reversed (Reversed Alphabet)");
    Console.WriteLine("7. Based On Formation Year (Year Ascending)");
    Console.WriteLine("8. Based On Formation Year (Year Descending)");
    Console.WriteLine("9. Based On Website (Alphabet)");
    Console.WriteLine("10. Based On Website Reversed (Reversed Alphabet)");
    Console.WriteLine("11. Based On Total Album Count (Number Ascending)");
    Console.WriteLine("12. Based On Total Album Count (Number Descending)");
    Console.WriteLine("13. Based On Artist Activity (Active First)");
    Console.WriteLine("14. Based On Artist Activity  (Not Active First)");

    Console.WriteLine("Type It Down!");
    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter A Valid Number, Not Letters Or Symbols!");
        Console.WriteLine("Valid Number Start At 1 And Ends in 14");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.Name)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");
                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 2)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.Name)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }

    else if (choice == 3)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.Country)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }

    else if (choice == 4)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.Country)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 5)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.Genre)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 6)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.Genre)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 7)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.ArtistDetails.FormationYear)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 8)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.ArtistDetails.FormationYear)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 9)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.ArtistDetails.Website)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 10)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.ArtistDetails.Website)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 11)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.ArtistDetails.TotalAlbums)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 12)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.ArtistDetails.TotalAlbums)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 14)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderBy(a => a.ArtistDetails.IsActive)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else if (choice == 13)
    {
        Janitor();
        var allArtists = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .OrderByDescending(a => a.ArtistDetails.IsActive)
                             .ToList();

        foreach (var artist in allArtists)
        {
            Line();
            Console.WriteLine($"Artist's Name -> {artist.Name}");
            Console.WriteLine($"Artist's Country -> {artist.Country}");
            Console.WriteLine($"Artist's Genre -> {artist.Genre}");
            Console.WriteLine($"Artist's Description -> {artist.Description}");

            if (artist.ArtistDetails != null)
            {
                Console.WriteLine($"Artist's Formation Year -> {artist.ArtistDetails.FormationYear}");
                Console.WriteLine($"Artist's Website -> {artist.ArtistDetails.Website}");
                Console.WriteLine($"Artist's Total Albums -> {artist.ArtistDetails.TotalAlbums}");
                Console.WriteLine($"Artist's Active -> {artist.ArtistDetails.IsActive}");

                Line();
            }
            else
            {
                Console.WriteLine("Artist Don't Have Full Details Yet!");
                Line();
            }
        }
    }
    else
    {
        Line();
        Console.WriteLine("Please Enter A Valid Number!");
        Console.WriteLine("Valid Number Start At 1 And Ends in 14");
        Line();
    }
}

void ChangeArtist()
{
    Console.BackgroundColor = ConsoleColor.Cyan;
    Console.ForegroundColor = ConsoleColor.Magenta;
    Janitor();
    Line();
    Console.WriteLine("Here Are ID Of Artists:");
    var allArtistId = _context.Artists
                              .Include(a => a.ArtistDetails)
                              .OrderBy(a => a.Id)
                              .ToList();
    foreach (var artist in allArtistId)
    {
        Line();
        Console.WriteLine($"Artist Name -> {artist.Name}");
        Console.WriteLine($"Artist Name -> {artist.Id}");
        Line();
    }
    Console.WriteLine("Choose And Type ID Of Artists That You Want To Change");
    int chooseId;
    bool validChooseId = int.TryParse(Console.ReadLine(), out chooseId);

    var artistToChange = _context.Artists
                                 .Include(a => a.ArtistDetails)
                                 .FirstOrDefault(a => a.Id == chooseId);

    if (!validChooseId)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter A Valid ID, not letters or symbols!");
        Line();
    }
    else if (artistToChange != null)
    {
        Janitor();
        Line();
        Console.WriteLine("What You Want To Change?");
        Console.WriteLine("1. Artist's Name");
        Console.WriteLine("2. Artist's Counrty");
        Console.WriteLine("3. Artist's Genre");
        Console.WriteLine("4. Artist's Description");
        Console.WriteLine("5. Artist's FormationYear");
        Console.WriteLine("6. Artist's Website");
        Console.WriteLine("7. Artist's Activity Status");
        Console.WriteLine("Type Down What To Change");
        int changingAtribute;
        bool validchangingAtribute = int.TryParse(Console.ReadLine(), out changingAtribute);

        if (!validchangingAtribute)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter A Valid Number, not letters or symbols!");
            Console.WriteLine("Valid Number Is Between 1 And 7!");
            Line();
        }
        else if (changingAtribute == 1)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Name Is -> {artistToChange.Name}");
            Console.WriteLine("Type Change Name");
            string newName = Console.ReadLine();
            Line();
            if (newName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Name!");
                Line();
            }
            else
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Name Updated To {newName} - {DateTime.Now}");
                }
                artistToChange.Name = newName;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Name Is -> {artistToChange.Name}");

                

                Line();
            }
        }
        else if (changingAtribute == 2)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Counrty Is -> {artistToChange.Country}");
            Console.WriteLine("Type Country");
            string newName = Console.ReadLine();
            Line();
            if (newName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Country!");
                Line();
            }
            else
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Country Updated To {newName} - {DateTime.Now}");
                }
                artistToChange.Country = newName;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Country Is -> {artistToChange.Country}");
     
                Line();
            }
        }
        else if (changingAtribute == 3)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Genre Is -> {artistToChange.Genre}");
            Console.WriteLine("Type Genre");
            string newName = Console.ReadLine();
            Line();
            if (newName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Genre!");
                Line();
            }
            else
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Genre Updated To {newName} - {DateTime.Now}");
                }

                artistToChange.Genre = newName;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Genre Is -> {artistToChange.Genre}");
                Line();
            }
        }
        else if (changingAtribute == 4)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Description Is -> {artistToChange.Description}");
            Console.WriteLine("Type Description");
            string newName = Console.ReadLine();
            Line();
            if (newName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Description!");
                Line();
            }
            else
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Description Updated To {newName} - {DateTime.Now}");
                }
                artistToChange.Description = newName;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Description Is -> {artistToChange.Description}");
                Line();
            }
        }
        else if (changingAtribute == 5)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Formation Year Is -> {artistToChange.ArtistDetails.FormationYear}");
            Console.WriteLine("Type Formation Year");
            int newYear;
            bool validNewYear = int.TryParse(Console.ReadLine(), out newYear);
            Line();
            if (!validNewYear)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Year, No Symbols Or Letters!");
                Console.WriteLine("Valid Year Is Between 1600 and 2024");
                Line();
            }
            else if (newYear >= 1600 && newYear <= 2024)
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Formation Year Updated To {newYear} - {DateTime.Now}");
                }
                artistToChange.ArtistDetails.FormationYear = newYear;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Formation Year Is -> {artistToChange.ArtistDetails.FormationYear}");
                Line();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Year!");
                Console.WriteLine("Valid Year Is Between 1600 and 2024");
                Line();
            }
        }
        else if (changingAtribute == 6)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Website Is -> {artistToChange.ArtistDetails.Website}");
            Console.WriteLine("Type Website");
            string newName = Console.ReadLine();
            Line();
            if (newName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Website!");
                Line();
            }
            else if (newName.Contains(".com") || newName.Contains(".net") || newName.Contains(".ge"))
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Website Updated To {newName} - {DateTime.Now}");
                }
                artistToChange.ArtistDetails.Website = newName;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Website Is -> {artistToChange.ArtistDetails.Website}");
                Line();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Website!");
                Console.WriteLine("Valid Website Should End With .com, .net or .ge!");
                Line();
            }
        }
        else if (changingAtribute == 7)
        {
            Janitor();
            Console.WriteLine($"Actual Artist Acivity Status Is -> {artistToChange.ArtistDetails.IsActive}");
            Console.WriteLine("Is Artist Active? Y/N");
            string newName = Console.ReadLine();
            Line();
            if (newName.ToLower() == "y")
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Activity Updated To Active - {DateTime.Now}");
                }
                artistToChange.ArtistDetails.IsActive = true;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Activity Status Is -> {artistToChange.ArtistDetails.IsActive}");
                Line();
            }
            else if (newName.ToLower() == "n")
            {
                Console.WriteLine("Saving Changes!");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Artist {artistToChange.Name} Activity Updated To Not Active - {DateTime.Now}");
                }
                artistToChange.ArtistDetails.IsActive = false;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"New Artist's Activity Status Is -> {artistToChange.ArtistDetails.IsActive}");
                Line();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Answer! Y/N!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter A Valid Number!");
            Console.WriteLine("Valid Number Is Between 1 And 7!");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter A Valid ID!");
        Line();
    }

}

void RemoveArtist()
{
    Console.BackgroundColor = ConsoleColor.Green;
    Console.ForegroundColor = ConsoleColor.Black;
    Janitor();
    Line();
    Console.WriteLine("Here Are List Of Artists:");
    var allArtist = _context.Artists
                            .Include(a => a.ArtistDetails)
                            .ToList();
    foreach (var artist in allArtist)
    {
        Line();
        Console.WriteLine($"Artist Name -> {artist.Name}");
        Console.WriteLine($"Artist Name -> {artist.Id}");
        Line();
    }
    Console.WriteLine("Type Down Artist ID To Remove It Permanently");
    int removeChoice;
    bool validRemoveChoice = int.TryParse(Console.ReadLine(), out removeChoice);
    var artistToRemove = _context.Artists
                             .Include(a => a.ArtistDetails)
                             .Include(a => a.Albums) 
                             .ThenInclude(album => album.songs) 
                             .FirstOrDefault(a => a.Id == removeChoice);
    if (!validRemoveChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Letters Or Symbols!");
        Line();
    }
    else if (artistToRemove != null)
    {
        Line();
        Console.WriteLine("Are You Sure? Y/N");
        string answer = Console.ReadLine();
        if (answer.ToLower() == "y")
        {
            Console.WriteLine("Removing Artist...");
            string fullRoute = Path.Combine(baseRoute, logRoute);

            using (StreamWriter wr = new StreamWriter(fullRoute, true))
            {
                wr.WriteLine("==========================");
                wr.WriteLine($"[LOG] Artist {artistToRemove.Name} Removed - {DateTime.Now}");
                foreach(var album in artistToRemove.Albums)
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Album {album.Title} Removed - {DateTime.Now}");    
                    foreach(var song in album.songs) 
                    {
                        wr.WriteLine("==========================");
                        wr.WriteLine($"[LOG] Song {song.Title} Removed - {DateTime.Now}");                       
                    }
                }
            }
            _context.Artists.Remove(artistToRemove);
            _context.SaveChanges();
            Line();
            Console.WriteLine("Artist Removed Succesfully!");
        }
        else if (answer.ToLower() == "n")
        {
            Line();
            Console.WriteLine("Artist Is Not Removed!");
        }
        else
        {
            Line();
            Console.WriteLine("Please Enter Valid Answer! Y or N!");
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter A Valid ID!");
        Line();
    }
}

void AddAlbum()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Green;
    Janitor();
    Line();
    Console.WriteLine("Here Are List Of Artists:");
    var allArtist = _context.Artists
                            .Include(a => a.ArtistDetails)
                            .ToList();
    foreach (var artist in allArtist)
    {
        Line();
        Console.WriteLine($"Artist Name -> {artist.Name}");
        Console.WriteLine($"Artist ID -> {artist.Id}");
        Line();
    }
    Console.WriteLine("Type Down Artist ID To Add It's Album");
    int artistAlbumToAdd;
    bool validArtistAlbumToAdd = int.TryParse(Console.ReadLine(), out artistAlbumToAdd);

    var AlbumToAdd = _context.Artists
                             .Include(a => a.Albums)
                             .FirstOrDefault(a => a.Id == artistAlbumToAdd);

    if (!validArtistAlbumToAdd)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Letters Or Symbols!");
        Line();
    }
    else if (AlbumToAdd != null)
    {
        Janitor();
        Line();
        Console.WriteLine($"Creating Album For {AlbumToAdd.Name}");
        Console.WriteLine("Type Album Title");
        string newName = Console.ReadLine();

        if (newName == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Name!");
            Line();
        }
        else
        {
            Line();
            Console.WriteLine("Type Album Release Year");
            int newReleaseYear;
            bool validNewReleaseYear = int.TryParse(Console.ReadLine(), out newReleaseYear);

            if (!validNewReleaseYear)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Year, No Symbols Or Letters!");
                Console.WriteLine($"Valid Year Is Between {AlbumToAdd.ArtistDetails.FormationYear} To 2024");
                Line();
            }
            else if (newReleaseYear >= AlbumToAdd.ArtistDetails.FormationYear && newReleaseYear <= 2024)
            {
                Line();
                Console.WriteLine("Type Album Genre");
                string newGenre = Console.ReadLine();

                if (newGenre == "")
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Genre!");
                    Line();
                }
                else
                {
                    Line();
                    Console.WriteLine("Time To Rate Album!");
                    Console.WriteLine("Enter Participants Number, How Many People Are With You?");
                    decimal newScore = 0;
                    int newParticipants;
                    bool validNewParticipants = int.TryParse(Console.ReadLine(), out newParticipants);
                    if (!validNewParticipants)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Valid Number, No Symbols Or Letters!");
                        Line();
                    }
                    else if (newParticipants > 0)
                    {
                        for (int i = 1; i <= newParticipants; i++)
                        {
                            Janitor();
                            Console.WriteLine($"Rate Participant Number {i}! Rate From 0.0 To 5.0");
                            decimal newPartScore;
                            bool validNewPartScore = decimal.TryParse(Console.ReadLine(), out newPartScore);

                            if (!validNewPartScore)
                            {
                                Line();
                                Console.WriteLine("Invalid Score, Do Not Use Symbols Or Numbers!");
                                Line();
                                i--;
                                continue;
                            }
                            else if (newPartScore >= 0.0m && newPartScore <= 5.0m)
                            {
                                Line();
                                newScore = newScore + newPartScore;
                                Console.WriteLine("Your Score Is Saved!");

                            }
                            else
                            {
                                Line();
                                Console.WriteLine("Invalid Score!");
                                Line();
                                i--;
                                continue;
                            }
                        }

                        Janitor();
                        Console.WriteLine("Now Calculating Score!");
                        decimal finalScore = newScore / newParticipants;
                        Console.WriteLine($"Album -> {newName} have -> {finalScore} Rating!");

                        Line();
                        Console.WriteLine("Saving Album!");
                        _context.Add(new Album
                        {
                            ArtistId = AlbumToAdd.Id,
                            Title = newName,
                            ReleaseYear = newReleaseYear,
                            Genre = newGenre,
                            Rating = finalScore,
                            RatingParticipant = newParticipants,
                            OverAllScore = newScore,
                            AlbumLength = TimeSpan.Zero,
                        });
                        AlbumToAdd.ArtistDetails.TotalAlbums = AlbumToAdd.ArtistDetails.TotalAlbums + 1;
                        _context.SaveChanges();

                        string fullRoute = Path.Combine(baseRoute, logRoute);

                        using (StreamWriter wr = new StreamWriter(fullRoute, true))
                        {
                            wr.WriteLine("==========================");
                            wr.WriteLine($"[LOG] Artist {AlbumToAdd.Name} Album {newName} Has Added - {DateTime.Now}");
                        }

                        Line();
                        Console.WriteLine("Album Saved!");
                        Line();
                    }
                    else
                    {
                        Line();
                        Console.WriteLine("Invalid Number!");
                        Line();
                    }
                }
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Year!");
                Console.WriteLine($"Valid Year Is Between {AlbumToAdd.ArtistDetails.FormationYear} To 2024");
                Line();
            }
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID!");
        Line();
    }
}

void ChangeAlbum()
{
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.Black;
    Janitor();
    Line();
    Console.WriteLine("Here Are List Of Albums:");
    var allAlbum = _context.Albums
                           .ToList();
    foreach (var album in allAlbum)
    {
        Line();
        Console.WriteLine($"Album Title -> {album.Title}");
        Console.WriteLine($"Album ID -> {album.Id}");
        Line();
    }
    Console.WriteLine("Type Down Album ID To Change");
    int chooseAlbum;
    bool validChooseAlbum = int.TryParse(Console.ReadLine(), out chooseAlbum);

    var AlbumToChange = _context.Albums
                                .FirstOrDefault(a => a.Id == chooseAlbum);
    if (!validChooseAlbum)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters");
        Line();
    }
    else if (AlbumToChange != null)
    {
        Janitor();
        Console.WriteLine("Which One Do You Want To Change?");
        Console.WriteLine("1. Album's Title");
        Console.WriteLine("2. Album's Release Year");
        Console.WriteLine("3. Album's Genre");
        int changeChoise;
        bool validChangeChoise = int.TryParse(Console.ReadLine(), out changeChoise);

        if (!validChangeChoise)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters");
            Line();
        }
        else if (changeChoise == 1)
        {
            Janitor();
            Line();
            Console.WriteLine($"Actual Album Title Is -> {AlbumToChange.Title}");
            Console.WriteLine("Enter New Title For Album");
            string newTitle = Console.ReadLine();

            if (newTitle == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Title!");
                Line();
            }
            else
            {
                Line();
                Console.WriteLine("Updating New Title...");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Album {AlbumToChange.Title} Title Has Changed to {newTitle} - {DateTime.Now}");
                }
                AlbumToChange.Title = newTitle;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"Saved! New Album Title Is -> {AlbumToChange.Title}");
            }
        }
        else if (changeChoise == 2)
        {
            Janitor();
            Line();
            Console.WriteLine($"Actual Album Release Year Is -> {AlbumToChange.ReleaseYear}");
            Console.WriteLine("Enter New Release Year For Album");
            int newTitle;
            bool validNewTitle = int.TryParse(Console.ReadLine(), out newTitle);

            if (!validNewTitle)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Release Year, Don't Use Symbols And Letters!");
                Line();
            }
            else if (newTitle >= 1600 && newTitle <= 2024)
            {
                Console.WriteLine("Updating New Release Year...");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Album {AlbumToChange.Title} Release Year Has Changed to {newTitle} - {DateTime.Now}");
                }
                AlbumToChange.ReleaseYear = newTitle;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"Saved! New Album Release Year Is -> {AlbumToChange.ReleaseYear}");
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Release Year!");
                Console.WriteLine("Valid Year Is Between 1600 And 2024");
                Line();
            }
        }
        else if (changeChoise == 3)
        {
            Janitor();
            Line();
            Console.WriteLine($"Actual Album Genre Is -> {AlbumToChange.Genre}");
            Console.WriteLine("Enter New Genre For Album");
            string newTitle = Console.ReadLine();

            if (newTitle == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Genre!");
                Line();
            }
            else
            {
                Line();
                Console.WriteLine("Updating New Genre...");
                string fullRoute = Path.Combine(baseRoute, logRoute);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Album {AlbumToChange.Title} Genre Has Changed to {newTitle} - {DateTime.Now}");
                }
                AlbumToChange.Genre = newTitle;
                _context.SaveChanges();
                Line();
                Console.WriteLine($"Saved! New Album Genre Is -> {AlbumToChange.Genre}");
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Number");
            Console.WriteLine("Valid Number Is Between 1 And 3");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID");
        Line();
    }
}

void RemoveAlbum()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Janitor();
    Line();
    Console.WriteLine("Here Are List Of Albums:");
    var allAlbum = _context.Albums
                           .ToList();
    foreach (var album in allAlbum)
    {
        Line();
        Console.WriteLine($"Album Title -> {album.Title}");
        Console.WriteLine($"Album ID -> {album.Id}");
        Line();
    }
    Console.WriteLine("Type Down Album ID To Remove");
    int chooseAlbum;
    bool validChooseAlbum = int.TryParse(Console.ReadLine(), out chooseAlbum);

    var AlbumToRemove = _context.Albums
                                .Include(a => a.songs)
                                .FirstOrDefault(a => a.Id == chooseAlbum);
    if (!validChooseAlbum)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols And Letters!");
        Line();
    }
    else if (AlbumToRemove != null)
    {
        Line();
        Console.WriteLine("Are You Sure? Y/N");
        string answer = Console.ReadLine();

        if (answer == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Answer! Enter Y or N");
            Line();
        }
        else if (answer.ToLower() == "y")
        {
            Janitor();
            Line();
            Console.WriteLine($"Removing {AlbumToRemove.Title} Album...");
            string fullRoute = Path.Combine(baseRoute, logRoute);

            using (StreamWriter wr = new StreamWriter(fullRoute, true))
            {                             
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Album {AlbumToRemove.Title} Removed - {DateTime.Now}");
                    foreach (var song in AlbumToRemove.songs)
                    {
                        wr.WriteLine("==========================");
                        wr.WriteLine($"[LOG] Song {song.Title} Removed - {DateTime.Now}");
                    }                
            }
            _context.Remove(AlbumToRemove);
            _context.SaveChanges();
            var minusAlbum = _context.Artists
                                     .Include(a => a.ArtistDetails)
                                     .FirstOrDefault(a => a.Id == AlbumToRemove.ArtistId);
            minusAlbum.ArtistDetails.TotalAlbums = minusAlbum.ArtistDetails.TotalAlbums - 1;
            _context.SaveChanges();
            Line();
            Console.WriteLine("Album Removed Successfully!");
        }
        else if (answer.ToLower() == "n")
        {
            Janitor();
            Line();
            Console.WriteLine("Album Removed Aborted!");
            Line();
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Answer! Enter Y or N");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID!");
        Line();
    }
}

void AllAlbums()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Janitor();
    Line();
    Console.WriteLine("Choose Filter To See All Albums!");
    Console.WriteLine("1. Based On Album's Title (Alphabet)");
    Console.WriteLine("2. Based On Album's Title Reversed (Reversed Alphabet)");
    Console.WriteLine("3. Based On Album's Release Year (Year Ascending)");
    Console.WriteLine("4. Based On Album's Release Year (Year Descending)");
    Console.WriteLine("5. Based On Album's Genre (Alphabet)");
    Console.WriteLine("6. Based On Album's Genre Reversed (Reversed Alphabet)");
    Console.WriteLine("7. Based On Album's Rating (Top Rating First)");
    Console.WriteLine("8. Based On Album's Rating (Low Rating First)");
    Console.WriteLine("9. Based On Album's Artist (Alphabet)");
    Console.WriteLine("10. Based On Album's Artist (Reversed Alphabet)");
    Console.WriteLine("11. Based On Album's Overall Length (Longest First)");
    Console.WriteLine("12. Based On Album's Overall Length (Shortest First)");
    Console.WriteLine("13. Based On Album's Song Count (Longest First)");
    Console.WriteLine("14. Based On Album's Song Count (Shortest First)");


    int choose;
    bool validChoose = int.TryParse(Console.ReadLine(), out choose);

    if (!validChoose)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Console.WriteLine("Valid Number Is Between 1 To 14!");
        Line();
    }
    else if (choose == 1)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.Title)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 2)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.Title)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 3)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.ReleaseYear)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 4)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.ReleaseYear)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 5)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.Genre)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 6)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.Genre)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 8)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.Rating)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 7)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.Rating)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 9)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.artist.Name)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 10)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.artist.Name)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 12)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.AlbumLength)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 11)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.AlbumLength)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 13)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderByDescending(a => a.songs.Count)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else if (choose == 14)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist).Include(a => a.songs)
                                .OrderBy(a => a.songs.Count)
                                .ToList();

        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.artist.Name}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Songs Count -> {album.songs.Count}");
            Line();
        }
        Line();
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Console.WriteLine("Valid Number Is Between 1 To 14!");
        Line();
    }
}

void ChangeRating()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Janitor();
    Console.WriteLine("Here Are All Albums:");
    var allAlbum = _context.Albums.ToList();
    foreach (var album in allAlbum)
    {
        Line();
        Console.WriteLine($"Album Title -> {album.Title}");
        Console.WriteLine($"Album ID -> {album.Id}");
        Line();
    }
    Console.WriteLine("Choose Album ID To Add Your Rating!");
    int chooseAlbum;
    bool validChooseAlbum = int.TryParse(Console.ReadLine(), out chooseAlbum);

    var choosenAlbum = _context.Albums
                               .FirstOrDefault(a => a.Id == chooseAlbum);

    if (!validChooseAlbum)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols OR Letters!");
        Line();
    }
    else if (choosenAlbum != null)
    {
        decimal newOverallScore = choosenAlbum.OverAllScore;
        Janitor();
        Line();
        Console.WriteLine("Enter How Many People Want To Add Their Rating");
        int newParticipants;
        bool validNewParticipants = int.TryParse(Console.ReadLine(), out newParticipants);

        if (!validNewParticipants)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID, Don't Use Symbols OR Letters!");
            Line();
        }
        else if (newParticipants > 0)
        {
            Janitor();
            Line();
            for (int i = 1; i <= newParticipants; i++)
            {
                Janitor();
                Line();
                Console.WriteLine($"Rate Number {i} Participant!");
                Console.WriteLine($"Rate Between 0.0 To 5.0!");
                decimal newPartScore;
                bool validNewPartScore = decimal.TryParse(Console.ReadLine(), out newPartScore);

                if (!validNewPartScore)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid Number, Don't Use Symbols OR Letters!");
                    Console.WriteLine("Valid Number Is Between 0.0 To 5.0");
                    Line();
                    i--;  
                    continue;
                }
                else if (newPartScore >= 0.0m && newPartScore <= 5.0m)
                {
                    newOverallScore = newOverallScore + newPartScore;
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid Number!");
                    Console.WriteLine("Valid Number Is Between 0.0 To 5.0");
                    Line();
                    i--;
                    continue;
                }
            }
            Janitor();
            Line();
            Console.WriteLine("Calculation New Overall Rating!");
            choosenAlbum.OverAllScore = newOverallScore;
            choosenAlbum.RatingParticipant = choosenAlbum.RatingParticipant + newParticipants;
            choosenAlbum.Rating = newOverallScore / choosenAlbum.RatingParticipant;
            _context.SaveChanges();
            string fullRoute = Path.Combine(baseRoute, logRoute);

            using (StreamWriter wr = new StreamWriter(fullRoute, true))
            {
                wr.WriteLine("==========================");
                wr.WriteLine($"[LOG] Album {choosenAlbum.Title} Rating Updated To {choosenAlbum.Rating} - {DateTime.Now}");
                
            }
            Line();
            Console.WriteLine($"Album -> {choosenAlbum.Title}, Has New Rating -> {choosenAlbum.Rating}!");
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID!");
            Line();
        }
        Line();
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID!");
        Line();
    }
}

void AddSong()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Janitor();
    Console.WriteLine("Here Are All Albums:");
    var allAlbums = _context.Albums
                            .Include(a => a.artist)
                            .ToList();

    foreach (var album in allAlbums)
    {
        Line();
        Console.WriteLine($"Album's Title -> {album.Title}");
        Console.WriteLine($"Album's Artist -> {album.artist.Name}");
        Console.WriteLine($"Album's ID -> {album.Id}");
        Line();
    }
    Line();
    Console.WriteLine("Type Down ID Of Album To Add Songs!");
    int chooseAlbum;
    bool validChooseAlbum = int.TryParse(Console.ReadLine(), out chooseAlbum);
    var choosenAlbum = _context.Albums.Include(a => a.songs)
                               .FirstOrDefault(a => a.Id == chooseAlbum);
    if (!validChooseAlbum)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (choosenAlbum != null)
    {
        Janitor();
        Line();
        Console.WriteLine("Enter Song Title!");
        string newTitle = Console.ReadLine();
        if (newTitle == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Title!");
            Line();
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Enter Song Duration! HH:MM:SS Format ");

            try
            {
                TimeSpan newDuration = TimeSpan.Parse(Console.ReadLine());

                if (newDuration.TotalSeconds <= 0)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Song Duration Must Be Greater Than Zero!");
                    Line();
                }
                else if (newDuration.TotalHours > 3)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Duration Is Unreasonably Long For A Song!");
                    Line();
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Enter Song Lyrics");
                    string newLyrics = Console.ReadLine();

                    if (newLyrics == "")
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Lyrics!");
                        Line();
                    }
                    else
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Enter Song Track Number");
                        int newTrackNumber;
                        bool validNewTrackNumber = int.TryParse(Console.ReadLine(), out newTrackNumber);
                        //int validSong = _context.Songs.FirstOrDefault(s => s.AlbumId == chooseAlbum).TrackNumber;

                        if (!validNewTrackNumber)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
                            Line();
                        }
                        //else if (newTrackNumber == validSong)
                        //{
                        //    Janitor();
                        //    Line();
                        //    Console.WriteLine("This Track Number Is Allready Used!");
                        //    Console.WriteLine("Please Try Another Number!");
                        //    Line();
                        //}
                        else if (newTrackNumber <= 0)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Number!");
                            Console.WriteLine("Try Number Above Zero!");
                            Line();
                        }
                        else
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Saving Song!");
                            _context.Add(new Song
                            {
                                AlbumId = chooseAlbum,
                                Title = newTitle,
                                Duration = newDuration,
                                Lyrics = newLyrics,
                                TrackNumber = newTrackNumber,
                                TimesPlayed = 0
                            });
                            choosenAlbum.AlbumLength = choosenAlbum.AlbumLength + newDuration;
                            _context.SaveChanges();

                            string fullRoute = Path.Combine(baseRoute, logRoute);

                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                            {
                                wr.WriteLine("==========================");
                                wr.WriteLine($"[LOG] Album {choosenAlbum.Title} Song {newTitle} Has Been Added  - {DateTime.Now}");

                            }
                            Line();
                        }
                    }
                }
            }
            catch (FormatException)
            {
                Janitor();
                Line();
                Console.WriteLine("Invalid Format! Please Use HH:MM:SS!");
                Line();
            }
            catch (OverflowException)
            {
                Janitor();
                Line();
                Console.WriteLine("The Duration Value Is Too Large!");
                Line();
            }
            catch (Exception ex)
            {
                Janitor();
                Line();
                Console.WriteLine($"An Error Occurred: {ex.Message}");
                Line();
            }


        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID!");
        Line();
    }
}

void ChangeSong()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Janitor();
    Console.WriteLine("Here Are All Songs:");
    var allSongs = _context.Songs
                            .Include(s => s.album)
                            .Include(s => s.album.artist)
                            .ToList();

    foreach (var Song in allSongs)
    {
        Line();
        Console.WriteLine($"Song Title -> {Song.Title}");
        Console.WriteLine($"Song's Album -> {Song.album.Title}");
        Console.WriteLine($"Song's Artist-> {Song.album.artist.Name}");
        Console.WriteLine($"Song's ID -> {Song.Id}");
        Line();
    }
    Line();
    Console.WriteLine("Type Down ID Of Song To Change It!");
    int chooseSong;
    bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

    var ChoosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

    if (!validChooseSong)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (ChoosenSong != null)
    {
        Janitor();
        Line();
        Console.WriteLine($"Actual Song Title Is -> {ChoosenSong.Title}");
        Console.WriteLine($"Enter New Title!");
        string newTitle = Console.ReadLine();

        if (newTitle == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Name!");
            Line();
        }
        else
        {
            TimeSpan actualDuration = ChoosenSong.Duration;
            Janitor();
            Line();
            Console.WriteLine($"Actual Song Duration Is -> {ChoosenSong.Duration}");
            Console.WriteLine("Enter New Song Duration! HH:MM:SS Format");

            try
            {
                TimeSpan newDuration = TimeSpan.Parse(Console.ReadLine());

                if (newDuration.TotalSeconds <= 0)
                {
                    Console.WriteLine("Song Duration Must Be Greater Than Zero!");
                }
                else if (newDuration.TotalHours > 3)
                {
                    Console.WriteLine("Duration Is Unreasonably Long For A Song!");
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine($"Actual Song Lyrics Is -> {ChoosenSong.Lyrics}");
                    Console.WriteLine("Enter New Lyrics!");
                    string newLyrics = Console.ReadLine();

                    if (newLyrics == "")
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Lyrics!");
                        Line();
                    }
                    else
                    {
                        Janitor();
                        Line();
                        Console.WriteLine($"Actual Song Track Number Is -> {ChoosenSong.TrackNumber}");
                        Console.WriteLine("Enter New Track Number");
                        int newTrackNumber;
                        bool validNewTrackNumber = int.TryParse(Console.ReadLine(), out newTrackNumber);

                        ChoosenSong.TrackNumber = 0;
                        var validSong = ChoosenSong.TrackNumber;

                        if (!validNewTrackNumber)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
                            Line();
                        }
                        else if (newTrackNumber == validSong)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("This Track Number Is Allready Used!");
                            Console.WriteLine("Please Try Another Number!");
                            Line();
                        }
                        else if (newTrackNumber <= 0)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Number!");
                            Console.WriteLine("Try Number Above Zero!");
                            Line();
                        }
                        else
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Saving Changes...");
                            string fullRoute = Path.Combine(baseRoute, logRoute);

                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                            {
                                wr.WriteLine("==========================");
                                wr.WriteLine($"[LOG] Song {ChoosenSong.Title} Title Has Been Change To {newTitle}  - {DateTime.Now}");

                            }

                            ChoosenSong.Title = newTitle;
                            ChoosenSong.Duration = newDuration;
                            ChoosenSong.Lyrics = newLyrics;
                            ChoosenSong.TrackNumber = newTrackNumber;

                            ChoosenSong.album.AlbumLength = (ChoosenSong.album.AlbumLength - actualDuration) + newDuration;
                            _context.SaveChanges();                            

                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                            {
                                wr.WriteLine("==========================");
                                wr.WriteLine($"[LOG] Song {ChoosenSong.Title} Duration Has Been Change To {newDuration}  - {DateTime.Now}");

                            }

                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                            {
                                wr.WriteLine("==========================");
                                wr.WriteLine($"[LOG] Song {ChoosenSong.Title} Lyrics Has Been Change To {newLyrics}  - {DateTime.Now}");

                            }

                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                            {
                                wr.WriteLine("==========================");
                                wr.WriteLine($"[LOG] Song {ChoosenSong.Title} TrackNumber Has Been Change To {newTrackNumber}  - {DateTime.Now}");

                            }

                            Line();
                            Console.WriteLine("Song Is Changed!");
                        }
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Format! Please Use HH:MM:SS!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The Duration Value Is Too Large!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occurred: {ex.Message}");
            }
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Line();
    }
}

void RemoveSong()
{
    Console.BackgroundColor = ConsoleColor.Gray;
    Console.ForegroundColor = ConsoleColor.Magenta;
    Janitor();
    Console.WriteLine("Here Are All Songs:");
    var allSongs = _context.Songs
                            .Include(s => s.album)
                            .Include(s => s.album.artist)
                            .ToList();

    foreach (var Song in allSongs)
    {
        Line();
        Console.WriteLine($"Song Title -> {Song.Title}");
        Console.WriteLine($"Song's Album -> {Song.album.Title}");
        Console.WriteLine($"Song's Artist-> {Song.album.artist.Name}");
        Console.WriteLine($"Song's ID -> {Song.Id}");
        Line();
    }
    Line();
    Console.WriteLine("Type Down ID Of Song To Remove It!");
    int chooseSong;
    bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

    var ChoosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

    if (!validChooseSong)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (ChoosenSong != null)
    {
        Janitor();
        Line();
        Console.WriteLine("Are You Sure? Y/N");
        string answer = Console.ReadLine();

        if (answer == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Answer!");
            Line();
        }
        else if (answer.ToLower() == "y")
        {
            Janitor();
            Line();
            Console.WriteLine("Removing Song...");
            string fullRoute = Path.Combine(baseRoute, logRoute);

            using (StreamWriter wr = new StreamWriter(fullRoute, true))
            {
                wr.WriteLine("==========================");
                wr.WriteLine($"[LOG] Song {ChoosenSong.Title} Has Been Removed - {DateTime.Now}");

            }
            _context.Albums.FirstOrDefault(a => a.Id == ChoosenSong.AlbumId).AlbumLength = _context.Albums.FirstOrDefault(a => a.Id == ChoosenSong.AlbumId).AlbumLength - ChoosenSong.Duration;
            _context.Remove(ChoosenSong);
            _context.SaveChanges();
            Line();
            Console.WriteLine("Song Removed!");
        }
        else if (answer.ToLower() == "n")
        {
            Janitor();
            Line();
            Console.WriteLine("Removing Song Aborted!");
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Answer! Enter Y Or N");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid ID!");
        Line();
    }
}

void AllSongs()
{
    Console.BackgroundColor = ConsoleColor.Cyan;
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Janitor();
    Line();
    Console.WriteLine("Choose Filter To See All Songs!");
    Console.WriteLine("1. Based On Song's Artist (Alphabet)");
    Console.WriteLine("2. Based On Song's Artist Reversed (Reversed Alphabet)");
    Console.WriteLine("3. Based On Song's Album (Alphabet)");
    Console.WriteLine("4. Based On Song's Album Reversed (Reversed Alphabet)");
    Console.WriteLine("5. Based On Song's Title (Alphabet)");
    Console.WriteLine("6. Based On Song's Title Reversed (Reversed Alphabet)");
    Console.WriteLine("7. Based On Song's Duration (Longest First)");
    Console.WriteLine("8. Based On Song's Duration (Shortest First)");
    Console.WriteLine("9. Based On Song's Playability (Highest First)");
    Console.WriteLine("10. Based On Song's Playability (Lowest First)");

    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Console.WriteLine("Valid Number Is Between 1 And 10");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderBy(s => s.album.artist.Name);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 2)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.album.artist.Name);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 3)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderBy(s => s.album.Title);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 4)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.album.Title);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 5)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderBy(s => s.Title);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 6)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.Title);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 7)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.Duration);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 8)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderBy(s => s.Duration);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 9)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.TimesPlayed);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else if (choice == 10)
    {
        Janitor();
        Line();
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderBy(s => s.TimesPlayed);

        foreach (var song in allSongs)
        {
            Line();
            Console.WriteLine($"Song Title -> {song.Title}");
            Console.WriteLine($"Song Album -> {song.album}");
            Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
            Console.WriteLine($"Song's Duration -> {song.Duration}");
            Console.WriteLine($"Song's Lyrics -> {song.Lyrics}");
            Console.WriteLine($"Song's Track Number -> {song.TrackNumber}");
            Console.WriteLine($"Song Total Played -> {song.TimesPlayed} Times!");
            Line();
        }
        Line();
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Console.WriteLine("Valid Number Is Between 1 And 10");
        Line();
    }
}

void PlaySong()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkRed;

    Janitor();
    Line();
    Console.WriteLine("Choose Song To Play!");
    Console.WriteLine("1. Choose Song Based On Artist");
    Console.WriteLine("2. Choose Song Based On Album");
    Console.WriteLine("3. Choose Song Based On All Song List");
    Console.WriteLine("4. Search Song");
    Console.WriteLine("5. Search Album");
    Console.WriteLine("6. Search Artist");

    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Console.WriteLine("Valid Number Is Between 1 And 6");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        Line();
        Console.WriteLine("Choose Artist From List!");

        var allArtist = _context.Artists.ToList();

        foreach (var artist in allArtist)
        {
            Line();
            Console.WriteLine($"Artist Name -> {artist.Name}");
            Console.WriteLine($"Artist Genre -> {artist.Genre}");
            Console.WriteLine($"Artist ID -> {artist.Id}");
            Line();
        }
        Console.WriteLine("Enter Artist's ID");
        int artistId;
        bool validArtistId = int.TryParse(Console.ReadLine(), out artistId);

        var choosenArtist = _context.Artists
                                    .Include(a => a.Albums)
                                    .FirstOrDefault(a => a.Id == artistId);

        if (!validArtistId)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
            Line();
        }
        else if (choosenArtist != null)
        {
            Janitor();
            Line();
            var artistAlbums = _context.Albums
                                       .Include(a => a.artist)
                                       .Include(a => a.songs)
                                       .Where(a => a.ArtistId == choosenArtist.Id)
                                       .ToList();

            Console.WriteLine($"{choosenArtist.Name} Albums:");
            foreach (var album in artistAlbums)
            {
                Line();
                Console.WriteLine($"Album ID -> {album.Id}");
                Console.WriteLine($"Album Title -> {album.Title}");
                Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
                Console.WriteLine($"Album Genre -> {album.Genre}");
                Console.WriteLine($"Album Duration -> {album.AlbumLength}");
                Console.WriteLine($"Album Song Count -> {album.songs.Count}");
                Console.WriteLine($"Album Rating -> {album.Rating}");
                Line();
            }
            Line();
            Console.WriteLine("Choose Album ID!");
            int chooseAlbumId;
            bool validChooseAlbumId = int.TryParse(Console.ReadLine(), out chooseAlbumId);

            var choosenAlbum = _context.Albums
                                       .Include(a => a.songs)
                                       .FirstOrDefault(a => a.Id == chooseAlbumId);

            if (!validChooseAlbumId)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                Line();
            }
            else if (choosenAlbum != null)
            {
                Janitor();
                Line();
                Console.WriteLine($"{choosenAlbum.Title} Songs:");
                Line();

                var albumSongs = _context.Songs
                                         .Include(s => s.album)
                                         .Include(s => s.album.artist)
                                         .Where(s => s.album.Id == choosenAlbum.Id)
                                         .ToList();

                foreach (var songs in albumSongs)
                {
                    Line();
                    Console.WriteLine($"Song ID -> {songs.Id}");
                    Console.WriteLine($"Song Title -> {songs.Title}");
                    Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
                    Console.WriteLine($"Song's Album -> {songs.album.Title}");
                    Console.WriteLine($"Song Duration -> {songs.Duration}");
                    Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
                    Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
                    Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
                    Line();
                }
                Console.WriteLine("Choose Song ID To Play!");

                int chooseSong;
                bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

                var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

                if (!validChooseSong)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                    Line();
                }
                else if (choosenSong != null)
                {
                    var theSong = choosenSong;
                    Song playingSong = new Song();
                    choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
                    _context.SaveChanges();

                    string fullRoute = Path.Combine(baseRoute, songPlayHistory);

                    using (StreamWriter wr = new StreamWriter(fullRoute, true))
                    {
                        wr.WriteLine("==========================");
                        wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

                    }

                    playingSong.PlaySongVisualization(theSong);
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID!");
                    Line();
                }
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID!");
            Line();
        }
    }
    else if (choice == 2)
    {
        Janitor();
        Line();
        var allAlbums = _context.Albums
                                .Include(a => a.artist)
                                .Include(a => a.songs)
                                .ToList();

        Console.WriteLine("All Albums:");
        foreach (var album in allAlbums)
        {
            Line();
            Console.WriteLine($"Album ID -> {album.Id}");
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Duration -> {album.AlbumLength}");
            Console.WriteLine($"Album Song Count -> {album.songs.Count}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Line();
        }
        Line();
        Console.WriteLine("Choose Album ID!");
        int chooseAlbumId;
        bool validChooseAlbumId = int.TryParse(Console.ReadLine(), out chooseAlbumId);

        var choosenAlbum = _context.Albums
                                   .Include(a => a.songs)
                                   .FirstOrDefault(a => a.Id == chooseAlbumId);

        if (!validChooseAlbumId)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
            Line();
        }
        else if (choosenAlbum != null)
        {
            Janitor();
            Line();
            Console.WriteLine($"{choosenAlbum.Title} Songs:");
            Line();

            var albumSongs = _context.Songs
                                     .Include(s => s.album)
                                     .Include(s => s.album.artist)
                                     .Where(s => s.album.Id == choosenAlbum.Id)
                                     .ToList();

            foreach (var songs in albumSongs)
            {
                Line();
                Console.WriteLine($"Song ID -> {songs.Id}");
                Console.WriteLine($"Song Title -> {songs.Title}");
                Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
                Console.WriteLine($"Song's Album -> {songs.album.Title}");
                Console.WriteLine($"Song Duration -> {songs.Duration}");
                Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
                Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
                Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
                Line();
            }
            Console.WriteLine("Choose Song ID To Play!");

            int chooseSong;
            bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

            var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

            if (!validChooseSong)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                Line();
            }
            else if (choosenSong != null)
            {
                var theSong = choosenSong;
                Song playingSong = new Song();
                choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
                _context.SaveChanges();

                string fullRoute = Path.Combine(baseRoute, songPlayHistory);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

                }

                playingSong.PlaySongVisualization(theSong);
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID!");
            Line();
        }
    }

    if (choice == 3)
    {
        Janitor();
        Line();
        Console.WriteLine($"All Songs:");
        Line();

        var albumSongs = _context.Songs
                                 .Include(s => s.album)
                                 .Include(s => s.album.artist)
                                 .ToList();

        foreach (var songs in albumSongs)
        {
            Line();
            Console.WriteLine($"Song ID -> {songs.Id}");
            Console.WriteLine($"Song Title -> {songs.Title}");
            Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
            Console.WriteLine($"Song's Album -> {songs.album.Title}");
            Console.WriteLine($"Song Duration -> {songs.Duration}");
            Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
            Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
            Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
            Line();
        }
        Console.WriteLine("Choose Song ID To Play!");

        int chooseSong;
        bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

        var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

        if (!validChooseSong)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
            Line();
        }
        else if (choosenSong != null)
        {
            var theSong = choosenSong;
            Song playingSong = new Song();
            choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
            _context.SaveChanges();

            string fullRoute = Path.Combine(baseRoute, songPlayHistory);

            using (StreamWriter wr = new StreamWriter(fullRoute, true))
            {
                wr.WriteLine("==========================");
                wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

            }

            playingSong.PlaySongVisualization(theSong);
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid ID!");
            Line();
        }
    }

    else if (choice == 4)
    {
        Janitor();
        Line();
        Console.WriteLine("Song Search...");
        Console.WriteLine("Enter Song Name");
        string songSearch = Console.ReadLine();

        var searchedSong = _context.Songs
                                   .Include(s => s.album)
                                   .Include(s => s.album.artist)
                                   .Where(s => s.Title.Contains(songSearch))
                                   .ToList();

        if (songSearch == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Song!");
            Line();
        }
        else if (searchedSong != null)
        {
            Janitor();
            Line();
            Console.WriteLine($"Here Are Results For: {songSearch}");

            foreach (var song in searchedSong)
            {
                Line();
                Console.WriteLine($"Song ID -> {song.Id}");
                Console.WriteLine($"Song Title -> {song.Title}");
                Console.WriteLine($"Song's Artist -> {song.album.artist.Name}");
                Console.WriteLine($"Song's Album -> {song.album.Title}");
                Console.WriteLine($"Song Duration -> {song.Duration}");
                Console.WriteLine($"Song Lyrics -> {song.Lyrics}");
                Console.WriteLine($"Song Track Number -> {song.TrackNumber}");
                Console.WriteLine($"Song Has Been Played -> {song.TimesPlayed} Times");
                Line();
            }
            Console.WriteLine("Choose Song ID To Play!");

            int chooseSong;
            bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

            var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

            if (!validChooseSong)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                Line();
            }
            else if (choosenSong != null)
            {
                var theSong = choosenSong;
                Song playingSong = new Song();
                choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
                _context.SaveChanges();

                string fullRoute = Path.Combine(baseRoute, songPlayHistory);

                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                {
                    wr.WriteLine("==========================");
                    wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

                }

                playingSong.PlaySongVisualization(theSong);
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine($"Sorry There Are No {songSearch} Song!");
            Line();
        }
    }

    else if (choice == 5)
    {
        Janitor();
        Line();
        Console.WriteLine("Album Search...");
        Console.WriteLine("Enter Album Name");
        string albumSearch = Console.ReadLine();

        var searchedAlbum = _context.Albums
                                   .Include(s => s.artist)
                                   .Include(s => s.songs)
                                   .Where(s => s.Title.Contains(albumSearch))
                                   .ToList();

        if (albumSearch == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Album!");
            Line();
        }
        else if (searchedAlbum != null)
        {
            Janitor();
            Line();
            Console.WriteLine($"Here Are Results For: {albumSearch}");

            foreach (var album in searchedAlbum)
            {
                Line();
                Console.WriteLine($"Album ID -> {album.Id}");
                Console.WriteLine($"Album Title -> {album.Title}");
                Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
                Console.WriteLine($"Album Genre -> {album.Genre}");
                Console.WriteLine($"Album Duration -> {album.AlbumLength}");
                Console.WriteLine($"Album Song Count -> {album.songs.Count}");
                Console.WriteLine($"Album Rating -> {album.Rating}");
                Line();
            }
            Console.WriteLine("Choose Album ID To Play!");
            int albumId;
            bool validAlbumId = int.TryParse(Console.ReadLine(), out albumId);

            var choosenAlbum = _context.Albums
                                       .Include(a => a.songs)
                                       .FirstOrDefault(a => a.Id == albumId);

            if (!validAlbumId)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                Line();
            }
            else if (choosenAlbum != null)
            {
                Janitor();
                Line();
                Console.WriteLine($"Album {choosenAlbum}' Songs:");
                var albumSongs = _context.Songs
                                     .Include(s => s.album)
                                     .Include(s => s.album.artist)
                                     .Where(s => s.album.Id == choosenAlbum.Id)
                                     .ToList();

                foreach (var songs in albumSongs)
                {
                    Line();
                    Console.WriteLine($"Song ID -> {songs.Id}");
                    Console.WriteLine($"Song Title -> {songs.Title}");
                    Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
                    Console.WriteLine($"Song's Album -> {songs.album.Title}");
                    Console.WriteLine($"Song Duration -> {songs.Duration}");
                    Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
                    Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
                    Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
                    Line();
                }
                Console.WriteLine("Choose Song ID To Play!");

                int chooseSong;
                bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

                var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

                if (!validChooseSong)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                    Line();
                }
                else if (choosenSong != null)
                {
                    var theSong = choosenSong;
                    Song playingSong = new Song();
                    choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
                    _context.SaveChanges();

                    string fullRoute = Path.Combine(baseRoute, songPlayHistory);

                    using (StreamWriter wr = new StreamWriter(fullRoute, true))
                    {
                        wr.WriteLine("==========================");
                        wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

                    }

                    playingSong.PlaySongVisualization(theSong);
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID!");
                    Line();
                }
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine($"Sorry There Are No {albumSearch} Album!");
            Line();
        }
    }

    else if (choice == 6)
    {
        Janitor();
        Line();
        Console.WriteLine("Artist Search...");
        Console.WriteLine("Enter Artist Name");
        string artistSearch = Console.ReadLine();

        var searchedArtist = _context.Artists
                                   .Where(s => s.Name.Contains(artistSearch))
                                   .ToList();

        if (artistSearch == "")
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Album!");
            Line();
        }
        else if (searchedArtist != null)
        {
            Janitor();
            Line();
            Console.WriteLine($"Here Are Results For: {searchedArtist}");

            foreach (var artist in searchedArtist)
            {
                Line();
                Console.WriteLine($"Artist Name -> {artist.Name}");
                Console.WriteLine($"Artist Genre -> {artist.Genre}");
                Console.WriteLine($"Artist ID -> {artist.Id}");
                Line();
            }
            Console.WriteLine("Enter Artist's ID");
            int artistId;
            bool validArtistId = int.TryParse(Console.ReadLine(), out artistId);

            var choosenArtist = _context.Artists
                                        .Include(a => a.Albums)
                                        .FirstOrDefault(a => a.Id == artistId);

            if (!validArtistId)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                Line();
            }
            else if (choosenArtist != null)
            {
                Janitor();
                Line();
                var artistAlbums = _context.Albums
                                           .Include(a => a.artist)
                                           .Include(a => a.songs)
                                           .Where(a => a.ArtistId == choosenArtist.Id)
                                           .ToList();

                Console.WriteLine($"{choosenArtist.Name} Albums:");
                foreach (var album in artistAlbums)
                {
                    Line();
                    Console.WriteLine($"Album ID -> {album.Id}");
                    Console.WriteLine($"Album Title -> {album.Title}");
                    Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
                    Console.WriteLine($"Album Genre -> {album.Genre}");
                    Console.WriteLine($"Album Duration -> {album.AlbumLength}");
                    Console.WriteLine($"Album Song Count -> {album.songs.Count}");
                    Console.WriteLine($"Album Rating -> {album.Rating}");
                    Line();
                }
                Line();
                Console.WriteLine("Choose Album ID!");
                int chooseAlbumId;
                bool validChooseAlbumId = int.TryParse(Console.ReadLine(), out chooseAlbumId);

                var choosenAlbum = _context.Albums
                                           .Include(a => a.songs)
                                           .FirstOrDefault(a => a.Id == chooseAlbumId);

                if (!validChooseAlbumId)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                    Line();
                }
                else if (choosenAlbum != null)
                {
                    Janitor();
                    Line();
                    Console.WriteLine($"{choosenAlbum.Title} Songs:");
                    Line();

                    var albumSongs = _context.Songs
                                             .Include(s => s.album)
                                             .Include(s => s.album.artist)
                                             .Where(s => s.album.Id == choosenAlbum.Id)
                                             .ToList();

                    foreach (var songs in albumSongs)
                    {
                        Line();
                        Console.WriteLine($"Song ID -> {songs.Id}");
                        Console.WriteLine($"Song Title -> {songs.Title}");
                        Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
                        Console.WriteLine($"Song's Album -> {songs.album.Title}");
                        Console.WriteLine($"Song Duration -> {songs.Duration}");
                        Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
                        Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
                        Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
                        Line();
                    }
                    Console.WriteLine("Choose Song ID To Play!");

                    int chooseSong;
                    bool validChooseSong = int.TryParse(Console.ReadLine(), out chooseSong);

                    var choosenSong = _context.Songs.FirstOrDefault(s => s.Id == chooseSong);

                    if (!validChooseSong)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Valid ID, Don't Use Symbols Or Letters!");
                        Line();
                    }
                    else if (choosenSong != null)
                    {
                        var theSong = choosenSong;
                        Song playingSong = new Song();
                        choosenSong.TimesPlayed = choosenSong.TimesPlayed + 1;
                        _context.SaveChanges();

                        string fullRoute = Path.Combine(baseRoute, songPlayHistory);

                        using (StreamWriter wr = new StreamWriter(fullRoute, true))
                        {
                            wr.WriteLine("==========================");
                            wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played - {DateTime.Now}");

                        }

                        playingSong.PlaySongVisualization(theSong);
                    }
                    else
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Valid ID!");
                        Line();
                    }
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Valid ID!");
                    Line();
                }


            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid ID!");
                Line();
            }
        }
        else
        {
            Janitor();
            Line();
            Console.WriteLine($"Sorry There Are No {artistSearch} Album!");
            Line();
        }
    }

    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Console.WriteLine("Valid Number Is Between 1 And 6");
        Line();
    }

}

void TopSongs()
{
    Console.BackgroundColor = ConsoleColor.DarkMagenta;
    Console.ForegroundColor = ConsoleColor.Gray;

    Janitor();
    Line();
    Console.WriteLine("Choose What To See");
    Console.WriteLine("1. Top 3 Most Played Songs");
    Console.WriteLine("2. Top 3 Longest Songs");
    Console.WriteLine("3. Top 10 Most Played Songs");
    Console.WriteLine("4. Top 10 Longest Songs");

    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Most Played Songs!");
        var topSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.TimesPlayed)
                               .Take(3);

        foreach (var songs in topSongs)
        {
            Line();
            Console.WriteLine($"Song ID -> {songs.Id}");
            Console.WriteLine($"Song Title -> {songs.Title}");
            Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
            Console.WriteLine($"Song's Album -> {songs.album.Title}");
            Console.WriteLine($"Song Duration -> {songs.Duration}");
            Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
            Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
            Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
            Line();
        }

    }
    else if (choice == 2)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Most Played Songs!");
        var topSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.Duration)
                               .Take(3);

        foreach (var songs in topSongs)
        {
            Line();
            Console.WriteLine($"Song ID -> {songs.Id}");
            Console.WriteLine($"Song Title -> {songs.Title}");
            Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
            Console.WriteLine($"Song's Album -> {songs.album.Title}");
            Console.WriteLine($"Song Duration -> {songs.Duration}");
            Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
            Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
            Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
            Line();
        }

    }
    else if (choice == 3)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Most Played Songs!");
        var topSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.TimesPlayed)
                               .Take(10);

        foreach (var songs in topSongs)
        {
            Line();
            Console.WriteLine($"Song ID -> {songs.Id}");
            Console.WriteLine($"Song Title -> {songs.Title}");
            Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
            Console.WriteLine($"Song's Album -> {songs.album.Title}");
            Console.WriteLine($"Song Duration -> {songs.Duration}");
            Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
            Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
            Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
            Line();
        }

    }
    else if (choice == 4)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Most Played Songs!");
        var topSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .ToList()
                               .OrderByDescending(s => s.Duration)
                               .Take(10);

        foreach (var songs in topSongs)
        {
            Line();
            Console.WriteLine($"Song ID -> {songs.Id}");
            Console.WriteLine($"Song Title -> {songs.Title}");
            Console.WriteLine($"Song's Artist -> {songs.album.artist.Name}");
            Console.WriteLine($"Song's Album -> {songs.album.Title}");
            Console.WriteLine($"Song Duration -> {songs.Duration}");
            Console.WriteLine($"Song Lyrics -> {songs.Lyrics}");
            Console.WriteLine($"Song Track Number -> {songs.TrackNumber}");
            Console.WriteLine($"Song Has Been Played -> {songs.TimesPlayed} Times");
            Line();
        }

    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number,!");
        Line();
    }
}

void GenreStatistic()
{
    Console.BackgroundColor = ConsoleColor.DarkYellow;
    Console.ForegroundColor = ConsoleColor.Black;

    Janitor();
    Line();
    Console.WriteLine("Choose To See Genre Statistic");
    Console.WriteLine("1. Total Album Genres");
    Console.WriteLine("2. Total Artist Genres");
    Console.WriteLine("3. Top 3 Most Listened Genres");
    Console.WriteLine("4. Top 10 Most Listened Genres");

    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        var allAlbumGenres = _context.Albums.GroupBy(a => a.Genre).ToList().Count();

        Console.WriteLine($"There Are -> {allAlbumGenres} Genre Albums");
        var allAlbums = _context.Albums.ToList();
        Line();
        Console.WriteLine("There Are This Albums:");
        Line();
        foreach (var genre in allAlbums)
        {
            Console.WriteLine($"{genre.Genre}");
        }
        Line();
    }
    else if (choice == 2)
    {
        Janitor();
        var allAlbumGenres = _context.Artists.GroupBy(a => a.Genre).ToList().Count();

        Console.WriteLine($"There Are -> {allAlbumGenres} Genre Albums");
        var allAlbums = _context.Albums.ToList();
        Line();
        Console.WriteLine("There Are This Albums:");
        Line();
        foreach (var genre in allAlbums)
        {
            Console.WriteLine($"{genre.Genre}");
        }
        Line();
    }
    else if (choice == 3)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Genres:");
        Line();
        var topGenres = _context.Albums
                                .Include(a => a.songs)
                                .ToList();

        List<TopThreeGenres> threeGenres = new List<TopThreeGenres>();
        foreach (var genre in topGenres)
        {

            var songs = genre.songs.ToList();

            int totalPlayed = 0;
            foreach (var song in songs)
            {
                totalPlayed = totalPlayed + song.TimesPlayed;
            }


            TopThreeGenres theTop = new TopThreeGenres(genre.Genre, totalPlayed);

            threeGenres.Add(theTop);
        }

        var actualThreeGenres = threeGenres.OrderByDescending(g => g.Played)
                                           .Take(3)
                                           .ToList();
        foreach (var topsong in actualThreeGenres)
        {
            Line();
            Console.WriteLine($"{topsong.Genre} Has Pleayed Total {topsong.Played} Times!");
            Line();
        }
    }
    else if (choice == 4)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 10 Genres:");
        Line();
        var topGenres = _context.Albums
                                .Include(a => a.songs)
                                .ToList();

        List<TopThreeGenres> threeGenres = new List<TopThreeGenres>();
        foreach (var genre in topGenres)
        {

            var songs = genre.songs.ToList();

            int totalPlayed = 0;
            foreach (var song in songs)
            {
                totalPlayed = totalPlayed + song.TimesPlayed;
            }


            TopThreeGenres theTop = new TopThreeGenres(genre.Genre, totalPlayed);

            threeGenres.Add(theTop);
        }

        var actualThreeGenres = threeGenres.OrderByDescending(g => g.Played)
                                           .Take(10)
                                           .ToList();
        foreach (var topsong in actualThreeGenres)
        {
            Line();
            Console.WriteLine($"{topsong.Genre} Has Pleayed Total {topsong.Played} Times!");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Line();
    }

}

void ArtistRating()
{
    Console.BackgroundColor = ConsoleColor.DarkCyan;
    Console.ForegroundColor = ConsoleColor.Black;

    Janitor();
    Line();
    Console.WriteLine("Enter Number To See:");
    Console.WriteLine("1. All Artist Rating");
    Console.WriteLine("2. All Artist (From Highest To Lowest) Rating");
    Console.WriteLine("3. Top 3 Artist By Rating");
    Console.WriteLine("4. Top 10 Artist By Rating");
    Console.WriteLine("5. Top 3 Artist By Lowest Rating");

    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Symbols Or Letters!");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        Line();
        Console.WriteLine("All Artist Rating:");
        Line();

        var allArtist = _context.Artists
                                .Include(a => a.ArtistDetails)
                                .Include(a => a.Albums)
                                .ToList();

        foreach (var artist in allArtist)
        {
            decimal artistRating = 0;

            foreach (var album in artist.Albums)
            {
                artistRating = artistRating + album.Rating;
            }

            decimal overallRating = artistRating / artist.Albums.Count;
            Line();
            Console.WriteLine($"{artist.Name}");
            Console.WriteLine($"Country -> {artist.Country}");
            Console.WriteLine($"Genre -> {artist.Genre}");
            Console.WriteLine($"Formation Year -> {artist.ArtistDetails.FormationYear}");
            Console.WriteLine($"Website -> {artist.ArtistDetails.Website}");
            Console.WriteLine($"Total Albums -> {artist.ArtistDetails.TotalAlbums}");
            Console.WriteLine($"Is Active -> {artist.ArtistDetails.IsActive}");
            Console.WriteLine($"Description -> {artist.Description}");
            Console.WriteLine($"Rating -> {overallRating}");
            Line();
        }
    }
    else if (choice == 2)
    {
        Janitor();
        Line();
        Console.WriteLine("Top Artist Rating:");
        Line();
        var allArtist = _context.Artists
                                .Include(a => a.ArtistDetails)
                                .Include(a => a.Albums)
                                .ToList();

        List<RatingArtist> top = new List<RatingArtist>();

        foreach (var artist in allArtist)
        {
            decimal artistRating = 0;
            foreach (var album in artist.Albums)
            {
                artistRating = artistRating + album.Rating;
            }
            decimal overallRating = artist.Albums.Count > 0
                ? artistRating / artist.Albums.Count
                : 0;

            RatingArtist rating = new RatingArtist(
                artist.Name,
                artist.Country,
                artist.Genre,
                artist.ArtistDetails.FormationYear,
                artist.ArtistDetails.Website,
                artist.ArtistDetails.TotalAlbums,
                artist.ArtistDetails.IsActive,
                artist.Description,
                overallRating);

            top.Add(rating);
        }

        foreach (var topArtist in top.OrderByDescending(t => t.Rating))
        {
            Line();
            Console.WriteLine($"{topArtist.Name}");
            Console.WriteLine($"Country -> {topArtist.Country}");
            Console.WriteLine($"Genre -> {topArtist.Genre}");
            Console.WriteLine($"Formation Year -> {topArtist.Year}");
            Console.WriteLine($"Website -> {topArtist.Website}");
            Console.WriteLine($"Total Albums -> {topArtist.Albums}");
            Console.WriteLine($"Is Active -> {topArtist.Activity}");
            Console.WriteLine($"Description -> {topArtist.Description}");
            Console.WriteLine($"Rating -> {topArtist.Rating}");
            Line();
        }
    }
    else if (choice == 3)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Artist:");
        Line();
        var allArtist = _context.Artists
                                .Include(a => a.ArtistDetails)
                                .Include(a => a.Albums)
                                .ToList();

        List<RatingArtist> top = new List<RatingArtist>();

        foreach (var artist in allArtist)
        {
            decimal artistRating = 0;
            foreach (var album in artist.Albums)
            {
                artistRating = artistRating + album.Rating;
            }
            decimal overallRating = artist.Albums.Count > 0
                ? artistRating / artist.Albums.Count
                : 0;

            RatingArtist rating = new RatingArtist(
                artist.Name,
                artist.Country,
                artist.Genre,
                artist.ArtistDetails.FormationYear,
                artist.ArtistDetails.Website,
                artist.ArtistDetails.TotalAlbums,
                artist.ArtistDetails.IsActive,
                artist.Description,
                overallRating);

            top.Add(rating);
        }

        foreach (var topArtist in top.OrderByDescending(t => t.Rating).Take(3))
        {
            Line();
            Console.WriteLine($"{topArtist.Name}");
            Console.WriteLine($"Country -> {topArtist.Country}");
            Console.WriteLine($"Genre -> {topArtist.Genre}");
            Console.WriteLine($"Formation Year -> {topArtist.Year}");
            Console.WriteLine($"Website -> {topArtist.Website}");
            Console.WriteLine($"Total Albums -> {topArtist.Albums}");
            Console.WriteLine($"Is Active -> {topArtist.Activity}");
            Console.WriteLine($"Description -> {topArtist.Description}");
            Console.WriteLine($"Rating -> {topArtist.Rating}");
            Line();
        }
    }
    else if (choice == 4)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 10 Artist:");
        Line();
        var allArtist = _context.Artists
                                .Include(a => a.ArtistDetails)
                                .Include(a => a.Albums)
                                .ToList();

        List<RatingArtist> top = new List<RatingArtist>();

        foreach (var artist in allArtist)
        {
            decimal artistRating = 0;
            foreach (var album in artist.Albums)
            {
                artistRating = artistRating + album.Rating;
            }
            decimal overallRating = artist.Albums.Count > 0
                ? artistRating / artist.Albums.Count
                : 0;

            RatingArtist rating = new RatingArtist(
                artist.Name,
                artist.Country,
                artist.Genre,
                artist.ArtistDetails.FormationYear,
                artist.ArtistDetails.Website,
                artist.ArtistDetails.TotalAlbums,
                artist.ArtistDetails.IsActive,
                artist.Description,
                overallRating);

            top.Add(rating);
        }

        foreach (var topArtist in top.OrderByDescending(t => t.Rating).Take(3))
        {
            Line();
            Console.WriteLine($"{topArtist.Name}");
            Console.WriteLine($"Country -> {topArtist.Country}");
            Console.WriteLine($"Genre -> {topArtist.Genre}");
            Console.WriteLine($"Formation Year -> {topArtist.Year}");
            Console.WriteLine($"Website -> {topArtist.Website}");
            Console.WriteLine($"Total Albums -> {topArtist.Albums}");
            Console.WriteLine($"Is Active -> {topArtist.Activity}");
            Console.WriteLine($"Description -> {topArtist.Description}");
            Console.WriteLine($"Rating -> {topArtist.Rating}");
            Line();
        }
    }
    else if (choice == 5)
    {
        Janitor();
        Line();
        Console.WriteLine("Lowest 3 Artist:");
        Line();
        var allArtist = _context.Artists
                                .Include(a => a.ArtistDetails)
                                .Include(a => a.Albums)
                                .ToList();

        List<RatingArtist> top = new List<RatingArtist>();

        foreach (var artist in allArtist)
        {
            decimal artistRating = 0;
            foreach (var album in artist.Albums)
            {
                artistRating = artistRating + album.Rating;
            }
            decimal overallRating = artist.Albums.Count > 0
                ? artistRating / artist.Albums.Count
                : 0;

            RatingArtist rating = new RatingArtist(
                artist.Name,
                artist.Country,
                artist.Genre,
                artist.ArtistDetails.FormationYear,
                artist.ArtistDetails.Website,
                artist.ArtistDetails.TotalAlbums,
                artist.ArtistDetails.IsActive,
                artist.Description,
                overallRating);

            top.Add(rating);
        }

        foreach (var topArtist in top.OrderBy(t => t.Rating).Take(3))
        {
            Line();
            Console.WriteLine($"{topArtist.Name}");
            Console.WriteLine($"Country -> {topArtist.Country}");
            Console.WriteLine($"Genre -> {topArtist.Genre}");
            Console.WriteLine($"Formation Year -> {topArtist.Year}");
            Console.WriteLine($"Website -> {topArtist.Website}");
            Console.WriteLine($"Total Albums -> {topArtist.Albums}");
            Console.WriteLine($"Is Active -> {topArtist.Activity}");
            Console.WriteLine($"Description -> {topArtist.Description}");
            Console.WriteLine($"Rating -> {topArtist.Rating}");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Line();
    }
}

void MostPlayedAlbums()
{
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.ForegroundColor = ConsoleColor.Gray;

    Janitor();
    Line();
    Console.WriteLine("Enter What You Want To See:");
    Console.WriteLine("1. Most Listened Albums List");
    Console.WriteLine("2. Top 3 Most Listened Albums");
    Console.WriteLine("3. Top 10 Most Listened Albums");
    Console.WriteLine("4. Least Listened Albums List");
    Console.WriteLine("5. Top 3 Least Listened Albums List");
    Console.WriteLine("6. Top 10 Least Listened Albums List");
    int choice;
    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

    if (!validChoice)
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number, Don't Use Letters Or Symbols!");
        Console.WriteLine("Valid Number Is Between 1 And 6");
        Line();
    }
    else if (choice == 1)
    {
        Janitor();
        Line();
        Console.WriteLine("Most Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderByDescending(t => t.AllListens))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else if (choice == 2)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 3 Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderByDescending(t => t.AllListens).Take(3))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else if (choice == 3)
    {
        Janitor();
        Line();
        Console.WriteLine("Top 10 Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderByDescending(t => t.AllListens).Take(10))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else if (choice == 4)
    {
        Janitor();
        Line();
        Console.WriteLine("Least Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderBy(t => t.AllListens))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else if (choice == 5)
    {
        Janitor();
        Line();
        Console.WriteLine("Least 3 Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderBy(t => t.AllListens).Take(3))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else if (choice == 6)
    {
        Janitor();
        Line();
        Console.WriteLine("Least 10 Listened Albums List:");
        List<MostAlbums> topAlbums = new List<MostAlbums>();

        var allAlbums = _context.Albums
                                .Include(a => a.songs)
                                .Include(a => a.artist)
                                .ToList();

        foreach (var album in allAlbums)
        {
            int allListenedSongs = 0;
            foreach (var song in album.songs)
            {
                allListenedSongs = allListenedSongs + song.TimesPlayed;
            }

            MostAlbums mostAlbums = new MostAlbums
                (
                    album.artist.Name,
                    album.Title,
                    album.ReleaseYear,
                    album.Genre,
                    album.Rating,
                    album.RatingParticipant,
                    album.OverAllScore,
                    album.AlbumLength,
                    allListenedSongs
                );
            topAlbums.Add(mostAlbums);
        }

        foreach (var album in topAlbums.OrderBy(t => t.AllListens).Take(10))
        {
            Line();
            Console.WriteLine($"Album Title -> {album.Title}");
            Console.WriteLine($"Album Artist -> {album.Artist}");
            Console.WriteLine($"Album Release Year -> {album.ReleaseYear}");
            Console.WriteLine($"Album Genre -> {album.Genre}");
            Console.WriteLine($"Album Rating -> {album.Rating}");
            Console.WriteLine($"Album Lenght -> {album.AlbumLength}");
            Console.WriteLine($"Album Has Been Listened -> {album.AllListens} Times!");
            Line();
        }
    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Number!");
        Console.WriteLine("Valid Number Is Between 1 And 6");
        Line();
    }
}

void SystemLog()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkCyan;

    Janitor();
    Line();
    Console.WriteLine("System Log:");
    string fullRoute = Path.Combine(baseRoute, logRoute);
    string raport = "";

    using (StreamReader sr = new StreamReader(fullRoute))
    {
        raport = sr.ReadToEnd();
        Console.WriteLine(raport);

    }
}

void PlaySongLog()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkRed;

    Janitor();
    Line();
    Console.WriteLine("Played Song History Log:");
    string fullRoute = Path.Combine(baseRoute, songPlayHistory);
    string raport = "";

    using (StreamReader sr = new StreamReader(fullRoute))
    {
        raport = sr.ReadToEnd();
        Console.WriteLine(raport);

    }
}

void Playlist()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.ForegroundColor = ConsoleColor.DarkGreen;

    Janitor();
    string fullRoute = Path.Combine(baseRoute, playlistRoute);
    string raport = "";

    using(StreamWriter wr = new StreamWriter(fullRoute))
    {
        TimeSpan allTime = TimeSpan.Zero;
        var allSongs = _context.Songs
                               .Include(s => s.album)
                               .Include(s => s.album.artist)
                               .OrderBy(s => s.album.Genre)
                               .ToList();
        foreach(var song in allSongs) 
        {
            allTime = allTime + song.Duration;
        }
        wr.WriteLine("==========================================");
        wr.WriteLine($"Playlist Duration: {allTime}");
        wr.WriteLine("==========================================");
        foreach(var song in allSongs)
        {
            wr.WriteLine("==========================================");
            wr.WriteLine($"{song.album.artist.Name} - {song.Title} ({song.Duration})");
            wr.WriteLine($"({song.album.Genre})");
            wr.WriteLine("==========================================");
        }
    }

    using (StreamReader sr = new StreamReader(fullRoute))
    {
        raport = sr.ReadToEnd();
        Console.WriteLine(raport);

    }
}

User newUser = new User();

void MainApp()
{

    Line();
    Console.WriteLine("Start App? Y/N");
    string firstAnswer = Console.ReadLine();
    if (firstAnswer == "")
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Y Or N");
        Line();
    }
    else if (firstAnswer.ToLower() == "n")
    {
        Janitor();
        Line();
        Console.WriteLine("See You Again!");
        Line();
        Environment.Exit(0);
    }
    else if (firstAnswer.ToLower() == "y")
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkCyan;

        Janitor();
        Line();
        Console.WriteLine("Welcome To Music Library Manager!");
        Console.WriteLine("Choose Action!");
        Console.WriteLine("1. Manage Artists");
        Console.WriteLine("2. Manage Albums");
        Console.WriteLine("3. Manage Songs");
        Console.WriteLine("4. Analytics");
        Console.WriteLine("5. Manage Files");
        Console.WriteLine("6. Log In / Sing Up");

        int secondAnswer;
        bool validSecondAnswer = int.TryParse(Console.ReadLine(), out secondAnswer);
        if (!validSecondAnswer)
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
            Line();
        }
        else if (secondAnswer == 1)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Artist Managment!");
            Console.WriteLine("Choose What To Do:");
            Console.WriteLine("1. Add New Artist");
            Console.WriteLine("2. Edit Artist");
            Console.WriteLine("3. Remove Artist");
            Console.WriteLine("4. See All Artists");

            int choice;
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);
            if (!validChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
                Console.WriteLine("Valid Number Is Between 1 And 4!");
                Line();
            }
            else if (choice == 1)
            {
                AddArtist();
            }
            else if (choice == 2)
            {
                ChangeArtist();
            }
            else if (choice == 3)
            {
                RemoveArtist();
            }
            else if (choice == 4)
            {
                SeeAllArtist();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number!");
                Console.WriteLine("Valid Number Is Between 1 And 4!");
                Line();
            }
        }
        else if (secondAnswer == 2)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Album Managment!");
            Console.WriteLine("Choose What To Do:");
            Console.WriteLine("1. Add New Album");
            Console.WriteLine("2. Edit Album");
            Console.WriteLine("3. Remove Album");
            Console.WriteLine("4. See All Albums");
            Console.WriteLine("5. Edit Album Rating");

            int choice;
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);
            if (!validChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
                Console.WriteLine("Valid Number Is Between 1 And 5!");
                Line();
            }
            else if (choice == 1)
            {
                AddAlbum();
            }
            else if (choice == 2)
            {
                ChangeAlbum();
            }
            else if (choice == 3)
            {
                RemoveAlbum();
            }
            else if (choice == 4)
            {
                AllAlbums();
            }
            else if (choice == 5)
            {
                ChangeRating();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number!");
                Console.WriteLine("Valid Number Is Between 1 And 4!");
                Line();
            }
        }

        else if (secondAnswer == 3)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Song Managment!");
            Console.WriteLine("Choose What To Do:");
            Console.WriteLine("1. Add New Song");
            Console.WriteLine("2. Edit Song");
            Console.WriteLine("3. Remove Song");
            Console.WriteLine("4. See All Songs");
            Console.WriteLine("5. Play Song");

            int choice;
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);
            if (!validChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
                Console.WriteLine("Valid Number Is Between 1 And 5!");
                Line();
            }
            else if (choice == 1)
            {
                AddSong();
            }
            else if (choice == 2)
            {
                ChangeSong();
            }
            else if (choice == 3)
            {
                RemoveSong();
            }
            else if (choice == 4)
            {
                AllSongs();
            }
            else if (choice == 5)
            {
                PlaySong();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number!");
                Console.WriteLine("Valid Number Is Between 1 And 5!");
                Line();
            }
        }

        else if (secondAnswer == 4)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Analytics!");
            Console.WriteLine("Choose What To Do:");
            Console.WriteLine("1. Top Songs");
            Console.WriteLine("2. Genre Statistics");
            Console.WriteLine("3. Artist Rating");
            Console.WriteLine("4. Most Played Albums");

            int choice;
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);
            if (!validChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
                Console.WriteLine("Valid Number Is Between 1 And 4!");
                Line();
            }
            else if (choice == 1)
            {
                TopSongs();
            }
            else if (choice == 2)
            {
                GenreStatistic();
            }
            else if (choice == 3)
            {
                ArtistRating();
            }
            else if (choice == 4)
            {
                MostPlayedAlbums();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number!");
                Console.WriteLine("Valid Number Is Between 1 And 4!");
                Line();
            }
        }

        else if (secondAnswer == 5)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Files Menagment!");
            Console.WriteLine("Choose What To Do:");
            Console.WriteLine("1. See Playlist Log");
            Console.WriteLine("2. See Recently Played Songs");
            Console.WriteLine("3. See System Log");

            int choice;
            bool validChoice = int.TryParse(Console.ReadLine(), out choice);
            if (!validChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number! Don't Use Letters Or Symbols!");
                Console.WriteLine("Valid Number Is Between 1 And 3!");
                Line();
            }
            else if (choice == 1)
            {
                Playlist();
            }
            else if (choice == 2)
            {
                PlaySongLog();
            }
            else if (choice == 3)
            {
                SystemLog();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Valid Number!");
                Console.WriteLine("Valid Number Is Between 1 And 3!");
                Line();
            }
        }

        else if (secondAnswer == 6)
        {
            Janitor();
            Line();
            Console.WriteLine("Welcome To Account Managment!");
            Console.WriteLine("1. Log In");
            Console.WriteLine("2. Sign Up");

            int accChoice;
            bool validAccChoice = int.TryParse(Console.ReadLine(), out  accChoice);

            if (!validAccChoice)
            {
                Janitor();
                Line();
                Console.WriteLine("Please Use Valid Numbers, Don't Use Symbols Or Letters!");
            }
            else if (accChoice == 1)
            {
                newUser.UserLogIn();
            }
            else if (accChoice == 2)
            {
                newUser.UserRegister();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Please Use Valid Numbers!");
            }
        } 

        else
        {
            Janitor();
            Line();
            Console.WriteLine("Please Enter Valid Number!");
            Line();
        }

    }
    else
    {
        Janitor();
        Line();
        Console.WriteLine("Please Enter Valid Answer! Y Or N!");
        Line();
    }
}

while (true)
{
    MainApp();
}

