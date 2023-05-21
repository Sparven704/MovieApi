# MovieApi

This is a a REST API that I created as a assignment for Chas Academy. With this API you can call on 7 different endpoints:

    /api/Genre/{personName} - This endpoint you enter the name of the person you want to get all their liked genres linked to them.
    /api/Movie/{personName} - This endpoint you enter the name of the person you want to get all the movies linked to the person.
    /api/Movie/{movieId}/rating/{personId}/{newRating} - Here you enter a persons id, movie id and then the new rating of the correspondings persons movie.
    /api/Movie - This endpoint will let you create a new movie object .
    /api/Person/GetAllPeople - This endpoint will let you get all people in the system.
    /api/PersonGenre - This endpoint will let you link a genre to a person.
    /api/TMDB/genres/{genreName}/movies - This one will call on a external endpoint to get recommended movies for the genre you enter.
