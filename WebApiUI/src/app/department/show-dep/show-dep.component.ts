import { Component, OnInit } from '@angular/core';
import {SharedService} from '../../shared.service'

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.scss']
})
export class ShowDepComponent implements OnInit {

  constructor(private service:SharedService) { }
  DepartmentList:any=[];

  ModalTitle:string="";
  ActivateAddEditDepComp:boolean = false;
  dep:any;

  ngOnInit(): void {
    this.refreshDepList();
  }
 
  addClick()
  {
      this.dep={
        DepartmentId:0,
        DepartmentName:""
      }
      this.ActivateAddEditDepComp = true;
      this.ModalTitle="Add Department";
  }

  closeClick(){
    this.ActivateAddEditDepComp = false;
    this.refreshDepList();
  }

  editClick(item:any)
  {
    this.dep = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
  }
  refreshDepList(){
    this.service.getDepList().subscribe(data=>{
      this.DepartmentList = data;
    })
  }
}
