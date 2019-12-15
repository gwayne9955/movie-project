import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieListDetailsComponent } from './movie-list-details.component';

describe('MovieListDetailsComponent', () => {
  let component: MovieListDetailsComponent;
  let fixture: ComponentFixture<MovieListDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MovieListDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieListDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
