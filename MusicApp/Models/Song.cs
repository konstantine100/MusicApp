using MusicApp.Models;
//namespace MusicApp.Models;
//internal class Song
//{
//    public int Id { get; set; }
//    public int AlbumId { get; set; }
//    public string Title { get; set; }
//    public TimeSpan Duration { get; set; }
//    public string Lyrics { get; set; }
//    public int TrackNumber { get; set; }
//    public int TimesPlayed { get; set; }
//    public Album album { get; set; }
//    public List<User> users { get; set; }
//    private void DrawBar(int height)
//    {
//        for (int i = 0; i < height; i++)
//        {
//            Console.Write("█");
//        }
//        Console.Write(" ");
//    }
//    private void Line()
//    {
//        Console.WriteLine("=============================================");
//    }
//    private void DrawProgressBar(int current, int total)
//    {
//        int barLength = 50;
//        int progress = (int)((float)current / total * barLength);
//        Console.Write("[");
//        for (int i = 0; i < barLength; i++)
//        {
//            if (i < progress)
//                Console.Write("=");
//            else
//                Console.Write(" ");
//        }
//        Console.Write($"] {current}/{total}s");
//    }
//    private void DrawVisualizer()
//    {
//        Random rnd = new Random();
//        for (int i = 0; i < 10; i++)
//        {
//            int height = rnd.Next(1, 15);
//            DrawBar(height);
//        }
//        Console.WriteLine();
//    }
//    public void PlaySongVisualization(Song song)
//    {
//        int duration = (int)song.Duration.TotalSeconds;
//        for (int i = 0; i <= duration; i++)
//        {
//            Console.Clear();
//            Line();
//            Console.WriteLine($"Now Playing: {song.album.artist.Name} - {song.Title}");
//            Line();
//            Console.WriteLine($"Song Lyrics: {song.Lyrics}");
//            Line();
//            Console.WriteLine();
//            DrawVisualizer();
//            DrawProgressBar(i, duration);
//            Thread.Sleep(1000);

//        }
//    }
//}

using VideoLibrary;
using NAudio.Wave;
using System.Diagnostics;

namespace MusicApp.Models;

internal class Song
{
    private string cacheFolder = "songCache";
    private string ffmpegPath = "ffmpeg.exe";
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string Title { get; set; }
    public TimeSpan Duration { get; set; }
    public string Lyrics { get; set; }
    public int TrackNumber { get; set; }
    public int TimesPlayed { get; set; }
    public Album album { get; set; }
    public List<User> users { get; set; }

    private void DrawBar(int height)
    {
        for (int i = 0; i < height; i++)
        {
            Console.Write("█");
        }
        Console.Write(" ");
    }

    private void Line()
    {
        Console.WriteLine("=============================================");
    }

    private void DrawProgressBar(int current, int total)
    {
        int barLength = 50;
        int progress = (int)((float)current / total * barLength);
        Console.Write("[");
        for (int i = 0; i < barLength; i++)
        {
            if (i < progress)
                Console.Write("=");
            else
                Console.Write(" ");
        }
        Console.Write($"] {current}/{total}s");
    }

    private void DrawVisualizer()
    {
        Random rnd = new Random();
        for (int i = 0; i < 10; i++)
        {
            int height = rnd.Next(1, 15);
            DrawBar(height);
        }
        Console.WriteLine();
    }

    private async Task<string> DownloadYouTubeAudio(string songTitle, string artistName)
    {
        try
        {
            string searchQuery = $"{artistName} {songTitle}";
            var youtube = YouTube.Default;
            var video = await youtube.GetVideoAsync($"ytsearch:{searchQuery}");

            Directory.CreateDirectory(cacheFolder);
            string videoFile = Path.Combine(cacheFolder, $"{video.FullName}.mp4");
            string audioFile = Path.Combine(cacheFolder, $"{video.FullName}.mp3");

            if (!File.Exists(audioFile))
            {
                Console.WriteLine("Downloading video...");
                var videoBytes = video.GetBytes();
                await File.WriteAllBytesAsync(videoFile, videoBytes);

                Console.WriteLine("Converting to MP3...");
                using (var process = new Process())
                {
                    process.StartInfo.FileName = ffmpegPath;
                    process.StartInfo.Arguments = $"-i \"{videoFile}\" -q:a 0 -map a \"{audioFile}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    await process.WaitForExitAsync();
                }

                if (File.Exists(videoFile))
                {
                    File.Delete(videoFile);
                }
            }

            return audioFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download audio: {ex.Message}");
            return null;
        }
    }


    private IWavePlayer CreateAudioPlayer(string audioFile)
    {
        var audioFileReader = new AudioFileReader(audioFile);
        var waveOutEvent = new WaveOutEvent();
        waveOutEvent.Init(audioFileReader);
        return waveOutEvent;
    }

    public async void PlaySongVisualization(Song song)
    {
        string audioFile = await DownloadYouTubeAudio(song.Title, song.album.artist.Name);
        IWavePlayer audioPlayer = null;

        if (audioFile != null)
        {
            try
            {
                audioPlayer = CreateAudioPlayer(audioFile);
                audioPlayer.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to play audio: {ex.Message}");
            }
        }

        int duration = (int)song.Duration.TotalSeconds;
        for (int i = 0; i <= duration; i++)
        {
            Console.Clear();
            Line();
            Console.WriteLine($"Now Playing: {song.album.artist.Name} - {song.Title}");
            Line();
            Console.WriteLine($"Song Lyrics: {song.Lyrics}");
            Line();
            Console.WriteLine();
            DrawVisualizer();
            DrawProgressBar(i, duration);
            Thread.Sleep(1000);
        }

        audioPlayer?.Stop();
        audioPlayer?.Dispose();
    }
}