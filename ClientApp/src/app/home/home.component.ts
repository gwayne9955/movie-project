import { Component, Inject, HostListener } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public tmdbResponse: TMDBResponse;
  public tmdbMovies;
  public omdbListing: OMDBMovieSearchTitle;
  public newArray;
  public pageNum: number;
  public columns: number;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute,
    private router: Router) {
    this.newArray = [];
    this.pageNum = 1;
    this.columns = 3;
    this.tmdbMovies = [];
    this.getPopularMovies();
  }

  getPopularMovies() {
    this.http.get<TMDBResponse>("https://api.themoviedb.org/3/discover/movie", {
      params: {
        api_key: "f28df3fec9ce98f371cc2a6636044a45",
        sort_by: "popularity.desc",
        page: this.pageNum.toString(),
        language: "en-US",
        with_original_language: "en"
      }
    })
      .subscribe(result => {
        this.tmdbResponse = result;
        this.tmdbMovies = this.tmdbMovies.concat(this.tmdbResponse.results);
        this.newArray = [];
        for (let i = 0; i < this.tmdbMovies.length; i += this.columns) {
          this.newArray.push({ items: this.tmdbMovies.slice(i, i + this.columns) });
        }
      }, error => alert(error.error));
  }

  popularMovieClick(title: string, originalTitle: string) {
    title = title.replace("…", "...");
    originalTitle = originalTitle.replace("…", "...");
    this.http.get<OMDBMovieSearchTitle>("https://www.omdbapi.com/", {
      params: {
        apikey: "281cdd33",
        t: title
      }
    })
      .subscribe(result => {
        if (result.Response == "True") {
          this.omdbListing = result;
          this.router.navigateByUrl(`/movie/${this.omdbListing.imdbID}`);
        }
        else {
          this.popularMovieSecondaryTitle(originalTitle);
        }
      }, error => alert(error.error));
  }

  popularMovieSecondaryTitle(originalTitle: string) {
    this.http.get<OMDBMovieSearchTitle>("https://www.omdbapi.com/", {
      params: {
        apikey: "281cdd33",
        t: originalTitle
      }
    })
      .subscribe(result => {
        this.omdbListing = result;
        this.router.navigateByUrl(`/movie/${this.omdbListing.imdbID}`);
      }, error => alert(error.error));
  }

  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
    //In chrome and some browser scroll is given to body tag
    let pos = (document.documentElement.scrollHeight || document.body.scrollHeight) - document.documentElement.clientHeight;
    let max = document.documentElement.scrollTop;
    if (pos == max && this.pageNum < this.tmdbResponse.total_pages) {
      this.pageNum++;
      this.getPopularMovies();
    }
  }
}
