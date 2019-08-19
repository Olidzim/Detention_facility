import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Detainee } from '../models/detainee';
import { SmartDetainee } from '../models/smartdetainee';
import { Detention } from '../models/detention';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })

export class DetaineeService {

    readonly detaineeUrl = 'http://localhost:58653/api/detainee'; 
    constructor( 
        private http: HttpClient 
        ) { }

    getDetainee(id: number): Observable<Detainee> {
        const url = `${this.detaineeUrl}/GetDetaineeByID/${id}`;           
        return this.http.get<Detainee>(url).pipe(              
        catchError(this.handleError<Detainee>(`getDetainee id=${id}`)));
    }

    getsmartDetaineesByDetentionID (id: number) : Observable<SmartDetainee[]>  {
        const url = `${this.detaineeUrl}/GetDetaineeByDetentionID/${id}`;
        return this.http.get<SmartDetainee[]>(url);    
    }

    getsmartDetainees () {
        const url = `${this.detaineeUrl}/GetDetainees/`;
        return this.http.get(url);    
    }

    searchDetaineesByName (term: string): Observable<SmartDetainee[]> {
        ///TODO DetaineeSearch controller
        if (!term.trim()) {
        return of([]);}
        return this.http.get<Detainee[]>(`${this.detaineeUrl}/GetDet?term=${term}`).pipe(            
        catchError(this.handleError<Detainee[]>('searchDetainees', [])));
    }

    searchDetaineesByAddress (term: string): Observable<SmartDetainee[]> {
        if (!term.trim()) {
        return of([]);}
        return this.http.get<Detainee[]>(`${this.detaineeUrl}/GetDetaineeByAddress?term=${term}`).pipe(            
        catchError(this.handleError<Detainee[]>('searchDetaineesByAddress', [])));
    }
        
    updateDetainee (detainee: Detainee) {
        return this.http.put(`${this.detaineeUrl}/UpdateDetainee/${detainee.detaineeID}`, detainee);
    }

    addDetaineeToDetention (detentionID: number, detaineeId: number) : Observable<Detention> {        
        return this.http.post<Detention>(`${this.detaineeUrl}/AddDetaineeToDetention/${detaineeId}`, detentionID);
      }

    addDetainee (detainee: Detainee)  {       
        return this.http.post(`${this.detaineeUrl}/InsertDetainee/`, detainee);
    }

    deleteDetainee (id: number) {
        return this.http.delete(`${this.detaineeUrl}/DeleteDetainee/${id}`);
    }

    private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {       
        console.error(error);
        return of(result as T);};
    }  
}