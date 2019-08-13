import { Component, OnInit } from '@angular/core';
import {DeliveryService} from '../services/delivery.service'
import { SmartDelivery } from '../models/smartdelivery';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit {

  date: Date;

  smartDeliverys: SmartDelivery[];

  constructor(private sharedService: SharedService, private deliveryService: DeliveryService) { }

  ngOnInit() {
    this.sharedService.currentDate.subscribe(date =>{
      console.log("else")     
       this.date = date
       if(this.date == undefined)
       {
         console.log("initNoDate")
         this.loadsmartDeliveries();
       }
       else
       {
         console.log("initDate")
         //this.getDetentionsByDate();
       }      
       }) 
     }
    
  

  loadsmartDeliveries() { 
    this.deliveryService.getsmartDeliverys()
    .subscribe((data: SmartDelivery[]) => this.smartDeliverys = data);
  }

}
