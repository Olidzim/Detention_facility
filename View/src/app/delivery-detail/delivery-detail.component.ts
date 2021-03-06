import { Input, Component, OnInit , Inject} from '@angular/core';
import {DeliveryService} from '../services/delivery.service';
import {SmartDelivery} from '../models/smartdelivery';
import {SmartEmployee} from '../models/smartemployee';
import { Delivery } from '../models/delivery';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EmployeeSearchComponent } from '../employee-search/employee.search.component';
import { SharedService } from '../services/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery-detail.component.html',
  styleUrls: ['./delivery-detail.component.css']
})
export class DeliveryDetailComponent implements OnInit { 

  @Input() detaineeID: number;
  @Input() detentionID: number;

  add: boolean;
  isDelivery: boolean = false;
  isDetainee: boolean = false;
  res: string;
  change: boolean = false;
  delivery: Delivery;
  defaultEmployeeID: number;

  constructor(private deliveryService: DeliveryService, 
    private sharedService: SharedService, 
    public router: Router) { }  
  
  openDialog(): void {}


  ngOnInit() {

    console.log(this.delivery)   
    this.getDelivery(this.detaineeID, this.detentionID)
    this.sharedService.ifChange = false;
    this.res = this.router.url.substring(0, 15);
    this.sharedService.cancelDeliveryStatus.subscribe(status => {   
      console.log(status)
      this.add = status   
    })  
  }


  ngOnChanges() {
   this.getDelivery(this.detaineeID, this.detentionID)
  }


  getEmployeeFromDetail(employeeIDForChange: number){  
  this.delivery.deliveredByEmployeeID = employeeIDForChange;
  }


  getDeliveryFromAdd(delivery: Delivery){      
    this.delivery = delivery;
    this.add = false;
    console.log(this.delivery)
    this.getDelivery(this.detaineeID, this.detentionID)
  }


  changeForm(){
    this.change = true;
    this.sharedService.ifChange = true;
  }


  addForm()
  {
    this.sharedService.changeDeliveryCancel(true)
    this.add = true;
  }


  deleteForm()
  {
    this.sharedService.changeDeliveryCancel(false)
    this.deliveryService.deleteDelivery(this.delivery.deliveryID)
    .subscribe(delivery => {
        this.ngOnInit();
      });
      this.isDelivery = false
  }


  saveChanges()  
  {    
    this.deliveryService.updateDelivery(this.delivery)
    .subscribe(data => this.delivery = data);
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
  }


  getDelivery(detaineeID, detentionID): void {
    if (detaineeID == undefined) {
      detaineeID = 0;
    }
    if (detentionID == undefined) {
      detentionID = 0;
    }
    this.deliveryService.getDelivery(detaineeID, detentionID)
    .subscribe( res => {
      if (res==undefined){      
        this.delivery = {}      
      }
      else {
        this.delivery = res; 
        this.defaultEmployeeID = this.delivery.deliveredByEmployeeID;
        this.isDelivery = true;      
      }
    });
  }
  

  cancel() {
    this.delivery.deliveredByEmployeeID = this.defaultEmployeeID;
    this.change = false;
    this.sharedService.ifChange = false;
    this.sharedService.default = true;
  }

}
