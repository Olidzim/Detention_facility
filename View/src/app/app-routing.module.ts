import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from '../app/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
import {DetaineeComponent} from './detainee/detainee.component';
import {DetaineeDetailComponent} from './detainee-detail/detainee-detail.component';
import {AddDetentionComponent} from './add-detention/add-detention.component';
import {EmployeeSearchComponent} from './employee-search/employee.search.component';
import {UploadComponent} from './upload/upload.component';
import {DetentionComponent} from './detention/detention.component';
import {AddDetaineeComponent} from './add-detainee/add-detainee.component';
import {FindDetaineeComponent} from './find-detainee/find-detainee.component';
import {DeliveryDetailComponent} from './delivery-detail/delivery-detail.component';
import {ReleaseDetailComponent} from './release-detail/release-detail.component';
import {DetentionSearchComponent} from './detention-search/detention-search.component';
import {EmployeeComponent} from './employee/employee.component';
import {DeliveryComponent} from './delivery/delivery.component';
import {ReleaseComponent} from './release/release.component';

EmployeeComponent
import {from } from 'rxjs';
import { DetentionDetailComponent } from './detention-detail/detention-detail.component';
const itemRoutes: Routes = [
  { path: 'details', component: DetaineeDetailComponent}
];
const iitemRoutes: Routes = [
  {path: 'add-detainee', component: AddDetaineeComponent},
  {path: 'find-detainee', component: FindDetaineeComponent},
  {path: 'delivery', component: DeliveryDetailComponent},
  {path: 'release', component: ReleaseDetailComponent},
  {path: 'detention-search', component: DetentionSearchComponent},
  {path: 'app-add-delivery', component: DetentionSearchComponent},  
  {path: 'find-detainee/detainee-detail/:id', component: DetaineeDetailComponent},
];
const tableRoutes: Routes = [
  {path: 'detention', component: DetentionComponent, children: iitemRoutes},
  {path: 'add-detention', component: AddDetentionComponent, children: iitemRoutes},
  {path: 'detainee', component: DetaineeComponent, children: itemRoutes},
  {path: 'employee', component: EmployeeComponent, children: itemRoutes},
  {path: 'detainee-detail', component: DetaineeDetailComponent},
  {path: 'find-detainee', component: FindDetaineeComponent},
  {path: 'detention-search', component: DetentionSearchComponent},
 // {path: 'find-detainee/detainee-detail/:id', component: DetaineeDetailComponent},
  {path: 'detainee/detainee-detail/:id', component: DetaineeDetailComponent},
  {path: 'detention/detention-detail/:id', component: DetentionDetailComponent},
  {path: 'delivery', component: DeliveryComponent},
  {path: 'release', component: ReleaseComponent}
];




const routes: Routes = [

{path:'', redirectTo:'/login',pathMatch:'full'},
{path: 'login', component: LoginComponent},
{path: 'home', component: HomeComponent, canActivate: [AuthGuard], children: tableRoutes},
{path: 'detainee', component: DetaineeComponent, children: itemRoutes},
{path: 'add-detention', component: AddDetentionComponent, children: iitemRoutes},
{path: 'employee-search', component: EmployeeSearchComponent, children: itemRoutes},
{path: 'upload', component: UploadComponent, children: itemRoutes},
{path: 'find-detainee', component: FindDetaineeComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
