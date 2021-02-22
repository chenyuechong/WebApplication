import { Component, OnInit, Input} from '@angular/core';
import { SharedService } from 'src/app/shared.service';
import  Swal  from 'sweetalert2';
@Component({
  selector: 'app-add-edit-emp',
  templateUrl: './add-edit-emp.component.html',
  styleUrls: ['./add-edit-emp.component.scss']
})
export class AddEditEmpComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() emp:any;

  EmployeeId:string="";
  EmployeeName:string="";
  Department:string ="";
  DateOfJoining:string ="";
  PhotoFileName:string = "";
  photoFilePath:string ="";
  DepartmentsList:any =[];

  ngOnInit(): void {
    
    this.LoadDepartsmentList();
  }

  LoadDepartsmentList()
  {
    this.service.getAllDepartmentName().subscribe(data=>{
      this.DepartmentsList = data;

      this.EmployeeId = this.emp.EmployeeId;
      this.EmployeeName = this.emp.EmployeeName;
      this.Department = this.emp.Department;
      this.DateOfJoining = this.emp.DateOfJoining;
      this.PhotoFileName = this.emp.PhotoFileName;
      this.photoFilePath = this.service.PhotoUrl+ this.photoFilePath;
    });
     
  }

  addEmployee(){
    var val={EmployeeId:this.EmployeeId,
      EmployeeName:this.EmployeeName,
      DateOfJoining : this.DateOfJoining,
      Department: this.Department,
      PhotoFileName: this.PhotoFileName

    };
    this.service.addEmployee(val).subscribe(
      res=>{
        if(res == "Create Successfully")
        {
          Swal.fire({
            icon: 'success',
            title: 'New Employee has been Created',
            showConfirmButton: false,
            timer: 1500
          })
        }
      });

  }

  updateEmployee(){
    var val={EmployeeId:this.EmployeeId,
      EmployeeName:this.EmployeeName,
      DateOfJoining : this.DateOfJoining,
      Department: this.Department,
      PhotoFileName: this.PhotoFileName

    };
    this.service.updateEmployee(val).subscribe(
      res=>alert(res.toString()));;
  }

  uploadPhoto(event:any)
  {
    var file = event.target.files[0];
    const formData:FormData= new FormData();
    formData.append('uploadedFile', file, file.name);

    this.service.uploadPhoto(formData).subscribe(data=>
      {
        this.PhotoFileName = data.toString();
        this.photoFilePath = this.service.PhotoUrl+this.PhotoFileName;
      })
  }
}
