using MusicApp.Data;
using MusicApp.SMTP;
using System.Text;
using BCrypt.Net;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace MusicApp.Models;

internal class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Nickname { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Song> songs { get; set; }

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


    DataContext _context = new DataContext();
    public void UserRegister()
    {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Janitor();
        Line();
        Console.WriteLine("Enter User Name");
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
            Janitor();
            Line();
            Console.WriteLine("Enter Last Name");
            string newLastName = Console.ReadLine();

            if (newLastName == "")
            {
                Janitor();
                Line();
                Console.WriteLine("Please Enter Last Name!");
                Line();
            }
            else
            {
                Janitor();
                Line();
                Console.WriteLine("Enter Nickname");
                string newNickname = Console.ReadLine();
                var validNickname = _context.Users.FirstOrDefault(v => v.Nickname == newNickname);

                if (newNickname == "")
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Please Enter Nickname!");
                    Line();
                }
                else if(validNickname != null)
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Sorry Nickname Is Taken!");
                    Line();
                }
                else
                {
                    Janitor();
                    Line();
                    Console.WriteLine("Enter Age");
                    int newAge;
                    bool validNewAge = int.TryParse(Console.ReadLine(), out newAge);

                    if (!validNewAge)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Valid Age! Don't Use Letters Or Symbols!");
                        Console.WriteLine("Valid Age Start At 13");
                        Line();
                    }
                    else if (newAge >= 13 && newAge <= 130) 
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Enter Email");
                        string newMail = Console.ReadLine();
                        var validMail = _context.Users.FirstOrDefault(v => v.Email == newMail);

                        if (newMail == "")
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Email!");
                            Line();
                        }
                        else if (validMail != null)
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("This Email Already Has An Accaount!");
                            Line();
                        }
                        else if (newMail.Contains("@yahoo.com") || newMail.Contains("@mail.ru"))
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Sorry! We Only Support Gmail!");
                            Line();
                        }
                        else if (newMail.Contains("@gmail.com"))
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Enter Password");
                            Console.WriteLine("Password Must Contain 8 Characters!");
                            string newPassword = Console.ReadLine();

                            if (newPassword == "")
                            {
                                Janitor();
                                Line();
                                Console.WriteLine("Please Enter Passoword!");
                                Line();
                            }
                            else if (newPassword.Length < 8)
                            {
                                Janitor();
                                Line();
                                Console.WriteLine("Please Enter Valid Passoword!");
                                Line();
                            }
                            else
                            {
                                Janitor();
                                Line();
                                Console.WriteLine("Processing...");
                                string? GenerateRandomPassword()
                                {
                                    const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                                    Random random = new Random();

                                    StringBuilder passwordBuilder = new StringBuilder(7);

                                    for (int i = 0; i < 7; i++)
                                    {
                                        int index = random.Next(validChars.Length);
                                        passwordBuilder.Append(validChars[index]);
                                    }

                                    return passwordBuilder.ToString();
                                }

                                string? randomNumber = GenerateRandomPassword();

                                SMTPService.SendEmail(newMail, "Music App Registration", @$"<!DOCTYPE html>
<html>
<head>
    <style>
        .email-container {{
            max-width: 600px;
            margin: 0 auto;
            font-family: 'Arial', sans-serif;
            background: #0a0a1a;
            color: #fff;
            padding: 30px;
            border-radius: 20px;
        }}
        .header {{
            background: linear-gradient(135deg, #00f2fe, #4facfe);
            padding: 25px;
            text-align: center;
            border-radius: 15px;
            margin-bottom: 30px;
            position: relative;
            overflow: hidden;
            box-shadow: 0 0 30px rgba(79,172,254,0.3);
        }}
        .header::before {{
            content: '';
            position: absolute;
            top: -50%;
            left: -50%;
            width: 200%;
            height: 200%;
            background: linear-gradient(45deg, transparent, rgba(255,255,255,0.1), transparent);
            transform: rotate(45deg);
            animation: shine 3s infinite;
        }}
        @keyframes shine {{
            0% {{ transform: translateX(-100%) rotate(45deg); }}
            100% {{ transform: translateX(100%) rotate(45deg); }}
        }}
        .content {{
            background: rgba(255,255,255,0.05);
            padding: 30px;
            border-radius: 15px;
            border: 1px solid rgba(255,255,255,0.1);
            backdrop-filter: blur(10px);
            box-shadow: 0 8px 32px rgba(0,0,0,0.1);
        }}
        .verification-code {{
            font-size: 36px;
            font-weight: bold;
            color: #4facfe;
            text-align: center;
            padding: 20px;
            margin: 25px 0;
            background: rgba(79,172,254,0.1);
            border-radius: 12px;
            letter-spacing: 8px;
            border: 2px solid rgba(79,172,254,0.3);
            text-shadow: 0 0 10px rgba(79,172,254,0.5);
            animation: pulse 2s infinite;
        }}
        @keyframes pulse {{
            0% {{ box-shadow: 0 0 0 0 rgba(79,172,254,0.4); }}
            70% {{ box-shadow: 0 0 0 15px rgba(79,172,254,0); }}
            100% {{ box-shadow: 0 0 0 0 rgba(79,172,254,0); }}
        }}
        .footer {{
            text-align: center;
            color: rgba(255,255,255,0.6);
            margin-top: 25px;
            font-size: 14px;
            border-top: 1px solid rgba(255,255,255,0.1);
            padding-top: 20px;
        }}
        p {{
            line-height: 1.6;
            margin: 15px 0;
        }}
        h1 {{
            margin: 0;
            font-size: 28px;
            background: linear-gradient(to right, #fff, #4facfe);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <h1>Welcome to Music App</h1>
        </div>
        <div class=""content"">
            <p>Hello {newName} {newLastName},</p>
            <p>Your journey into the future of music begins here. To complete your registration, use this secure verification code:</p>
            <div class=""verification-code"">{randomNumber}</div>
            <p>Enter this code to unlock your Music App experience.</p>
        </div>
        <div class=""footer"">
            <p>Automated Security Message • Do Not Reply</p>
        </div>
    </div>
</body>
</html>");
                                Janitor();
                                Line();
                                Console.WriteLine("Please Enter Code From Email");
                                string validatePassword = Console.ReadLine();

                                if(validatePassword != randomNumber)
                                {
                                    Janitor();
                                    Line();
                                    Console.WriteLine("Wrong Code!");
                                }
                                else
                                {
                                    Janitor();
                                    Line();
                                    Console.WriteLine("Saving Information...");
                                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                                    _context.Add(new User
                                    {
                                        Name = newName,
                                        LastName = newLastName,
                                        Nickname = newNickname,
                                        Age = newAge,
                                        Email = newMail,
                                        Password = hashedPassword
                                    });
                                    _context.SaveChanges();
                                    Console.WriteLine("Saved!");    
                                }
                            }
                        }
                        else
                        {
                            Janitor();
                            Line();
                            Console.WriteLine("Please Enter Valid Email!");
                            Line();
                        }
                    }
                    else if (newAge < 13 && newAge > 0)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("We Are Sorry, This App Is 13+!");
                        Console.WriteLine($"Please Wait {13 - newAge } Year!");
                        Line();
                    }
                    else
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Valid Age!");
                        Console.WriteLine("Valid Age Start At 13");
                        Line();
                    }
                }
            }
        }
    }
    public void UserLogIn()
    {

        Line();
        Console.WriteLine("Enter Email");
        string logPassword = Console.ReadLine();
        var choosenEmail = _context.Users
                                   .Include(c => c.songs)                                  
                                   .FirstOrDefault(c => c.Email == logPassword);

        if ( choosenEmail == null )
        {
            Janitor();
            Line();
            Console.WriteLine("Wrong Email!");
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Janitor();
            Line();
            Console.WriteLine("Enter Password");
            string logInPassword = Console.ReadLine();

            bool isCorrect = BCrypt.Net.BCrypt.Verify(logInPassword, choosenEmail.Password);

            if ( !isCorrect )
            {
                Janitor();
                Line();
                Console.WriteLine("Wrong Email!");
            }
            else if (isCorrect)
            {
                Janitor();
                Line();
                Console.WriteLine($"Welcome {choosenEmail.Nickname}!");
                void userMenu()
                {
                    Line();
                    Console.WriteLine("What Do You Want To Do?");
                    Console.WriteLine("1. Add Song To The Playlist");
                    Console.WriteLine("2. Play Song From The Playlist");
                    Console.WriteLine("3. See The Playlist");
                    Console.WriteLine("4. Import Playlist To Mail");
                    Console.WriteLine("5. Remove Song From The Playlist");
                    Console.WriteLine("6. Exit");
                    Console.WriteLine("Enter Number");
                    Console.WriteLine("\n");
                    int choice;
                    bool validChoice = int.TryParse(Console.ReadLine(), out choice);

                    if (!validChoice)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Please Enter Valid Number, Don't Use Letters Or Symbols!");
                        Console.WriteLine("Valid Number Is Between 1 And 6!");
                    }
                    else if (choice == 1)
                    {
                        void chooseSong()
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkRed;

                            Janitor();
                            Line();
                            Console.WriteLine("Choose Song To Add To The Playlist!");
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
                                        Console.WriteLine("Choose Song ID To Add To The Playlist!");

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

                                            choosenEmail.songs.Add(theSong);
                                            _context.SaveChanges();

                                            string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                            {
                                                wr.WriteLine("==========================");
                                                wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                            }

                                            Console.WriteLine("Song Is Saved!");
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
                                    Console.WriteLine("Choose Song ID To Add To The Playlist!");

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

                                        choosenEmail.songs.Add(theSong);
                                        _context.SaveChanges();

                                        string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                        using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                        {
                                            wr.WriteLine("==========================");
                                            wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                        }
                                        Console.WriteLine("Song Is Saved!");
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
                                Console.WriteLine("Choose Song ID To Add To The Playlist!");

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
                                    choosenEmail.songs.Add(theSong);
                                    _context.SaveChanges();

                                    string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                    using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                    {
                                        wr.WriteLine("==========================");
                                        wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                    }
                                    Console.WriteLine("Song Is Saved!");
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
                                    Console.WriteLine("Choose Song ID To Add To The Playlist!");

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

                                        choosenEmail.songs.Add(theSong);
                                        _context.SaveChanges();

                                        string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                        using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                        {
                                            wr.WriteLine("==========================");
                                            wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                        }
                                        Console.WriteLine("Song Is Saved!");
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
                                        Console.WriteLine("Choose Song ID To Add To The Playlist!");

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

                                            choosenEmail.songs.Add(theSong);
                                            _context.SaveChanges();

                                            string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                            using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                            {
                                                wr.WriteLine("==========================");
                                                wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                            }
                                            Console.WriteLine("Song Is Saved!");
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
                                            Console.WriteLine("Choose Song ID To Add To The Playlist!");

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

                                                choosenEmail.songs.Add(theSong);
                                                _context.SaveChanges();

                                                string fullRoute = Path.Combine(baseRoute, $"{choosenEmail.Nickname}-Logs.txt");

                                                using (StreamWriter wr = new StreamWriter(fullRoute, true))
                                                {
                                                    wr.WriteLine("==========================");
                                                    wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Added To Playlist - {DateTime.Now}");

                                                }
                                                Console.WriteLine("Song Is Saved!");
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
                        chooseSong();
                    }
                    else if (choice == 2)
                    {
                        void PlaylistSongPlay()
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Janitor();
                            Line();
                            Console.WriteLine("Here Is Full Playlist, Choose Song ID To Play");
                            var allSongs = _context.Songs
                                                   .Include(s => s.users)
                                                   .Include(s => s.album)
                                                   .Include(s => s.album.artist)
                                                   .ToList();
                            foreach (var allUsers in allSongs)
                            {
                                var users = allUsers.users.ToList();
                                foreach (var user in users)
                                {
                                    if (user.Id == choosenEmail.Id)
                                    {                                        
                                        var userSongs = user.songs.ToList();

                                        foreach(var songs in userSongs)
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
                                }

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
                                    wr.WriteLine($"[LOG] Song {choosenSong.Title} Has Been Played By User {choosenEmail.Nickname} - {DateTime.Now}");

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
                        PlaylistSongPlay();
                    }
                    else if (choice == 3)
                    {
                        void AllPlaylist()
                        {
                            Console.BackgroundColor = ConsoleColor.DarkYellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Janitor();
                            Line();
                            Console.WriteLine("Here Is Full Playlist");

                            TimeSpan allDuration = TimeSpan.Zero;

                            var userSongs = _context.Users
                                .Where(u => u.Id == choosenEmail.Id)
                                .SelectMany(u => u.songs)
                                .Include(s => s.album)
                                    .ThenInclude(a => a.artist)
                                .ToList();

                            foreach (var song in userSongs)
                            {
                                Line();
                                Console.WriteLine($"{song.album.artist.Name} - {song.Title}");
                                Console.WriteLine($"{song.Duration}");
                                allDuration += song.Duration;
                                Line();
                            }

                            Console.WriteLine($"Total Playlist Duration: {allDuration}");
                        }
                        AllPlaylist();
                    }
                    else if (choice == 4)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Sending Playlist To Mail...");

                        List<Song> AllUserSongs = _context.Users
                                                            .Where(u => u.Id == choosenEmail.Id)
                                                            .SelectMany(u => u.songs)
                                                            .Include(s => s.album)
                                                                .ThenInclude(a => a.artist)
                                                            .ToList();


                        TimeSpan allDuration = TimeSpan.Zero;

                        var emailBody = new StringBuilder();
                        emailBody.AppendLine("<!DOCTYPE html><html>");
                        emailBody.AppendLine("<head>\r\n    <style>\r\n        .email-container {\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            font-family: 'Arial', sans-serif;\r\n            background: #111827;\r\n            color: #fff;\r\n            padding: 30px;\r\n            border-radius: 16px;\r\n        }\r\n        .header {\r\n            background: linear-gradient(135deg, #3b82f6, #8b5cf6);\r\n            padding: 25px;\r\n            text-align: center;\r\n            border-radius: 12px;\r\n            margin-bottom: 30px;\r\n        }\r\n        .playlist-container {\r\n            background: rgba(255,255,255,0.05);\r\n            border-radius: 12px;\r\n            padding: 20px;\r\n            margin: 20px 0;\r\n        }\r\n        .song-item {\r\n            display: flex;\r\n            justify-content: space-between;\r\n            align-items: center;\r\n            padding: 15px;\r\n            margin: 10px 0;\r\n            background: rgba(255,255,255,0.03);\r\n            border-radius: 8px;\r\n            border: 1px solid rgba(255,255,255,0.1);\r\n            transition: transform 0.2s;\r\n        }\r\n        .song-item:hover {\r\n            transform: translateX(5px);\r\n            background: rgba(255,255,255,0.05);\r\n        }\r\n        .song-info {\r\n            flex-grow: 1;\r\n        }\r\n        .artist-name {\r\n            color: #8b5cf6;\r\n            font-weight: bold;\r\n            margin-bottom: 4px;\r\n        }\r\n        .song-title {\r\n            color: #e5e7eb;\r\n        }\r\n        .duration {\r\n            color: #9ca3af;\r\n            font-size: 0.9em;\r\n            padding-left: 15px;\r\n        }\r\n        .total-duration {\r\n            text-align: right;\r\n            padding: 20px;\r\n            background: rgba(59, 130, 246, 0.1);\r\n            border-radius: 8px;\r\n            margin-top: 20px;\r\n            font-weight: bold;\r\n            color: #3b82f6;\r\n        }\r\n        .greeting {\r\n            font-size: 1.2em;\r\n            color: #e5e7eb;\r\n            margin-bottom: 20px;\r\n        }\r\n    </style>\r\n</head>");
                        emailBody.AppendLine("<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <h1>Your Music App Playlist</h1>\r\n        </div>");
                        emailBody.AppendLine($"<div class=\"greeting\">Hello {choosenEmail.Nickname}, Here Is Your Playlist:</div>");
                        emailBody.AppendLine("<div class=\"playlist-container\">");

                        foreach (var songs in AllUserSongs)
                        {
                            emailBody.AppendLine("<div class=\"song-item\">");
                            emailBody.AppendLine("<div class=\"song-info\">");
                            emailBody.AppendLine($"<div class=\"artist-name\">{songs.album.artist.Name}</div>");
                            emailBody.AppendLine($"<div class=\"song-title\">{songs.Title}</div>");
                            emailBody.AppendLine("</div>");
                            emailBody.AppendLine($"<div class=\"duration\">{songs.Duration}</div>");
                            emailBody.AppendLine("</div>");
                            allDuration = allDuration + songs.Duration;
                        }
                        emailBody.AppendLine("</div>");
                        emailBody.AppendLine($"<div class=\"total-duration\">Total Duration: {allDuration}</div>");
                        emailBody.AppendLine("</div></body></html>");

                        SMTPService.SendEmail(choosenEmail.Email, "Music App Playlist", emailBody.ToString() );
                        Console.WriteLine("Mail Sended!");
                    }
                    else if (choice == 5)
                    {
                        void RemoveSong()
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Janitor();
                            Line();
                            Console.WriteLine("Here Is Full Playlist");                           

                            var userSongs = _context.Users
                                .Where(u => u.Id == choosenEmail.Id)
                                .SelectMany(u => u.songs)
                                .Include(s => s.album)
                                    .ThenInclude(a => a.artist)
                                .ToList();

                            foreach (var song in userSongs)
                            {
                                Line();
                                Console.WriteLine($"ID -> {song.Id}");
                                Console.WriteLine($"{song.album.artist.Name} - {song.Title}");
                                Console.WriteLine($"{song.Duration}");
                                Line();
                            }

                            Console.WriteLine("Choose ID To Remove Song");

                            if (int.TryParse(Console.ReadLine(), out int songId))
                            {
                                var user = _context.Users
                                    .Include(u => u.songs)
                                    .FirstOrDefault(u => u.Id == choosenEmail.Id);

                                var songToRemove = user?.songs.FirstOrDefault(s => s.Id == songId);

                                if (songToRemove != null)
                                {
                                    user.songs.Remove(songToRemove);
                                    _context.SaveChanges();
                                    Console.WriteLine("Song removed successfully");
                                }
                                else
                                {
                                    Console.WriteLine("Song not found");
                                }
                            }
                        }
                        RemoveSong();
                    }
                    else if (choice == 6)
                    {
                        Janitor();
                        Line();
                        Console.WriteLine("Logging Off...");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Please Enter Valid Number!");
                        Console.WriteLine("Valid Number Is Between 1 And 6!");
                    }
                }
                while (true)
                {
                    userMenu();
                }
            }
        }
    }

}
