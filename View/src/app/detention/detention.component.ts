import { Component, OnInit } from '@angular/core';
import { Detention } from '../models/detention';
import { DetentionService }  from '../services/detention.service';
import { Router } from '@angular/router';
import {SharedService} from '../services/shared.service';
import { SmartDetention } from '../models/smartdetention';

@Component({
  selector: 'app-detention',
  templateUrl: './detention.component.html',
  styleUrls: ['./detention.component.css']
})

export class DetentionComponent implements OnInit {
   date: Date; 

  detentions: SmartDetention[];
  constructor(
    private detentionService: DetentionService,
    private router: Router,
    private sharedService: SharedService
    ) { }

  ngOnInit() {

    this.sharedService.currentDate.subscribe(date =>{
      if(this.router.url == '/home/detention') {
        this.date = date
        if(this.date == undefined) {
          this.loadsmartDetentions();
        }
        else {
           this.getDetentionsByDate();
        } 
      }      
    }) 
  }

  loadsmartDetentions() { 
    this.detentionService.getSmartDetentions()
    .subscribe((data: Detention[]) => this.detentions = data);
  }

  toDetentionDetail(d: SmartDetention)
  {    
    this.sharedService.forDetentionDetailID = d.detentionID;    
    this.router.navigateByUrl('/home/detention/detention-detail/'+d.detentionID);
  }

  getDetentionsByDate() {  
    this.detentionService.searchDetentionsByDate(this.date).subscribe
    ((data: Detention[]) => this.detentions = data)
  } 

}
