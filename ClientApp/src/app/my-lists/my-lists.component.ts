import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-my-lists',
  templateUrl: './my-lists.component.html',
  styleUrls: ['./my-lists.component.css']
})
export class MyListsComponent implements OnInit {
  public movieLists: MovieListResponse;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<MovieListResponse>(this.baseUrl + 'movielist').subscribe(result => {
      this.movieLists = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

interface MovieListResponse {
  items: MovieList[];
  totalItems: number;
}

interface MovieList {
  movieListId: number;
  name: string;
  // Movies: 
}
