namespace MusicApp.Models;

internal class TopThreeGenres
{
    public string Genre { get; set; }
    public int Played { get; set; }
    public TopThreeGenres(string genre, int played)
    {
        Genre = genre;
        Played = played;
    }
}
