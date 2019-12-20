import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-edit-movie-list-name-modal',
  templateUrl: './edit-movie-list-name-modal.component.html',
  styleUrls: ['./edit-movie-list-name-modal.component.css']
})
export class EditMovieListNameModalComponent implements OnInit {
  private closeResult: string;
  private editName: string;
  @Input() receivedParentListName: string;
  @Output() listNameToEmit = new EventEmitter<string>();

  constructor(private modalService: NgbModal) { }

  ngOnInit() {
    this.editName = this.receivedParentListName;
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
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

  editListName(newListName: string) {
    this.listNameToEmit.emit(newListName);
  }

}
