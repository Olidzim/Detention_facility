import { Component, OnInit } from '@angular/core';
import {DeliveryService} from '../services/delivery.service'
import { SmartDelivery } from '../models/smartdelivery';

@Component({
  selector: 'delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.css']
})
export class DeliveryComponent implements OnInit {

  smartDeliverys: SmartDelivery[];

  constructor(private deliveryService: DeliveryService) { }

  ngOnInit() {
    this.loadsmartDeliveries();
  }

  loadsmartDeliveries() { 
    this.deliveryService.getsmartDeliverys()
    .subscribe((data: SmartDelivery[]) => this.smartDeliverys = data);
  }

}
