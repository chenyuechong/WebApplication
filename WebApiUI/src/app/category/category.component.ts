import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit {

  constructor(private service:SharedService) { }
  CategoryList:any=[];

  page:number = 1;
  pageSize:number = 3;
  ngOnInit(): void {
    this.refreshCategoryList();
  }
 
  refreshCategoryList(){
    this.service.getCategoryList().subscribe(data=>{
      this.CategoryList = data;
    })
  }
}
