import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployeeComponent } from './employee/employee.component';
import { DepartmentComponent } from './department/department.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent} from './register/register.component';
import { PetitionsComponent } from './petitions/petitions.component';
import { CategoryComponent } from './category/category.component';

const routes: Routes = [
{path:'employee', component:EmployeeComponent},
{path:'department', component:DepartmentComponent},
{path:'home', component:HomeComponent},
{path:'login', component:LoginComponent},
{path:'register',component:RegisterComponent},
{path:'petitions', component:PetitionsComponent},
{path:'category',component:CategoryComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
