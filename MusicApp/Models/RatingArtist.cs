using System.Diagnostics.Metrics;

namespace MusicApp.Models;

internal class RatingArtist
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Country { get; set; }
    public int Year { get; set; }
    public string Website { get; set; }
    public int Albums { get; set; }
    public bool Activity { get; set; }
    public string Description { get; set; }
    public decimal Rating { get; set; }

    public RatingArtist(string name, string country, string genre, int year, string website, int albums, bool activity, string description, decimal rating)
    {
        Name = name;
        Country = country;
        Genre = genre;
        Year = year;
        Website = website;
        Albums = albums;
        Activity = activity;
        Description = description;
        Rating = rating;
    }
}
