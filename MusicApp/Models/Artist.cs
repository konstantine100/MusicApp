﻿using MusicApp.Models;
namespace MusicApp.Models;

internal class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public List<Album> Albums { get; set; }
    public ArtistDetails ArtistDetails { get; set; }

}
