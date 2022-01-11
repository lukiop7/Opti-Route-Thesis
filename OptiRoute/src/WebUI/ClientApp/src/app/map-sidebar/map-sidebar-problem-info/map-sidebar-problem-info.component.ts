import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {FormGroup} from '@angular/forms';

@Component({
  selector: 'app-map-sidebar-problem-info',
  templateUrl: './map-sidebar-problem-info.component.html',
  styleUrls: ['./map-sidebar-problem-info.component.scss']
})
export class MapSidebarProblemInfoComponent implements OnInit, OnDestroy {
  @Input('group')
  public problemInfoForm: FormGroup;
  @Output() continueClicked = new EventEmitter<void>();
  @Output() backClicked = new EventEmitter<void>();


  constructor() { }
  ngOnDestroy(): void {
  }

  ngOnInit(): void {
  }

  onClick() {
    this.continueClicked.emit();
  }

  onBackClick(){
    this.backClicked.emit();
  }

}
