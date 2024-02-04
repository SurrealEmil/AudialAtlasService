# Audial Atlas

<img src="/AudialAtlasService/Images/audial_atlas_logo.jpg">




## Intro

Welcome to the Audial Atlas music API. This is a group project and a school assignment to develop a music API. We had tons of fun developing this and it gave us all new insights of API Development in general.

#### Program flow:
| Client API Call → Endpoint → Handler → Repo → Back to client with fetched result |
|----------------------------------------------------------------------------------|

<br>

We followed the design of repository pattern, with focus on the concept of IoC. We have repositories that fetches data from the database, and sends it to our Handlers. Our Handlers then give an IResult based on result back to endpoint.

We use DTOs to send data via calls, and ViewModels to return limited designed data back.

<br>

## Features

#### Storage: 

    Users with unique IDs and passwords.
    Songs, Artists, Genres.
    Favorites: Songs, Artists, Genres.


#### Retrieval:

    All songs, artists and genres.
    All favorites: songs, artists and genres.
    Get top 5 songs from artist.


<br>


## Prerequisites

#### Framework
    .NET 6 SDK

#### AudialAtlasService

    EntityFrameworkCore (v6.0.26)
    EntityFrameworkCore.SqlServer (v6.0.26)
    EntityFrameworkCore.Tools (v6.0.26)
    
#### AudialAtlasServiceClient
    Newtonsoft.Json (v13.0.3)
    
#### AudialAtlasServiceTest

    EntityFrameworkCore (v6.0.26)
    EntityFrameworkCore.InMemory (v6.0.26)
    NET.Test.Sdk (v17.5.0)
    MSTest.TestAdapter (v2.2.10)
    MSTest.TestFramework (v2.2.10)

#### Tools
    An API client such as Insomnia or Postman

<br>


## Audial Atlas API and Endpoints

The API is a minimal API using inversion of control and dependency injection. The endpoints are mostly self explanatory. 

These are the endpoints that currently exist:

#### GET User

    /users/allusers                         Displays all users
    /users/{userId}/artists                 Displays all artists liked by user
    /users/{userId}/genres                  Displays all genres liked by user
    /users/{userId}/songs                   Displays all sings liked by user
    /users/{userName}/check                 Checks if a user already exists
    /users/login/{userName}/{password}      Authenticates a user, and allows them to log in


#### POST User

    /users                      Adds user to database
    /users/search               Searches for user by user name
    /users/connect-to-artist    “Likes” an artist,
    /users/connect-to-genre     “Likes” a genre,
    /users/connect-to-song      “Likes” a song,


#### GET Song

    /songs              Displays all songs in database
    /songs/{songId}     Displays one song


#### POST Song

    /artists/{artistId}/genres/{genreId}/songs    Adds song to database
    
    (Requires both an artist and a genre)


#### GET Artist

    /artists                Displays all artists in database
    /artists/{artistId}     Displays one artist


#### POST Artist

    /artists - Adds artist to database
    
    
#### GET Genre

    /genres                     Displays all genres
    /genres/{genreId}           Displays one genre
    /artists/genres/{genreId}   Displays all artists linked to genre
    /songs/genres/{genreId}     Displays all songs linked to genre


#### POST Genre

    /genres    Adds genre to database


#### Linking endpoints

    /artists/{artistId}/genres/{genreId}    Links a genre to an artist via POST
    /songs/{songId}/genres/{genreId}        Links a genre to a song via POST


<br>


## External API

Part of the assignment was to include an external API in our own API. We used the Deezer API. In retrospect we should have implemented the Deezer API earlier in development. This would have allowed us to build our models around the data Deezer returns. As it currently stands we use the Deezer API to get the top five songs of an artist via searching for the artists name. Even though Deezer has most artists, some artists are not searchable via our API. The calls still work, but Deezer doesn’t return a result for those artists. 

#### Endpoint

    /deezer/{artistNameQuery}/topfivesongs


#### Documentation

[Deezer Guidelines](https://developers.deezer.com/guidelines/getting_started )


<br>


## Console client

We use a simple console client located in a separate project to make calls to our API and then display the info to the user. 

The client is detached from the main project to simulate external calls to the API. 

From the main screen the user is able to either log in or sign up for the service. For convenience the base URL for the API is provided by <i>ConfigurationBuilder</i> via <i>appsettings.Development.json</i> located in the main project.


<br>


## Testing

We use MSTest in our program and are currently able to test Genre endpoints and Song endpoints with Artist endpoints being looked at.

For testing we use InMemory Database.


<br>


## Credits

Filip Nilsson - [filip-io](https://github.com/filip-io)
<br>Dennis Briffa - [Balos87](https://github.com/Balos87)
<br>Emil Ejderklev - [SurrealEmil](https://github.com/SurrealEmil)
<br>Pontus Ahlbäck - [PAhlback](https://github.com/PAhlback)
