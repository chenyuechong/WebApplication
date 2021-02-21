import { Component, OnInit } from '@angular/core';
import {SharedService} from '../../shared.service';
import Swal from 'sweetalert2';
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

  DepartmentIdFilter:string="";
  DepartmentNameFilter:string="";
  DepartmentWithoutFilter:any=[];
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
      this.refreshDepList();
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
    this.refreshDepList();
  }

  deleteClick(item:any)
  {
    if(confirm('Are you sure??')){
      this.service.deleteDepartment(item.DepartmentId).subscribe(
        data=>{
          alert(data.toString());
          this.refreshDepList();
        }
      );

    }
  }

  refreshDepList(){
    this.service.getDepList().subscribe(data=>{
      this.DepartmentList = data;
      this.DepartmentWithoutFilter = data;
    })
  }


  FilterFn(){
    var DepartmentIdFilter = this.DepartmentIdFilter;
    var DepartmentNameFilter = this.DepartmentNameFilter;

    this.DepartmentList = this.DepartmentWithoutFilter.filter(function(el:any){
      return el.DepartmentId.toString().toLowerCase().includes(
        DepartmentIdFilter.toString().trim().toLowerCase())&&
        el.DepartmentName.toString().toLowerCase().includes(
          DepartmentNameFilter.toString().trim().toLowerCase()
      )
    });
  }

  sortResult(prop:any,asc:boolean)
  {
    this.DepartmentList = this.DepartmentWithoutFilter.sort(function(a:any, b:any){
      if(asc)
      {
        return ((a[prop]>b[prop])?1:((a[prop]<b[prop])?-1:0));
      }else{
        return ((b[prop]>a[prop])?1:((b[prop]<a[prop])?-1:0));;
      }
    })

  }
}
