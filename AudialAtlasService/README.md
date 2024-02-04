# AudialAtlasService

This is a group project and a school assingment to develop a music API.
We had tons of fun developing this, and it gave us all new insight of API Development in general.

Flow of things: Client API Calls -> Endpoint -> Handler -> Repo -> Back to client with programmed result.

We followed the design of repository pattern, with focus on the concept of IoC.
We have repositories that fetches data from the database, and pushes it to our Handlers. Our Handlers then give a IResult based on result back to endpoint.

We have DTO to fetch data from calls, and ViewModels to show limited designed data back.
