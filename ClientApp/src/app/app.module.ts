import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AddMovieListComponent } from './add-movie-list/add-movie-list.component';
import { MyListsComponent } from './my-lists/my-lists.component';
import { MovieListDetailsComponent } from './movie-list-details/movie-list-details.component';
import { EventEmitterService } from './event-emitter.service';
import { MovieSearchComponent } from './movie-search/movie-search.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AddMovieListComponent,
    MyListsComponent,
    MovieListDetailsComponent,
    MovieSearchComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'search', component: MovieSearchComponent },
      { path: 'add-movie-list', component: AddMovieListComponent, canActivate: [AuthorizeGuard] },
      { path: 'my-lists', component: MyListsComponent, canActivate: [AuthorizeGuard] },
      { path: 'my-lists/:id', component: MovieListDetailsComponent, canActivate: [AuthorizeGuard] }
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    EventEmitterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
