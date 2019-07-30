import { Component, OnInit, Input, ANALYZE_FOR_ENTRY_COMPONENTS } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { SmartEmployee } from '../models/smartemployee';
import { DetentionService }  from '../services/detention.service';
import { DetaineeService }  from '../services/detainee.service';
import { Detention } from '../models/detention';
import { Detainee } from '../models/detainee';
import { HttpClient } from '@angular/common/http';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'add-detention',
  templateUrl: './add-detention.component.html',
  styleUrls: ['./add-detention.component.css']
})
export class AddDetentionComponent implements OnInit {
  employeeWhoDetain: SmartEmployee;

  detainee: Detainee;

  detainees : Detainee[] = new Array();
  newDetainees: Detainee[] = new Array();

  ss: number = 4;

  detention:  Detention = new Detention(); 

  private searchTerms = new Subject<string>();

  constructor( private http: HttpClient,  private detentionService: DetentionService, private detaineeService: DetaineeService) { }

  ngOnInit() {
  }
  search(term: string): void {
  this.searchTerms.next(term);
  }

  getEmployeeFromSearch(foundEmployee: SmartEmployee)
  {
  this.employeeWhoDetain = foundEmployee;
  }

//ternar
  onActivate(componentReference) {
  componentReference.anyFunction();
  componentReference.searchItem.subscribe((data) => {
  let newDetainee = data;
    if (newDetainee.detaineeID == undefined)
      {
      this.newDetainees.push(data);
      }
    else
      {
      this.detainees.push(data);
      }
    })
  } 

  addDetention()
  {
  /**Add detention**/
    this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
    this.detentionService.addDetention(this.detention).subscribe(response => {
    this.addDetainees(response.detentionID)
    });
  }

  addDetainees (detentionID)
  {
  /**Add new detainees**/
  this.newDetainees.forEach(newDetainee => {
    this.detaineeService.addDetainee(newDetainee).subscribe(response => {
      newDetainee = response;
      this.addDetaineesInDetention(detentionID,newDetainee.detaineeID);
      });
    });
  this.detainees.forEach(detainee => {
    this.addDetaineesInDetention(detentionID,detainee.detaineeID);
    });
  }

  addDetaineesInDetention(detentionID,detaineeID)
  {
  /**Add detainees in detention **/
    this.detentionService.addDetaineeToDetention(detentionID,detaineeID).subscribe(hero => {
    });
  }

  save(): void {
  this.detention.detainedByEmployeeID = this.employeeWhoDetain.employeeID;
    this.detentionService.addDetention(this.detention).subscribe(hero => {
     this.detention = hero
    });
  }
}


