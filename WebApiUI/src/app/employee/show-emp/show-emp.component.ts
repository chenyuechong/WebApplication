import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.scss']
})
export class ShowEmpComponent implements OnInit {
  
  constructor(private service:SharedService) { }
  EmployeeList:any=[];

  page:number = 1;
  pageSize:number = 3;
  
  ModalTitle:string="";
  ActivateAddEditEmpComp:boolean = false;
  emp:any;
  EmployeeIdFilter:string="";
  EmployeeNameFilter:string="";
  EmployeeWithoutFilter:any=[];
  ngOnInit(): void {
    this.refreshEmpList();
  }
 
  addClick()
  {
      this.emp={
        EmployeeId:0,
        EmployeeName:"",
        Department:"",
        DateOfJoining:"",
        PhotoFileName:"anonymous.png"
      }
      this.ActivateAddEditEmpComp = true;
      this.ModalTitle="Add Department";
      this.refreshEmpList();
  }

  closeClick(){
    this.ActivateAddEditEmpComp = false;
    this.refreshEmpList();
  }

  editClick(item:any)
  {
    this.emp = item;
    this.ModalTitle = "Edit Employee";
    this.ActivateAddEditEmpComp = true;
    this.refreshEmpList();
  }

  deleteClick(item:any)
  {
    if(confirm('Are you sure??')){
      this.service.deleteEmployee(item.EmployeeId).subscribe(
        data=>{
          alert(data.toString());
          this.refreshEmpList();
        }
      );

    }
  }

  refreshEmpList(){
    this.service.getEmpList().subscribe(data=>{
      this.EmployeeList = data;
    })
  }
  FilterFn(){
    var EmployeeIdFilter = this.EmployeeIdFilter;
    var EmployeeNameFilter = this.EmployeeNameFilter;

    this.EmployeeList = this.EmployeeWithoutFilter.filter(function(el:any){
      return el.DepartmentId.toString().toLowerCase().includes(
        EmployeeIdFilter.toString().trim().toLowerCase())&&
        el.DepartmentName.toString().toLowerCase().includes(
          EmployeeNameFilter.toString().trim().toLowerCase()
      )
    });
  }

  sortResult(prop:any,asc:boolean)
  {
    this.EmployeeList = this.EmployeeWithoutFilter.sort(function(a:any, b:any){
      if(asc)
      {
        return ((a[prop]>b[prop])?1:((a[prop]<b[prop])?-1:0));
      }else{
        return ((b[prop]>a[prop])?1:((b[prop]<a[prop])?-1:0));;
      }
    })

  }
}