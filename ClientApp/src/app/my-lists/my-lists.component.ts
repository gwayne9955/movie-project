import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventEmitterService } from '../event-emitter.service';    

@Component({
  selector: 'app-my-lists',
  templateUrl: './my-lists.component.html',
  styleUrls: ['./my-lists.component.css']
})
export class MyListsComponent implements OnInit {
  public movieLists: MovieListResponse;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string,
    private eventEmitterService: EventEmitterService) {
    this.getMovieLists();
  }

  ngOnInit() {
    if (this.eventEmitterService.subsVar==undefined) {    
      this.eventEmitterService.subsVar = this.eventEmitterService.    
      invokeMyListsComponentFunction.subscribe(() => {    
        this.getMovieLists();    
      });    
    }
  }

  getMovieLists() {
    this.http.get<MovieListResponse>(this.baseUrl + 'movielist').subscribe(result => {
      this.movieLists = result;
    }, error => console.error(error));
  }

}

interface MovieListResponse {
  items: MovieList[];
  totalItems: number;
}

interface Movie {
  movieListRefID: number;
  name: string;
  posterURL: string;
  imdbID: string;
}

interface MovieList {
  movieListId: number;
  name: string;
  movies: Movie[];
}
