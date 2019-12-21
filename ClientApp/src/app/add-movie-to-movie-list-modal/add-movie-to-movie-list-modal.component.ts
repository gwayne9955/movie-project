import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-add-movie-to-movie-list-modal',
  templateUrl: './add-movie-to-movie-list-modal.component.html',
  styleUrls: ['./add-movie-to-movie-list-modal.component.css']
})
export class AddMovieToMovieListModalComponent implements OnInit {
  private closeResult: string;
  public movieLists: MovieListResponse;
  @Output() listIdToAddTo = new EventEmitter<string>();

  constructor(private modalService: NgbModal,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.getMovieLists();
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', scrollable: true }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  getMovieLists() {
    this.http.get<MovieListResponse>(this.baseUrl + 'movielist', {
      params: {
        Page: "1",
        ItemsPerPage: "100000"
      }
    }).subscribe(result => {
      this.movieLists = result;
    }, error => alert(error.error));
  }

  selectMovieListToAddTo(id: string) {
    this.listIdToAddTo.emit(id);
  }

}