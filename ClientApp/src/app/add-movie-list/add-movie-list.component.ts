import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-movie-list',
  templateUrl: './add-movie-list.component.html',
  styleUrls: ['./add-movie-list.component.css']
})
export class AddMovieListComponent {
  public listName: string;
  public ableToAdd: boolean;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router) {
    this.ableToAdd = false;
  }

  addListName() {
    this.http.post<MovieList>(this.baseUrl + 'movielist', {
      Name: this.listName
    })
      .subscribe(result => {
        alert("added " + this.listName);
        this.listName = "";
        this.router.navigateByUrl('/my-lists');
      }, error => console.error(error));
  }

  toggleAbleToAdd() {
    if (this.listName.length > 0 && !this.ableToAdd) {
      this.ableToAdd = true;
    }
    else if (this.listName.length == 0 && this.ableToAdd) {
      this.ableToAdd = false;
    }
  }

}
