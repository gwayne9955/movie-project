import { Component, OnInit, Inject, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-movie-search',
  templateUrl: './movie-search.component.html',
  styleUrls: ['./movie-search.component.css']
})
export class MovieSearchComponent implements OnInit {
  private routeSub;
  private searchQueryString: string;
  private tmdbResponse: TMDBResponse;
  private tmdbMovies;
  private omdbListing: MovieSearchListing;
  private newArray;
  private pageNum: number;
  private columns: number;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') private baseUrl: string, 
  private route: ActivatedRoute, 
  private router: Router) {
    this.newArray = [];
    this.columns = 3;
    this.pageNum = 1;
    this.tmdbMovies = [];
  }

  ngOnInit() {
    this.routeSub = this.route
    .queryParams
    .subscribe(params => {
      this.searchQueryString = params['q'] || "";
      this.searchQuery();
    });
  }

  searchQueryOnKeyUp() {
      this.router.navigate(['search'], { queryParams: { q: this.searchQueryString}});
  }

  searchQuery() {
    if (this.searchQueryString && this.searchQueryString.length >= 3) {
      this.http.get<TMDBResponse>("https://api.themoviedb.org/3/search/movie", {
        params: {
          api_key: "f28df3fec9ce98f371cc2a6636044a45",
          query: this.searchQueryString,
          page: this.pageNum.toString()
        }})
      .subscribe(result => {
        this.tmdbResponse = result;
        this.tmdbMovies = this.tmdbMovies.concat(this.tmdbResponse.results);
        this.newArray = [];
        for (let i = 0; i < this.tmdbMovies.length; i += this.columns) {
          this.newArray.push({ items: this.tmdbMovies.slice(i, i + this.columns) });
        }
      }, error => console.error(error));
    }
    else {
      this.tmdbResponse = null;
    }
  }

  searchMovieClick(title: string) {
    title = title.replace("…", "...");
    this.http.get<MovieSearchListing>("https://www.omdbapi.com/", {
        params: {
          apikey: "281cdd33",
          t: title
        }})
      .subscribe(result => {
        this.omdbListing = result;
        this.router.navigateByUrl(`/movie/${this.omdbListing.imdbID}`);
      }, error => console.error(error));
  }

  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollHeight || document.body.scrollHeight) - document.documentElement.clientHeight;
    let max = document.documentElement.scrollTop;
    if (pos == max)   {
      this.pageNum++;
      this.searchQuery();
    }
  }
  
}

interface TMDBResponse {
  page: number;
  total_results: number;
  total_pages: number;
  results: TMDBMovie[];
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