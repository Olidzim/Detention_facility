import { Component, OnInit, Output, Input, EventEmitter, Inject } from '@angular/core';
import { Delivery } from '../models/delivery';
import { DeliveryService } from '../services/delivery.service';
import { SharedService } from '../services/shared.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-add-delivery',
  templateUrl: './add-delivery.component.html',
  styleUrls: ['./add-delivery.component.css']
})

export class AddDeliveryComponent implements OnInit {
  
  @Input() detaineeID: number;
  @Input() detentionID: number;
  @Output() delivery: Delivery = new Delivery;
  @Output() toNewDelivery = new EventEmitter<Delivery>();

  constructor(
    private deliveryService: DeliveryService, 
    private sharedService: SharedService) { }


  ngOnInit() {    
  }


  getEmployeeFromDetail(employeeIDForChange: number) {  
    console.log("deliveryadd")
    this.delivery.deliveredByEmployeeID = employeeIDForChange;
    }


  save() {
    this.delivery.detaineeID = this.detaineeID;
    this.delivery.detentionID = this.detentionID;
    this.deliveryService.createDelivery(this.delivery)
    .subscribe((data: Delivery) => {
    let sendDelivery = new Delivery; 
    console.log(data);
    this.toNewDelivery.emit(this.delivery);
    });  
  }
  

  cancel() {
  this.sharedService.changeDeliveryCancel(false)
  }

}
