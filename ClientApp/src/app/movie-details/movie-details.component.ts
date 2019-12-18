import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { EventEmitterService } from '../event-emitter.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  private routeSub;
  private imdbID: string;
  private omdbMovie: Movie;
  private tmdbMovieFind: TMDBMovieFind;
  private tmdbMovie: TMDBMovie;
  private found: boolean;
  private receivedChildId: string;
  public isAuthenticated: Observable<boolean>;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
    private route: ActivatedRoute, 
    private router: Router,
    private eventEmitterService: EventEmitterService,
    private authorizeService: AuthorizeService) {
      this.found = true;
     }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.routeSub = this.route.params.subscribe(params => {
      this.imdbID = params['id'];

      if (this.imdbID == "undefined") {
        this.found = false;
      }
      else {
        this.getOMDBListing();
        this.getTMDBListing();
      }
      
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  getOMDBListing() {
    this.http.get<Movie>("https://www.omdbapi.com/", {
        params: {
          apikey: "281cdd33",
          i: this.imdbID,
          plot: "full",
          tomatoes: "true"
        }})
      .subscribe(result => {
        this.omdbMovie = result;
      }, error => console.error(error));
  }

  getTMDBListing() {
    this.http.get<TMDBMovieFind>(`https://api.themoviedb.org/3/find/${this.imdbID}`, {
        params: {
          api_key: "f28df3fec9ce98f371cc2a6636044a45",
          external_source: "imdb_id"
        }})
      .subscribe(result => {
        this.tmdbMovieFind = result;
        this.tmdbMovie = this.tmdbMovieFind.movie_results[0] || null;
      }, error => console.error(error));
  }

  addMovieToMovieList(id: string) {
    this.http.post<MoviePost>(this.baseUrl + 'movie', {
      Name: this.omdbMovie.Title,
      imdbID: this.omdbMovie.imdbID,
      MovieListRefId: parseInt(id),
      PosterURL: this.tmdbMovie.poster_path
    })
  .subscribe(result => {
    // debugger;
      alert("added " + this.omdbMovie.Title);
      // this.listName = "";
      // this.router.navigateByUrl('/my-lists');
    }, error => console.error(error));
  }

  getMessage(id: string) {
    this.receivedChildId = id;
    this.addMovieToMovieList(this.receivedChildId);
  }

}

interface TMDBMovieFind {
  movie_results: TMDBMovie[];
  person_results: any[];
  tv_results: any[];
  tv_episode_results: any[];
  tv_season_results: any[];
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

interface MoviePost {
  Name: string;
  imdbId: string;
  MovieListRefId: number;
}

interface Movie {
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
