using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
namespace MusicApp.Data;

internal class DataContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistDetails> ArtistsDetails { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>()
            .HasOne(a => a.ArtistDetails)
            .WithOne(ad => ad.Artist)
            .HasForeignKey<ArtistDetails>(ad => ad.ArtistId)
            .IsRequired();
        // Artist - Album (One-to-Many)
        modelBuilder.Entity<Album>()
            .HasOne(al => al.artist)
            .WithMany(a => a.Albums)
            .HasForeignKey(al => al.ArtistId)
            .IsRequired();

        // Album - Song (One-to-Many)
        modelBuilder.Entity<Song>()
            .HasOne(s => s.album)
            .WithMany(al => al.songs)
            .HasForeignKey(s => s.AlbumId)
            .IsRequired();

        // Song - User (Many-to-Many)
        modelBuilder.Entity<Song>()
            .HasMany(s => s.users)
            .WithMany(u => u.songs)
            .UsingEntity<Dictionary<string, object>>(
                "UserSongs",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Song>()
                    .WithMany()
                    .HasForeignKey("SongId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UserId", "SongId");
                    j.ToTable("UserSongs");
                });
    }
}
