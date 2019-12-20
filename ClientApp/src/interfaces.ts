interface MovieListResponse {
    items: MovieList[];
    totalItems: number;
}

interface Movie {
    movieListRefID: number;
    name: string;
    posterURL: string;
    watched: number;
    imdbID: string;
}

interface MovieList {
    movieListId: number;
    name: string;
    movies: Movie[];
}

interface TMDBResponse {
    page: number;
    total_results: number;
    total_pages: number;
    results: TMDBMovie[];
}

interface TMDBMovie {
    popularity: number;
    vote_count: number;
    video: boolean;
    poster_path: string;
    id: number;
    adult: boolean;
    backdrop_path: string;
    original_language: string;
    original_title: string;
    genre_ids: number[];
    title: string;
    vote_average: number;
    overview: string;
    release_date: string;
}

interface OMDBMovieSearchTitle {
    Title: string;
    Year: string;
    Rated: string;
    Released: string;
    Runtime: string;
    Genre: string;
    Director: string;
    Writer: string;
    Actors: string;
    Plot: string;
    Language: string;
    Country: string;
    Awards: string;
    Poster: string;
    Ratings: number[];
    Metascore: string;
    imdbRating: string;
    imdbVotes: string;
    imdbID: string;
    Type: string;
    DVD: string;
    BoxOffice: string;
    Production: string;
    Website: string;
    Response: string;
}

interface TMDBMovieFind {
    movie_results: TMDBMovie[];
    person_results: any[];
    tv_results: any[];
    tv_episode_results: any[];
    tv_season_results: any[];
}

interface MoviePost {
    Name: string;
    imdbId: string;
    MovieListRefId: number;
}

interface OMDBMovie {
    Title: string;
    Year: string;
    Rated: string;
    Released: string;
    Runtime: string;
    Genre: string;
    Director: string;
    Writer: string;
    Actors: string;
    Plot: string;
    Language: string;
    Country: string;
    Awards: string;
    Poster: string;
    Ratings: [
        {
            Source: string;
            Value: string
        },
        {
            Source: string;
            Value: string
        },
        {
            Source: string;
            Value: string
        }
    ];
    Metascore: string;
    imdbRating: string;
    imdbVotes: string;
    imdbID: string;
    Type: string;
    DVD: string;
    BoxOffice: string;
    Production: string;
    Website: string;
    Response: string;
}
