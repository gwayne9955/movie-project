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
  public userId = "";
  public listName = "";
  public userName: string;

  constructor(private authorizeService: AuthorizeService) { 

  }

  ngOnInit() {
    // maybe get user-id?
  }

  addListName() {
    // debugger;
    this.authorizeService.getUser().pipe(map(u => u && u.name)).subscribe(response => {
      this.userName = response;
      console.log(response);
    });
  }

}

interface MovieList {
  userId: string;
  name: string;
}
