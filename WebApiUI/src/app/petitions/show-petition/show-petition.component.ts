import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-petition',
  templateUrl: './show-petition.component.html',
  styleUrls: ['./show-petition.component.scss']
})
export class ShowPetitionComponent implements OnInit {

  constructor(private service:SharedService) { }

  PetitionList:any=[];

  page:number = 1;
  pageSize:number = 3;
  
  ModalTitle:string="";
  ActivateAddEditEmpComp:boolean = false;
  emp:any;
  PetitionIdFilter:string="";
  PetitionNameFilter:string="";
  PetitionWithoutFilter:any=[];
  ngOnInit(): void {
    this.refreshPepList();
  }
  refreshPepList(){
    this.service.getPepList().subscribe(data=>{
      this.PetitionList = data;
    })
  }
  deleteClick(item: any)
  {

  }
  editClick(item:any)
  {

  }
  closeClick()
  {

  }
  addClick(){}

  FilterFn(){
    var PetitionIdFilter = this.PetitionIdFilter;
    var PetitionNameFilter = this.PetitionNameFilter;

    this.PetitionList = this.PetitionWithoutFilter.filter(function(el:any){
      return el.DepartmentId.toString().toLowerCase().includes(
        PetitionIdFilter.toString().trim().toLowerCase())&&
        el.DepartmentName.toString().toLowerCase().includes(
          PetitionNameFilter.toString().trim().toLowerCase()
      )
    });
  }


  sortResult(prop:any,asc:boolean)
  {
    this.PetitionList = this.PetitionWithoutFilter.sort(function(a:any, b:any){
      if(asc)
      {
        return ((a[prop]>b[prop])?1:((a[prop]<b[prop])?-1:0));
      }else{
        return ((b[prop]>a[prop])?1:((b[prop]<a[prop])?-1:0));;
      }
    })

  }
}
