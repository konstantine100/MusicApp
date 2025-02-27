# Music Management Application

A console-based music management application developed using C# and Entity Framework. The app allows users to manage music data, including creating artists, albums, and songs, with additional features like playback simulation, user management, and playlist export.

## Features
- **Artist Management**: Create artists and set artist descriptions.
- **Album and Song Management**: Add albums and songs to the application.
- **Song Playback Simulation**: Simulate song playback within the app.
- **Search, Filter, and Sort**: Search and filter artists, albums, and songs, and sort them based on various criteria.
- **User Management**: Users can create accounts, with password encryption via bcrypt and email verification through an SMTP system.
- **Playlist Creation and Export**: Users can create playlists and export them via email.

## Technologies Used
- **C#**: Core programming language used to develop the application.
- **Entity Framework**: Used for data access and ORM (Object-Relational Mapping) to interact with a SQL Server database.
- **SQL Server**: Local database for storing user and music data.
- **bcrypt**: Used for secure password encryption.
- **SMTP**: For email verification and playlist export functionality.

## Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/konstantine100/MusicApp.git
