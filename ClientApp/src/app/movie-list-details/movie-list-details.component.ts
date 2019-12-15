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
  private routeSub;
  private id: number;
  private movieList: MovieList;
  private movieListEditName: string;
  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
    private route: ActivatedRoute, 
    private router: Router,
    private eventEmitterService: EventEmitterService) { }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id'];
      // console.log(params); // log the entire params object
      // console.log(params['id']); // log the value of id
      this.http.get<MovieList>(this.baseUrl + 'movielist/' + params['id'])
      .subscribe(result => {
        this.movieList = result;
      }, error => console.error(error));
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  editListName() {
    this.http.put<MovieList>(this.baseUrl + 'movielist/' + this.id, {
      Name: this.movieListEditName
    })
      .subscribe(result => {
        this.movieList = result;
        this.movieListEditName = "";
      }, error => console.error(error));
  }

  deleteMovieList() {
    if (confirm("Are you sure you want to delete this movie list?"))
    {
      this.http.delete<MovieList>(this.baseUrl + 'movielist/' + this.id)
        .subscribe(result => {
          alert("MovieList '" + result.name + "' deleted");
          // this.eventEmitterService.onMyListsComponentButtonClick();
          this.router.navigateByUrl('/my-lists');
        }, error => console.error(error));
    }
  }
}

interface MovieList {
  movieListId: number;
  name: string;
  // Movies: 
}
