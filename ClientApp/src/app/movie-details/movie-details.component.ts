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
  private omdbMovie: OMDBMovie;
  private tmdbMovieFind: TMDBMovieFind;
  private tmdbMovie: TMDBMovie;
  private found: boolean;
  private receivedChildId: string;
  public isAuthenticated: Observable<boolean>;
  public rottenTomatoRating: string;
  public rating: string;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router,
    private eventEmitterService: EventEmitterService,
    private authorizeService: AuthorizeService) {
    this.found = true;
    this.rottenTomatoRating = "";
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
    this.http.get<OMDBMovie>("https://www.omdbapi.com/", {
      params: {
        apikey: "281cdd33",
        i: this.imdbID,
        plot: "full",
        tomatoes: "true"
      }
    })
      .subscribe(result => {
        // debugger;
        this.omdbMovie = result;
        if (this.omdbMovie.Ratings.length > 0) {
          if (this.omdbMovie.Ratings.length > 1 && this.omdbMovie.Ratings[1].Source == "Rotten Tomatoes") {
            this.getRottenTomatoRating(this.omdbMovie.Ratings[1].Value);
            this.rating = this.omdbMovie.Ratings[1].Value;
          }
          else {
            this.rating = this.omdbMovie.Ratings[0].Value;
          }
        }

      }, error => console.error(error));
  }

  getTMDBListing() {
    this.http.get<TMDBMovieFind>(`https://api.themoviedb.org/3/find/${this.imdbID}`, {
      params: {
        api_key: "f28df3fec9ce98f371cc2a6636044a45",
        external_source: "imdb_id"
      }
    })
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
        alert("added " + this.omdbMovie.Title);
      }, error => console.error(error));
  }

  getRottenTomatoRating(value: string) {
    var percentage = parseInt(value.substring(0, value.length - 1));
    if (percentage < 60 && percentage > 0) {
      this.rottenTomatoRating = "splat";
    }
    else if (percentage < 75) {
      this.rottenTomatoRating = "fresh";
    }
    else {
      this.rottenTomatoRating = "certified";
    }
  }

  getMessage(id: string) {
    this.receivedChildId = id;
    this.addMovieToMovieList(this.receivedChildId);
  }

}