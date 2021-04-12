import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tokenize } from '@angular/compiler/src/ml_parser/lexer';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl= "https://localhost:44397/api/";
readonly PhotoUrl = "https://localhost:44397/Photos/"
token:string="";
UserId:string ="";


constructor(private http:HttpClient) { }

  getDepList():Observable<any[]>
  {
    return this.http.get<any>(this.APIUrl+'department');
  }

  addDepartment(val:any){
    return this.http.post(this.APIUrl+'Department', val);
  }
  updateDepartment(val:any){
    return this.http.put(this.APIUrl+'Department', val);
  }
  deleteDepartment(val:any){
    return this.http.delete(this.APIUrl+'Department/'+ val);
  }
  getEmpList():Observable<any[]>
  {
    return this.http.get<any>(this.APIUrl+'Employee');
  }

  addEmployee(val:any){
    return this.http.post(this.APIUrl+'Employee', val);
  }
  updateEmployee(val:any){
    return this.http.put(this.APIUrl+'Employee', val);
  }
  deleteEmployee(val:any){
    return this.http.delete(this.APIUrl+'Employee/'+ val);
  }


  uploadPhoto(val:any){
    return this.http.post(this.APIUrl+'Employee/SaveFile', val);
  }

  getAllDepartmentName():Observable<any[]>
  {
    return this.http.get<any>(this.APIUrl+'Employee/GetAllDepartmentNames');
  }


  getCategoryList():Observable<any[]>
  {
    return this.http.get<any>(this.APIUrl+'petitions/categories');
  }
  getPepList():Observable<any[]>
  {
    return this.http.get<any>(this.APIUrl+'petitions');
  }

  login(val:any)
  {
    return this.http.post(this.APIUrl+'users/login', val);
  }
}
