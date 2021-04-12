import { Component, Input, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private service:SharedService) { }

 

  ngOnInit(): void {
   
  }

  


  login(Email:any, password:any){
    var val={
      email:Email,
      password:password};
    this.service.login(val).subscribe(
      res=>{
        this.service.token= JSON.parse(res.toString())['token'];
        this.service.UserId = JSON.parse(res.toString())['userId'];
        console.log(res);
      });
  }

}
