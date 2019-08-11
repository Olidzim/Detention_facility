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

  private cancelDeliverySource = new BehaviorSubject<boolean>(undefined);
  cancelDeliveryStatus = this.cancelDeliverySource.asObservable();

  private cancelReleaseSource = new BehaviorSubject<boolean>(undefined);
  cancelReleaseStatus = this.cancelReleaseSource.asObservable();

  private changeEmployeeSource = new BehaviorSubject<boolean>(undefined);
  changeEmployeeStatus = this.changeEmployeeSource.asObservable();

    files: File[] = new Array;
    //id: number;
    forDetentionDetailID: number;
    forDetaineeDetailID: number;
    ifChange = false;
    default = false;
    ifDetention = false;
    date: Date;
  
    changeMessage(date: Date)
    {       
      this.messageSource.next(date)
    }

    changeReleaseCancel(cancel: boolean)
    {       
      this.cancelReleaseSource.next(cancel)
    }

    changeDeliveryCancel(cancel: boolean)
    {       
      this.cancelDeliverySource.next(cancel)
    }

    changeEmployeeStatusFlag(cancel: boolean)
    {       
      this.changeEmployeeSource.next(cancel)
    }




    constructor(
   
        private http: HttpClient
        ) { }
        
        ngOnChanges() {
          alert(this.date)
        }
    }