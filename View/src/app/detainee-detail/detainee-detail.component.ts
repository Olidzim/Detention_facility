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

  constructor(
    private sharedService: SharedService,
    private http: HttpClient,
    private sanitizer: DomSanitizer,
    private detaineeService: DetaineeService,
    private deliveryService: DeliveryService,
    private detentionService: DetentionService
    ) { }

  ngOnInit() {
    this.sharedService.id;
    this.getDetainee(this.sharedService.id);
  }
  public getSantizeUrl() { 

    this.path = "http://localhost:58653/UploadFile/"+this.detainee.homePhoneNumber; 
    return this.sanitizer.bypassSecurityTrustUrl(this.path);

  }

  getSmartDetentionsByID(): void {

    let id = this.detainee.detaineeID;
    //+this.route.snapshot.paramMap.get('id');
    this.detentionService.getSmartDetentionsByDetaineeID(id)
    .subscribe(smartDetentions => this.smartDetentions = smartDetentions); 
    
  }  

  getSmartDetentionsByDetaineeID(): void {            
    this.detentionService.getSmartDetentionsByDetaineeID(this.sharedService.id)
    .subscribe(res => this.smartDetentions = res);  
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
      console.log(d)
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
      
    }); 
    console.log(this.detainee)
    this.getSmartDetentionsByDetaineeID();
   
   // alert(this.myDate)
  }
}

