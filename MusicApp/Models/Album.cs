using MusicApp.Models;
namespace MusicApp.Models;

internal class Album
{
    public int Id { get; set; }
    public int ArtistId { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    public decimal Rating { get; set; } 
    public int RatingParticipant { get; set; }
    public decimal OverAllScore { get; set; }
    public TimeSpan AlbumLength { get; set; }
    public Artist artist { get; set; }  
    public List<Song> songs { get; set; }
}
