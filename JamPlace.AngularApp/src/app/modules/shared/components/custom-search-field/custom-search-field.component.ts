import { Component, OnInit, Output,Input } from '@angular/core';
import { Observable, Subject } from 'rxjs';
@Component({
  selector: 'app-custom-search-field',
  templateUrl: './custom-search-field.component.html',
  styleUrls: ['./custom-search-field.component.scss']
})
export class CustomSearchFieldComponent implements OnInit {

  @Output() currentTerm: Subject<string>;
  @Input('placeholder-text') placeholderText: string;
   showClear: boolean;
   searchText: string;

  constructor() { 
    if(!this.placeholderText)
      this.placeholderText="Szukaj po nazwie";
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
