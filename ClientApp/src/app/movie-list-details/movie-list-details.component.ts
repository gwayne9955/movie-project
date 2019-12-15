import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-movie-list-details',
  templateUrl: './movie-list-details.component.html',
  styleUrls: ['./movie-list-details.component.css']
})
export class MovieListDetailsComponent implements OnInit, OnDestroy {
  private routeSub;
  private movieList: MovieList;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute) { }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
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

}

interface MovieList {
  movieListId: number;
  name: string;
  // Movies: 
}
