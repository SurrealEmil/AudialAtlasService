﻿// <auto-generated />
using AudialAtlasService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AudialAtlasService.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ArtistGenre", b =>
                {
                    b.Property<int>("ArtistsArtistId")
                        .HasColumnType("int");

                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsArtistId", "GenresGenreId");

                    b.HasIndex("GenresGenreId");

                    b.ToTable("ArtistGenre");
                });

            modelBuilder.Entity("ArtistUser", b =>
                {
                    b.Property<int>("ArtistsArtistId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsArtistId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("ArtistUser");
                });

            modelBuilder.Entity("AudialAtlasService.Models.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("AudialAtlasService.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("AudialAtlasService.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongId"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("SongTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("AudialAtlasService.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("SongsSongId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "SongsSongId");

                    b.HasIndex("SongsSongId");

                    b.ToTable("GenreSong");
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("GenreUser");
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.Property<int>("SongsSongId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("SongsSongId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("SongUser");
                });

            modelBuilder.Entity("ArtistGenre", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudialAtlasService.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistUser", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudialAtlasService.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudialAtlasService.Models.Song", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudialAtlasService.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsSongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreUser", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudialAtlasService.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SongUser", b =>
                {
                    b.HasOne("AudialAtlasService.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsSongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AudialAtlasService.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudialAtlasService.Models.Artist", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
