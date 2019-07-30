import { Component, OnInit , ViewChild } from '@angular/core';
import { DetentionService }  from '../services/detention.service';
import { DetaineeService }  from '../services/detainee.service';
import { Router } from '@angular/router';
import { SharedService } from '../services/shared.service';
import { SmartDetention } from '../models/smartdetention';
import { SmartDetainee } from '../models/smartdetainee';
import { DeliveryComponent } from '../delivery/delivery.component';

import { Detention } from '../models/detention';

@Component({
  selector: 'app-detention-detail',
  templateUrl: './detention-detail.component.html',
  styleUrls: ['./detention-detail.component.css'],
})
export class DetentionDetailComponent implements OnInit {
  today = new Date();

  constructor(
    private detentionService: DetentionService,
    private detaineeService: DetaineeService,
    private router: Router,
    private sharedService: SharedService) { }    
    smartDetention: SmartDetention = new SmartDetention;
    smartDetainees: SmartDetainee[];
    smartDetainee: SmartDetainee;
    loadcomponent: boolean;
    detention: number;
    detainee: number;
    dShow: boolean = false;
    rShow: boolean;
    some: number = 0;
    ddetention: Detention = new Detention();
    show: boolean = false;
    employeeID: number = 0;

  ngOnInit() {
    this.getSmartDetentionsByID();
  }

  getSmartDetentionsByID(): void {    
    let id = this.sharedService.forDetentionDetailID;    
    this.detentionService.getSmartDetentionByDetentionID(id)
    .subscribe(res => this.smartDetention = res);  
    console.log(this.smartDetention)

    this.detentionService.getDetentionByDetentionID(id)
    .subscribe(
      res => {
      this.ddetention = res
      this.employeeID = res.detainedByEmployeeID
      console.log(this.employeeID)
      }
    );    
    console.log(this.ddetention)

    this.getSmartDetaineesByDetentionID();    
    this.employeeID = this.ddetention.detainedByEmployeeID;
    this.showEmployeeDetail();  
  }  

  showEmployeeDetail() : void  {
  this.show = true; 
  }

  getSmartDetaineesByDetentionID(): void {  
    console.log(this.ddetention)
    let id = this.sharedService.forDetentionDetailID;    
    this.detaineeService.getsmartDetaineesByDetentionID(this.sharedService.forDetentionDetailID)
    .subscribe(res => this.smartDetainees = res);  
  } 

  getDetainee(d: SmartDetainee): void {
    this.smartDetainee = undefined;
    this.smartDetainee = d;
    this.sharedService.ifChange = false;
    console.log(d)
  } 

  dataChanged(newObj) { 
    this.some = null
    this.some = newObj
  }

  releaseShow(){
  this.dShow = false;
  this.rShow = true;
  }

  deliveryShow(){
  this.rShow = false;
  this.dShow = true;
  }

  onActivate(componentReference) {  
    console.log(componentReference)
    componentReference.getHeroes(this.detention, this.detainee);
  }
}
