namespace MusicApp.Models;

internal class MostAlbums
{

    public string Artist { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    public decimal Rating { get; set; }
    public int RatingParticipant { get; set; }
    public decimal OverAllScore { get; set; }
    public TimeSpan AlbumLength { get; set; }
    public int AllListens { get; set; }

    public MostAlbums(string artist, string title, int releaseYear, string genre, decimal rating, int ratingParticipant, decimal overAllScore, TimeSpan albumLength, int allListens)
    {
        Artist = artist;
        Title = title;
        ReleaseYear = releaseYear;
        Genre = genre;
        Rating = rating;
        RatingParticipant = ratingParticipant;
        OverAllScore = overAllScore;
        AlbumLength = albumLength;
        AllListens = allListens;
    }
}
