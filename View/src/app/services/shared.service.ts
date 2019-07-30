import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Detainee } from '../models/detainee';
import { SmartDetention } from '../models/smartdetention';
import { SmartDelivery } from '../models/smartdelivery';
import { SmartEmployee } from '../models/smartemployee';
import { Detention } from '../models/detention';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({ providedIn: 'root' })
export class SharedService {
    id: number;
    forDetentionDetailID: number;
    ifChange = false;
    default = false;

    constructor(
        private http: HttpClient
        ) { }
         
    }