import { Component, OnInit, Input, ANALYZE_FOR_ENTRY_COMPONENTS, ComponentFactoryResolver } from '@angular/core';
import { Observable, Subject , of } from 'rxjs';
import { SmartEmployee } from '../models/smartemployee';
import { DetentionService }  from '../services/detention.service';
import { DetaineeService }  from '../services/detainee.service';
import { Detention } from '../models/detention';
import { Detainee } from '../models/detainee';
import { ResponseClass } from '../models/response';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { SharedService } from '../services/shared.service';
import { DomSanitizer, SafeResourceUrl, SafeUrl} from '@angular/platform-browser';
import { map } from 'rxjs/operators';
import { catchError } from 'rxjs/operators'; 
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

@Component({
  selector: 'add-detention',
  templateUrl: './add-detention.component.html',
  styleUrls: ['./add-detention.component.css']
})
export class AddDetentionComponent implements OnInit {

  employeeWhoDetain: SmartEmployee;
  detainee: Detainee;
  resp: ResponseClass
  detainees : Detainee[] = new Array();
  newDetainees: Detainee[] = new Array();
  detention:  Detention = new Detention(); 
  private searchTerms = new Subject<string>();
  

  constructor( 
    private http: HttpClient, 
    private sanitizer: DomSanitizer, 
    private detentionService: DetentionService, 
    private detaineeService: DetaineeService, 
    private sharedService: SharedService) { }


  ngOnInit() {
    
    this.sharedService.detaineeToDetention.subscribe(detainee => {  
      console.log("ddd"+ detainee)
      if (detainee != undefined) {     
        if (this.searchInOldDetaineesArray(detainee.detaineeID)) {
          alert("Уже добавлен")
       
        }
        else {
          this.detainees.push(detainee) 
          
        }
      }
    }) 
  }


  searchInOldDetaineesArray (id: number) : boolean
  {
    let isAdded = false;
    this.detainees.forEach(element => {
      if (element.detaineeID == id) {    
        console.log("element"+element.detaineeID)  
        isAdded= true;
      }      
    });
    return isAdded;
  }


  search(term: string): void {
  this.searchTerms.next(term);  
  }


  getEmployeeFromSearch(foundEmployee: SmartEmployee){  
  this.employeeWhoDetain = foundEmployee;
  }

  
  onActivate(componentReference) { 
    componentReference.searchItem.subscribe((data) => {
      let newDetainee = data;
      if (newDetainee.detaineeID == undefined){
      this.newDetainees.push(data);
      }
      else {
      this.detainees.push(data);
      }
    })
  } 


  addDetention() { 
    let smartResponse
    this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
    console.log("det"+this.newDetainees)
    if (this.newDetainees.length > 0) {
      this.uploadFile().subscribe( data=> {
        smartResponse = data
        if(smartResponse.isSuccess) {
          console.log("file uploaded")        
          this.detentionService.addDetention(this.detention).subscribe(response => {
          this.addDetainees(response.detentionID)
          });
        }
        else if (!smartResponse.isSuccess) {
          alert(smartResponse.message)
        }
      })
    }
    else {
    this.detentionService.addDetention(this.detention).subscribe(response => {
      this.addDetainees(response.detentionID)
      });
    }
  }


  uploadFile(): any {
    let r: ResponseClass;
    let formData: FormData = new FormData(); 
    this.sharedService.files.forEach(element => {
      formData.append('uploadFile',element, element.name);  
    }); 

    let url = "http://localhost:58653/api/Upload/UploadJsonFile";
    console.log("form data: "+formData)  

    return this.http.post(url, formData).pipe(map((response: ResponseClass) => {
      r = response
      return r;
    }));
  }


  /**Add new detainees**/
  addDetainees (detentionID) {  



    for (let index = 0; index < this.newDetainees.length; index++) {
      this.detaineeService.addDetainee(this.newDetainees[index]).subscribe((response: Detainee) => {      
        
         
        console.log(`promise result: ${response}`);
         this.addDetaineesInDetention(detentionID,response.detaineeID);
         });
      
    }
 /* this.newDetainees.forEach(newDetainee => {
    this.detaineeService.addDetainee(newDetainee).subscribe(response => {
     // newDetainee = response;
     
      this.detainees.push(response)
      console.log(`promise result: ${response}`);
      this.addDetaineesInDetention(detentionID,newDetainee.detaineeID);
      });
    });*/
    console.log('I will not wait until promise is resolved');
   this.detainees.forEach (detainee => {
    console.log("Привет")
    this.addDetaineesInDetention(detentionID,detainee.detaineeID);
    });
  }


   /**Add detainees in detention **/
  addDetaineesInDetention(detentionID,detaineeID){ 
    this.detaineeService.addDetaineeToDetention(detentionID,detaineeID).subscribe(hero => {
      this.detainees.length = 0;
      this.newDetainees.length = 0;
      this.sharedService.sendDetaineeToDetention(undefined)
    });
    
   
   
  }


  save(): void {
  this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
    this.detentionService.addDetention(this.detention).subscribe(hero => {
     this.detention = hero
    });
  }
}


