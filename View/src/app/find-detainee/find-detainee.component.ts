import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { DetaineeService }  from '../services/detainee.service';
import {debounceTime, distinctUntilChanged, switchMap} from 'rxjs/operators';
 import { Detainee } from '../models/detainee';
 import { SharedService} from '../services/shared.service';
import { SmartDetainee } from '../models/smartdetainee';

@Component({
  selector: 'find-detainee',
  templateUrl: './find-detainee.component.html',
  styleUrls: ['./find-detainee.component.css']
})
export class FindDetaineeComponent implements OnInit {

  @Output() searchItem: EventEmitter<any> = new EventEmitter();

  private searchTermsName = new Subject<string>();
  private searchTermsAddress = new Subject<string>();

  route: string = this.router.url;
  smartDetainees$: Observable<SmartDetainee[]>;
  addressDetainees$: Observable<SmartDetainee[]>;
  buttonShow: boolean = false;
  detaineesCheck: Detainee[] = new Array;
  foundDetainee: Detainee = new Detainee();
    
  constructor(
    private detaineeService: DetaineeService, 
    private sharedService: SharedService, 
    private router: Router) { }

  ngOnInit(): void {
    this.smartDetainees$ = this.searchTermsName.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((term: string) => this.detaineeService.searchDetaineesByName(term)),        
    );
    this.addressDetainees$ = this.searchTermsAddress.pipe(
      debounceTime(300), 
      distinctUntilChanged(),
      switchMap((term: string) => this.detaineeService.searchDetaineesByAddress(term)),   
     );
  }


  searchByName(term: string): void {
    if (term == "#" || term == "&")
    term = ""
    this.searchTermsName.next(term);
  }


  searchByAddres(term: string): void {
    if (term == "#" || term == "&")
    term = ""
    this.searchTermsAddress.next(term);
  }

 
  chooseSmartDetainee(foundSmartDetainee: SmartDetainee) {    
    this.sharedService.forDetaineeDetailID = foundSmartDetainee.detaineeID
    this.foundDetainee.detaineeID = foundSmartDetainee.detaineeID;
    if (typeof this.detaineesCheck !== 'undefined' && this.detaineesCheck.length > 0){
      this.detaineesCheck.forEach(element => {
      if (element.detaineeID == this.foundDetainee.detaineeID)
        this.buttonShow = false;
      else{ 
        this.buttonShow = true;
        this.detaineesCheck.push(this.foundDetainee)
      }
      });
    }   
    else {  
      this.detaineesCheck.push(this.foundDetainee)
      this.buttonShow = true;
    }
    if (this.route == "/home/add-detention/find-detainee"){
      this.router.navigateByUrl('/home/add-detention/find-detainee/detainee-detail/'+this.foundDetainee.detaineeID);
    }
    else
      this.router.navigateByUrl('/home/detainee/detainee-detail/'+this.foundDetainee.detaineeID);
  }  


  onActivate(componentReference) {  
    componentReference.getDetainee(this.foundDetainee.detaineeID);
  }


  sendDetaineeToDetention() {
    var copy = Object.assign({}, this.foundDetainee);    
    this.searchItem.emit(copy);
    this.buttonShow = false;
  }
}