import { Input, Component, OnInit , Inject} from '@angular/core';
import {DeliveryService} from '../services/delivery.service';
import {SmartDelivery} from '../models/smartdelivery';
import {SmartEmployee} from '../models/smartemployee';
import { Delivery } from '../models/delivery';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { EmployeeSearchComponent } from '../employee-search/employee.search.component';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit { 

  change: boolean = false;
  @Input() detaineeID: number;
  @Input() detentionID: number;
  delivery: Delivery;
  defaultEmployeeID: number;
  constructor(private deliveryService: DeliveryService, private sharedService: SharedService) { }
  
  
  openDialog(): void {}

  ngOnChanges() {
   this.getDelivery(this.detaineeID, this.detentionID)
  }

  ngOnInit() {   
    this.getDelivery(this.detaineeID, this.detentionID)
    this.sharedService.ifChange = false;
  }

  getEmployeeFromDetail(employeeIDForChange: number){  
  this.delivery.deliveredByEmployeeID = employeeIDForChange;
  }

  changeForm(){
    this.change = true;
    this.sharedService.ifChange = true;
  }

  getDelivery(detaineeID, detentionID): void {
    if (detaineeID == undefined)
    {
      detaineeID = 0;
    }
    if (detentionID == undefined)
    {
      detentionID = 0;
    }
    this.deliveryService.getDelivery(detaineeID, detentionID)
    .subscribe(
    res => 
    {if (res==undefined)
    {      
     this.delivery = {}      
    }
    else
    {
      this.delivery = res;
      console.log(this.delivery)
      this.defaultEmployeeID = this.delivery.deliveredByEmployeeID;      
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
