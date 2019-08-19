import { Component, OnInit , ViewChild } from '@angular/core';
import { DetentionService }  from '../services/detention.service';
import { DetaineeService }  from '../services/detainee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { SharedService } from '../services/shared.service';
import { SmartDetention } from '../models/smartdetention';
import { SmartDetainee } from '../models/smartdetainee';
import { DeliveryDetailComponent } from '../delivery-detail/delivery-detail.component';

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
    private route: ActivatedRoute,
    private sharedService: SharedService) { }  
    
    change: boolean = false;
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
    defaultEmployeeID: number;

  ngOnInit() {
    this.sharedService.ifChange = false;
    this.getSmartDetentionsByID();
  }

  changeDetention()
  { 
    this.change = true;
    this.sharedService.ifChange = true;
    this.sharedService.ifDetention = true;
    this.smartDetainee = undefined;  
  }

  cancel() {
    this.sharedService.ifDetention = false;
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
    this.ddetention.detainedByEmployeeID = this.defaultEmployeeID;
  }

  getSmartDetentionsByID(): void {  
    let id
    if (this.sharedService.forDetentionDetailID == undefined) { 
      id = this.route.snapshot.paramMap.get('id');
    } 
    else 
    {   
     id = this.sharedService.forDetentionDetailID;  
    }     
    this.detentionService.getSmartDetentionByDetentionID(id)
    .subscribe(res => {
        this.smartDetention = res      
      });      

    this.detentionService.getDetentionByDetentionID(id)
    .subscribe(
      res => {
      this.ddetention = res
      this.employeeID = res.detainedByEmployeeID
      this.defaultEmployeeID = res.detainedByEmployeeID  
      this.getSmartDetaineesByDetentionID();   
      }
    );     
  
    this.employeeID = this.ddetention.detainedByEmployeeID;
    this.showEmployeeDetail();  
  }  

  getEmployeeFromDetail(employeeIDForChange: number) {  
    this.ddetention.detainedByEmployeeID = employeeIDForChange;
  }

  deleteDetention() {

    this.detentionService.deleteDetention(this.ddetention.detentionID).subscribe
    (res=>console.log(res))
    this.router.navigate(['/home/detention']);
  }

  showEmployeeDetail() : void  {
  this.show = true; 
  }

  saveChanges()
  {
    this.detentionService.updateDetention(this.ddetention)
    .subscribe(data => this.ddetention = data);
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
  }

  getSmartDetaineesByDetentionID(): void { 
    this.detaineeService.getsmartDetaineesByDetentionID(this.ddetention.detentionID )
    .subscribe(res => this.smartDetainees = res);  
  } 

  getDetainee(d: SmartDetainee): void {  
    if (!this.change) {
      this.smartDetainee = undefined;
      this.smartDetainee = d;
      this.sharedService.ifChange = false;    
    }
  } 

  onActivate(componentReference) {   
    componentReference.getHeroes(this.detention, this.detainee);
  }
}
