import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-my-lists',
  templateUrl: './my-lists.component.html',
  styleUrls: ['./my-lists.component.css']
})
export class MyListsComponent implements OnInit {
  public movieLists: MovieList[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http.get<MovieList[]>(this.baseUrl + 'movielist').subscribe(result => {
      debugger;
      this.movieLists = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}

interface MovieList {
  movieListId: number;
  name: string;
  // Movies: 
}
