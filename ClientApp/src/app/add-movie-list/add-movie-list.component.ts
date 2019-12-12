import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from '../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'app-add-movie-list',
  templateUrl: './add-movie-list.component.html',
  styleUrls: ['./add-movie-list.component.css']
})
export class AddMovieListComponent implements OnInit {
  public listName;
  public lists;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}

  ngOnInit() {
    // maybe get user-id?
  }

  addListName() {
    debugger;
    // this.authorizeService.getUser().pipe(map(u => u && u.name)).subscribe(response => {
    //   this.userName = response;
    //   console.log(response);
    // });
    this.http.post<MovieList>(this.baseUrl + 'movielist', 
    {
      listName: this.listName
    });
    // this.http.get<MovieList[]>(this.baseUrl + 'movielist').subscribe(result => {
    //   // debugger;
    //   this.lists = result;
    // }, error => console.error(error));
  }

}

interface MovieList {
  name: string;
}
