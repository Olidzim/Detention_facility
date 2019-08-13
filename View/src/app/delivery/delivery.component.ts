import { Component, OnInit } from '@angular/core';
import {DeliveryService} from '../services/delivery.service'
import { SmartDelivery } from '../models/smartdelivery';
import { SharedService } from '../services/shared.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit {

  date: Date;
  
  smartDeliverys: SmartDelivery[];

  constructor(private router: Router, private sharedService: SharedService, private deliveryService: DeliveryService) { }

  ngOnInit() {
    this.sharedService.currentDate.subscribe(date =>{        
       this.date = date     
       if(this.router.url == '/home/delivery')
       {
        if(this.date == undefined)
        {
        console.log("initNoDate")
        this.loadsmartDeliveries();
        }
        else
        {
        console.log("initDate")
        this.getSmartDeliveriesByDate();
        } 
      }     
       }) 
     }
    
    getSmartDeliveriesByDate() {   
    this.deliveryService.getSmartDeliveriesByDate(this.date).subscribe
    ((smartDeliverys:SmartDelivery[]) => this.smartDeliverys = smartDeliverys)
    } 

  loadsmartDeliveries() { 
    this.deliveryService.getsmartDeliverys()
    .subscribe((data: SmartDelivery[]) => this.smartDeliverys = data);
  }

}
