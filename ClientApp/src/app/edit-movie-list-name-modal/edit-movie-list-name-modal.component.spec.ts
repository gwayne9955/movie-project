import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMovieListNameModalComponent } from './edit-movie-list-name-modal.component';

describe('EditMovieListNameModalComponent', () => {
  let component: EditMovieListNameModalComponent;
  let fixture: ComponentFixture<EditMovieListNameModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditMovieListNameModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditMovieListNameModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
