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

  ss: number = 4;

  detention:  Detention = new Detention(); 

  private searchTerms = new Subject<string>();

  constructor( private http: HttpClient, private sanitizer: DomSanitizer, private detentionService: DetentionService, private detaineeService: DetaineeService, private sharedService: SharedService) { }

  ngOnInit() {
  }
  search(term: string): void {
  this.searchTerms.next(term);
  }

  getEmployeeFromSearch(foundEmployee: SmartEmployee)
  {
  console.log(foundEmployee)
  this.employeeWhoDetain = foundEmployee;
  }

//ternar
  onActivate(componentReference) {
  componentReference.anyFunction();
  componentReference.searchItem.subscribe((data) => {
  let newDetainee = data;
    if (newDetainee.detaineeID == undefined)
      {
      this.newDetainees.push(data);
      }
    else
      {
      this.detainees.push(data);
      }
    })
  } 

  addDetention()
  { 
    let r
    this.uploadFile().subscribe(data=>{
      r = data
      if(r.isSuccess)
      {
        this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
        this.detentionService.addDetention(this.detention).subscribe(response => {
        this.addDetainees(response.detentionID)
        });
      }
      else if (!r.isSuccess)
      {
        alert(r.message)
      }
  
  
  })
 

  /**Add detention**/


      
  


  }
  but()
  {
    
   
    let k = this.sanitizer.bypassSecurityTrustUrl("http://localhost:58653/UploadFile/1.png");
    console.log()
 
    /*alert()
    this.http.get("http://localhost:58653/UploadFile/1.png", {observe: 'response'}).subscribe(data => 
    {
      console.log(data.status)
      if (data.status == 404)
      {
      alert("Нет")
      }
      else  
      {
        alert("Есть")
      }
    })*/

  }

  uploadFile(): any
  {
    let r: ResponseClass;
    ///TODO Upload file service
    let formData: FormData = new FormData(); 
    this.sharedService.files.forEach(element => {
      formData.append('uploadFile',element, element.name);  
    });
    
    //this.detainee.photo = this.file.name;
    let apiUrl1 = "http://localhost:58653/api/Upload/UploadJsonFile";
    console.log("form data: "+formData)  

    return this.http.post(apiUrl1, formData).pipe(map((response: ResponseClass) => 
    {r = response
      return r;
    }));
  }
     /// <<<=== use `map` here


  


  /*  this.http.post(apiUrl1, formData).subscribe
   (res=> 
    {r = res
    return r;
    })*/


   
  

  addDetainees (detentionID)
  {
  /**Add new detainees**/
  this.newDetainees.forEach(newDetainee => {
    this.detaineeService.addDetainee(newDetainee).subscribe(response => {
      newDetainee = response;
      this.addDetaineesInDetention(detentionID,newDetainee.detaineeID);
      });
    });
  this.detainees.forEach(detainee => {
    this.addDetaineesInDetention(detentionID,detainee.detaineeID);
    });
  }

  addDetaineesInDetention(detentionID,detaineeID)
  {
  /**Add detainees in detention **/
    this.detaineeService.addDetaineeToDetention(detentionID,detaineeID).subscribe(hero => {
    });
  }

  save(): void {
  this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
    this.detentionService.addDetention(this.detention).subscribe(hero => {
     this.detention = hero
    });
  }
}


