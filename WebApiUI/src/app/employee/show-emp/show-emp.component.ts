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

  ModalTitle:string="";
  ActivateAddEditDepComp:boolean = false;
  emp:any;
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
        PhotoFileName:""
      }
      this.ActivateAddEditDepComp = true;
      this.ModalTitle="Add Department";
  }

  closeClick(){
    this.ActivateAddEditDepComp = false;
    this.refreshEmpList();
  }

  editClick(item:any)
  {
    this.emp = item;
    this.ModalTitle = "Edit Department";
    this.ActivateAddEditDepComp = true;
  }

  deleteClick(item:any)
  {
    if(confirm('Are you sure??')){
      this.service.deleteDepartment(item.EmployeeId).subscribe(
        data=>{
          alert(data.toString());
          this.refreshEmpList();
        }
      );

    }
  }

  refreshEmpList(){
    this.service.getDepList().subscribe(data=>{
      this.EmployeeList = data;
    })
  }

}
