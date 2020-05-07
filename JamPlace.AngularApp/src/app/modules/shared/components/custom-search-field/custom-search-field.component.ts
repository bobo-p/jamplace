import { Component, OnInit, Output } from '@angular/core';
import { Observable, Subject } from 'rxjs';
@Component({
  selector: 'app-custom-search-field',
  templateUrl: './custom-search-field.component.html',
  styleUrls: ['./custom-search-field.component.scss']
})
export class CustomSearchFieldComponent implements OnInit {

  @Output() currentTerm: Subject<string>;
   showClear: boolean;
   searchText: string;

  constructor() { 
    this.currentTerm=new Subject<string>();
  }

  ngOnInit() {
  }
  search(term: string): void {
    term ? this.showClear = true : this.showClear = false;
    this.currentTerm.next(term);
  }
  clear() {
    this.searchText='';
    this.showClear=false;
    this.currentTerm.next('');
  }

}
