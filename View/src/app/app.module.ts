import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from "@angular/common/http";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthInterceptor } from './auth/auth.interceptor';
import { ReactiveFormsModule,FormsModule} from '@angular/forms'
import { LoginService } from '../app/login/login.service';
import { DetaineeComponent } from './detainee/detainee.component';
import { DetaineeDetailComponent } from './detainee-detail/detainee-detail.component';
import { AddDetentionComponent } from './add-detention/add-detention.component';
import { EmployeeSearchComponent } from './employee-search/employee.search.component';
import { EmployeeComponent } from './employee/employee.component';
import { AddDetaineeComponent } from './add-detainee/add-detainee.component';
import { UploadComponent } from './upload/upload.component';
import { FindDetaineeComponent } from './find-detainee/find-detainee.component';
import { DeliveryDetailComponent } from './delivery-detail/delivery-detail.component';
import { ReleaseDetailComponent } from './release-detail/release-detail.component';
import { DetentionDetailComponent } from './detention-detail/detention-detail.component';
import { DetentionSearchComponent } from './detention-search/detention-search.component';
import { DetentionComponent } from './detention/detention.component';
import { UserComponent } from './user/user.component';
import { EmployeeDetailComponent } from './employee-detail/employee-detail.component';
import {MatDialogModule} from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { DatePipe } from '@angular/common';
import { AddDeliveryComponent } from './add-delivery/add-delivery.component';
import { AddReleaseComponent } from './add-release/add-release.component';
import { DeliveryComponent } from './delivery/delivery.component';
import { ReleaseComponent } from './release/release.component';
import { PermissionManagerService } from './role/permission-manager.service';
import { IsGrantedDirective } from './role/is-granted.directive';
import { RegistrationComponent } from './registration/registration.component';
import { UserDetailComponent } from './user-detail/user-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    DetaineeComponent,
    DetaineeDetailComponent,
    AddDetentionComponent,
    EmployeeSearchComponent,
    AddDetaineeComponent,
    UploadComponent,
    FindDetaineeComponent,
    DeliveryDetailComponent,
    ReleaseDetailComponent,
    DetentionDetailComponent,
    DetentionSearchComponent,
    DetentionComponent,
    EmployeeComponent,
    UserComponent,
    EmployeeDetailComponent,
    AddDeliveryComponent,
    AddReleaseComponent,
    DeliveryComponent,
    ReleaseComponent, 
    IsGrantedDirective, 
    RegistrationComponent, 
    UserDetailComponent 
  ],

  imports: [
    NgbModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
   ],

  providers: [
    PermissionManagerService,
    DatePipe,
    LoginService,
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: false}}],

  bootstrap: [AppComponent]
})
export class AppModule { }
