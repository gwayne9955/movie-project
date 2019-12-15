import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-movie-search',
  templateUrl: './movie-search.component.html',
  styleUrls: ['./movie-search.component.css']
})
export class MovieSearchComponent implements OnInit {
  private searchQueryString: string;
  private movieSearchResult: MovieSearchResult;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
  private route: ActivatedRoute, 
  private router: Router) { }

  searchQueryOnKeyUp(e) {
    if (this.searchQueryString.length >= 3) {
      this.http.get<MovieSearchResult>("https://www.omdbapi.com/", {
        params: {
          apikey: "281cdd33",
          s: this.searchQueryString
        }})
      .subscribe(result => {
        debugger;
        this.movieSearchResult = result;
      }, error => console.error(error));
    }
  }

  ngOnInit() {
  }

  
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