import { Component, ViewChild, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent implements OnInit {
  public title = 'WebApiUI';
 

  constructor() {
  }

ngOnInit(): void {
}
}
