namespace MusicApp.Models;

internal class UserSong
{
    public int UserId { get; set; }
    public User user { get; set; }
    public int SongId { get; set; }
    public Song Song { get; set; }
}
