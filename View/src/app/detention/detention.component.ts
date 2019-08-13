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
   
  // message: String;

  detentions: Detention[];
  constructor(
    private detentionService: DetentionService,
    private router: Router,
    private sharedService: SharedService
    ) { }

  ngOnInit() {
   /* if(this.date == undefined)
    {
      console.log("undefined")
      this.loadsmartDetentions()
    }
    else {    */
    this.sharedService.currentDate.subscribe(date =>{
     console.log("else")     
      this.date = date
      if(this.date == undefined)
      {
        console.log("initNoDate")
        this.loadsmartDetentions();
      }
      else
      {
        console.log("initDate")
        this.getDetentionsByDate();
      }      
      }) 
    }

  loadsmartDetentions() {
    console.log("NoDate")
    this.detentionService.getSmartDetentions()
    .subscribe((data: Detention[]) => this.detentions = data);
  }

  toDetentionDetail(d: SmartDetention)
  {    
    this.sharedService.forDetentionDetailID = d.detentionID;    
    this.router.navigateByUrl('/home/detention/detention-detail/'+d.detentionID);
  }

  getDetentionsByDate() {   
    console.log("Date")
    this.detentionService.searchDetentionsByDate(this.date).subscribe
    ((data: Detention[]) => this.detentions = data)
  } 

}
