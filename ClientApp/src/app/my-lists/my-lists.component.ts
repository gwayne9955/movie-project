import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PipeTransform, Pipe } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-lists',
  templateUrl: './my-lists.component.html',
  styleUrls: ['./my-lists.component.css']
})
export class MyListsComponent implements OnInit {
  private routeSub;
  private movieLists: MovieListResponse;
  private itemsPerPage: number;
  private currentPage: number;
  private maxPage: number;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private route: ActivatedRoute) {
    this.itemsPerPage = 10;
    this.currentPage = 1;
  }

  ngOnInit() {
    this.routeSub = this.route
      .queryParams
      .subscribe(params => {
        this.currentPage = parseInt(params['page'] || "1");
        this.getMovieLists(this.currentPage.toString());
      });
  }

  getMovieLists(pageNumber: string) {
    this.http.get<MovieListResponse>(this.baseUrl + 'movielist', {
      params: {
        Page: pageNumber,
        ItemsPerPage: this.itemsPerPage.toString()
      }
    }).subscribe(result => {
      this.movieLists = result;
      this.maxPage = this.getMaxPage(this.movieLists.totalItems, this.itemsPerPage);
    }, error => alert(error.error));
  }

  getMaxPage(totalItems: number, itemsPerPage: number) {
    let n = 0;
    while (totalItems > 0) {
      ++n;
      totalItems -= itemsPerPage;
    }
    return n;
  }

}

@Pipe({ name: 'pages' })
export class PagesPipe implements PipeTransform {
  transform(value: number, itemsPerPage: number): any {
    const arr = [];
    let n = 0;
    while (value > 0) {
      arr.push(++n);
      value -= itemsPerPage;
    }
    return arr;
  }
}