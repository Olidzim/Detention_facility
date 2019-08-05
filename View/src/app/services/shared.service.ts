import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of, BehaviorSubject } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Detainee } from '../models/detainee';
import { SmartDetention } from '../models/smartdetention';
import { SmartDelivery } from '../models/smartdelivery';
import { SmartEmployee } from '../models/smartemployee';
import { Detention } from '../models/detention';
import { DatePipe } from '@angular/common'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({ providedIn: 'root' })
export class SharedService {

  private messageSource = new BehaviorSubject<Date>(undefined);
  currentDate = this.messageSource.asObservable();

    id: number;
    forDetentionDetailID: number;
    ifChange = false;
    default = false;
    ifDetention = false;
    date: Date;
  
    changeMessage(date: Date)
    {       
      this.messageSource.next(date)
    }

    constructor(
   
        private http: HttpClient
        ) { }
        
        ngOnChanges() {
          alert(this.date)
        }
    }