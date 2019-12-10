import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMovieListComponent } from './add-movie-list.component';

describe('AddMovieListComponent', () => {
  let component: AddMovieListComponent;
  let fixture: ComponentFixture<AddMovieListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddMovieListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddMovieListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
