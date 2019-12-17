import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { EventEmitterService } from '../event-emitter.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.css']
})
export class MovieDetailsComponent implements OnInit {
  private routeSub;
  private imdbID: string;
  private movie: Movie;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
    private route: ActivatedRoute, 
    private router: Router,
    private eventEmitterService: EventEmitterService) { }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.imdbID = params['id'];
      this.http.get<Movie>("https://www.omdbapi.com/", {
        params: {
          apikey: "281cdd33",
          i: this.imdbID,
          plot: "full",
          tomatoes: "true"
        }})
      .subscribe(result => {
        this.movie = result;
      }, error => console.error(error));
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  addMovieToMovieList() {
  //   this.http.post<MoviePost>(this.baseUrl + 'movie', {
  //     Name: this.movie.Title,
  //     imdbID: this.movie.imdbID,
  //     MovieListRefId: 11
  //   })
  // .subscribe(result => {
  //   // debugger;
  //     alert("added " + this.movie.Title);
  //     // this.listName = "";
  //     // this.router.navigateByUrl('/my-lists');
  //   }, error => console.error(error));
  }

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
