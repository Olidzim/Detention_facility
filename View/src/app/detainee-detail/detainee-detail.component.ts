import { Component, OnInit, Input } from '@angular/core';
import { Detainee } from '../models/detainee';
import { DetaineeService }  from '../services/detainee.service';
import { DomSanitizer, SafeResourceUrl, SafeUrl} from '@angular/platform-browser';
import { SmartDetention } from '../models/smartdetention';
import { identifierModuleUrl } from '@angular/compiler';
import { SmartDelivery } from '../models/smartdelivery';
import { element } from 'protractor';
import { isNull } from 'util';
import { isEmpty } from 'rxjs/operators';
import {HttpClient,HttpHeaders} from "@angular/common/http"
import { SmartRelease } from '../models/smartrelease';
import { SharedService} from '../services/shared.service';
import { DeliveryService} from '../services/delivery.service';
import { DetentionService } from '../services/detention.service';
import { Router, Route } from '@angular/router';
@Component({
  selector: 'app-detainee-detail',
  templateUrl: './detainee-detail.component.html',
  styleUrls: ['./detainee-detail.component.css']
})
export class DetaineeDetailComponent implements OnInit {
  @Input() detainee: Detainee = new Detainee();
  @Input() smartDetentions: SmartDetention[];
  smartDelivery: SmartDelivery;
  smartRelease: SmartRelease;
  path :string;
  s : number = 1;
  k: number;
  sho: boolean = false;
  text: string = 'Не доставлен';
  smartDetention: SmartDetention;
  change: boolean = false;
  age: number;
  myDate = new Date();
  filetoUpload: File = null;
  filetoUploadMassive: File[] = null;
  fileToUpload: File = null;
  imageUrl: string = null;
  url;

  constructor(
    private sharedService: SharedService,
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    private detaineeService: DetaineeService,
    private deliveryService: DeliveryService,
    private detentionService: DetentionService,
    private router: Router,
 
    ) { }

  ngOnInit() {
    //this.sharedService.id;
    this.getDetainee(this.sharedService.forDetaineeDetailID);
  }
  public getSantizeUrl() { 
    alert("call")
    console.log(this.detainee)    
    this.path = "http://localhost:58653/UploadFile/"+this.detainee.photo; 
    this.url = this.sanitizer.bypassSecurityTrustUrl(this.path);
  }

  getSmartDetentionsByID(): void {

    let id = this.detainee.detaineeID;
    //+this.route.snapshot.paramMap.get('id');
    this.detentionService.getSmartDetentionsByDetaineeID(id)
    .subscribe(smartDetentions => this.smartDetentions = smartDetentions);     
  }  

  getSmartDetentionsByDetaineeID(): void {            
    this.detentionService.getSmartDetentionsByDetaineeID(this.sharedService.forDetaineeDetailID)
    .subscribe(res => this.smartDetentions = res);  
  } 

  deleteCurrentDetainee()
  {
    console.log(this.detainee.detaineeID)
    this.detaineeService.deleteDetainee(this.detainee.detaineeID).subscribe
    (res => {console.log(res)
    this.router.navigate(['/home/detainee']);
    })
    
  }

  handleFileInput(file: FileList)
  {
  console.log("handleFileInput")
  this.fileToUpload = file.item(0);
  var reader = new FileReader();
  reader.onload = (event:any) =>
  {
    this.url = event.target.result;
  }
  reader.readAsDataURL(this.fileToUpload);
  console.log(this.fileToUpload)
  this.detainee.photo = this.fileToUpload.name;
  }

  saveChanges()
  {
    console.log(this.detainee)
    this.detaineeService.updateDetainee(this.detainee)
    .subscribe(data => this.detainee = data);
    this.change = false;
  /*  this.sharedService.ifChange = false;
    this.sharedService.default = true;*/
  }

  getDetention(d: SmartDetention): void {
    if(this.change)
    {

    }
    else
    {
      this.smartDetention = undefined;
      this.smartDetention = d;
      this.sharedService.ifChange = false;    
    }
  } 
  

/*
  trackByFn(index, item) {

    return item.id;

  }
  /*
  hi(i:number,e:SmartDetention): void {


    this.k = i;
    this.sho = true;
    //this.getSmartDeliveries();
    this.getTrue(e.detentionID);
    this.getSmartReleases();

  }
 */



 
     getSmartDeliveries(): void {  

    this.http.get('http://localhost:58653/Api/Delivery/GetDeliveryByIDs/1/1')
    .subscribe((data: SmartDelivery) => this.smartDelivery = data)      

  }

  uploadFile()
  {
    alert("dd")
    ///TODO Upload file service
    let formData: FormData = new FormData(); 
    formData.append('uploadFile',   this.fileToUpload, this.fileToUpload.name);  
    this.detainee.photo = this.fileToUpload.name;
    let apiUrl1 = "http://localhost:58653/api/Upload/UploadJsonFile";
    console.log(formData)  
    this.http.post(apiUrl1, formData)  
    .subscribe(hero => {
    });
  }
/*
    getTrue(k: number): void {
    //TODO Delivery Service
    let id = this.detainee.detaineeID;
    //+this.route.snapshot.paramMap.get('id');
    this.deliveryService.getSmartDelivery(this.detainee.detaineeID,k)
    .subscribe((data: SmartDelivery) => this.smartDelivery = data); 
       
  }
*/
    getSmartReleases(): void {  

    this.http.get('http://localhost:58653/Api/Release/GetReleaseByIDs/1/1')
    .subscribe((data: SmartRelease) => this.smartRelease = data)  

  }

    getDetainee(id: number): void {

    this.detaineeService.getDetainee(id)
    .subscribe(response =>     
      {this.detainee = response 
        let newDate = new Date(response.birthDate);
        let timeDiff = Math.abs(Date.now() - newDate.getTime());
        this.age = Math.floor((timeDiff / (1000 * 3600 * 24))/365.25);
        this.getSantizeUrl();
      
    });   
    this.getSmartDetentionsByDetaineeID();     
   
   // alert(this.myDate)
  }

  
}

