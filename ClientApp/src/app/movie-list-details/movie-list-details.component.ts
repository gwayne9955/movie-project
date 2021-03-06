import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { EventEmitterService } from '../event-emitter.service';

@Component({
  selector: 'app-movie-list-details',
  templateUrl: './movie-list-details.component.html',
  styleUrls: ['./movie-list-details.component.css']
})
export class MovieListDetailsComponent implements OnInit, OnDestroy {
  public routeSub;
  public id: number;
  public movieList: MovieList;
  public movieListEditName: string;
  public receivedChildListName: string;
  public movies;
  public newArray;
  public columns: number;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router,
    private eventEmitterService: EventEmitterService) {
    this.newArray = [];
    this.columns = 4;
    this.movies = [];
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id'];
      this.getMovieList();
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  getMovieList() {
    this.http.get<MovieList>(this.baseUrl + 'movielist/' + this.id)
      .subscribe(result => {
        this.movieList = result;
        this.newArray = [];
        for (let i = 0; i < this.movieList.movies.length; i += this.columns) {
          this.newArray.push({ items: this.movieList.movies.slice(i, i + this.columns) });
        }
      }, error => alert(error.error));
  }

  editListName() {
    this.http.put<MovieList>(this.baseUrl + 'movielist/' + this.id, {
      Name: this.receivedChildListName
    })
      .subscribe(result => {
        this.movieList = result;
      }, error => alert(error.error));
  }

  deleteMovieList() {
    if (confirm("Are you sure you want to delete this movie list?")) {
      this.http.delete<MovieList>(this.baseUrl + 'movielist/' + this.id)
        .subscribe(result => {
          alert("MovieList '" + result.name + "' deleted");
          this.router.navigateByUrl('/my-lists');
        }, error => alert(error.error));
    }
  }

  deleteMovieFromList(imdbID: string) {
    this.http.delete<Movie>(this.baseUrl + `movie/${this.id}/${imdbID}`)
      .subscribe(result => {
        this.getMovieList();
        alert("Movie '" + result.name + "' deleted");
      }, error => alert(error.error));
  }

  getListName(listName: string) {
    this.receivedChildListName = listName;
    if (this.receivedChildListName != this.movieList.name && this.receivedChildListName.length > 0) {
      this.editListName();
    }
  }

  toggleMovieWatched(imdbID: string, beforeWatchedStatus: number) {
    var newWatched = 1;
    if (beforeWatchedStatus == 1) {
      newWatched = 0;
    }
    this.http.put<Movie>(this.baseUrl + `movie/${this.id}/${imdbID}`, {
      Watched: newWatched
    })
      .subscribe(result => {
        this.getMovieList();
      }, error => alert(error.error));
  }

}
