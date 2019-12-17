import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private tmdbResponse: TMDBResponse;
  private omdbResponse: MovieSearchResult;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
  private route: ActivatedRoute, 
  private router: Router) {
    this.getPopularMovies();
  }

  getPopularMovies() {
    this.http.get<TMDBResponse>("https://api.themoviedb.org/3/discover/movie", {
        params: {
          api_key: "f28df3fec9ce98f371cc2a6636044a45",
          sort_by: "popularity.desc"
        }})
      .subscribe(result => {
        this.tmdbResponse = result;
      }, error => console.error(error));
  }

  popularMovieClick(title: string) {
    this.http.get<MovieSearchResult>("https://www.omdbapi.com/", {
        params: {
          apikey: "281cdd33",
          s: title
        }})
      .subscribe(result => {
        this.omdbResponse = result;
        this.router.navigateByUrl(`/movie/${this.omdbResponse.Search[0].imdbID}`);
      }, error => console.error(error));
  }
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

interface MovieSearchResult {
  Search: MovieSearchListing[];
  totalResults: string;
  Response: string;
}

interface MovieSearchListing {
  Title: string;
  Year: string;
  imdbID: string;
  Type: string;
  Poster: string;
}