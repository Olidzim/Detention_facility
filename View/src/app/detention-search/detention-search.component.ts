import { Component, OnInit } from '@angular/core';
import {Detention} from '../models/detention'; 
import { Observable, Subject } from 'rxjs';
import { DetaineeService }  from '../services/detainee.service';
import {
   debounceTime, distinctUntilChanged, switchMap
 } from 'rxjs/operators';
 
import { SmartDetention } from '../models/smartdetention';
import { SmartDetainee} from '../models/smartdetainee';
import { DetentionService } from '../services/detention.service';
import { SharedService } from '../services/shared.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-detention-search',
  templateUrl: './detention-search.component.html',
  styleUrls: ['./detention-search.component.css']
})

export class DetentionSearchComponent implements OnInit {
  date: Date;
  detention: Detention;
  detentions$: Observable<Detention[]>;
  foundDetentions: SmartDetention[];
  foundDetention: Detention = new Detention();
  foundDetainees: SmartDetainee[];
  private searchTerms = new Subject<string>();
  flag: number;
  constructor (
    private detentionService: DetentionService, 
    private detaineeService: DetaineeService,
    private sharedService: SharedService,
    private router: Router
    ) {} 

  search(term: string): void {
    this.searchTerms.next(term);
  }
 
  chooseSearchResult(foundDetention: Detention){
    this.detention = foundDetention;
  }

  ngOnInit(): void {
  }

  getDetaineesOfDetention(index: number, chosenDetention: SmartDetention) { 
    this.flag = index;
    this.detaineeService.getsmartDetaineesByDetentionID(chosenDetention.detentionID).subscribe
    ((data: SmartDetainee[]) => this.foundDetainees = data)    
  }

  getfoundDetentions() {
    this.flag = null; 
    this.detentionService.searchDetentionsByDate(this.date).subscribe
    ((data: SmartDetention[]) => this.foundDetentions = data)
  }   

  toDetentionDetail(d: SmartDetention) {
    this.router.navigateByUrl('/home/detention/detention-detail/'+d.detentionID);
  }
}

