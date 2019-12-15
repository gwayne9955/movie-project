import { Component, OnInit, OnDestroy } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-movie-list-details',
  templateUrl: './movie-list-details.component.html',
  styleUrls: ['./movie-list-details.component.css']
})
export class MovieListDetailsComponent implements OnInit, OnDestroy {
  private routeSub;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      console.log(params); // log the entire params object
      console.log(params['id']); // log the value of id
    });
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

}
