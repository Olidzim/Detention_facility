import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { SmartDetention } from '../models/smartdetention';
import { Detention } from '../models/detention';
import { DatePipe } from '@angular/common';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json ; charset=utf-8' })
};

@Injectable({ providedIn: 'root' })

export class DetentionService {

  readonly DetentionsUrl = 'http://localhost:58653/api/detention';
  constructor(
    private http: HttpClient,
    public datepipe: DatePipe    
    ) { }

 /* getDetentions() {   
    return this.http.get(`${this.DetentionsUrl}/GetDetentions/`);    
  }*/

/** Search detentions from the server */
/*searchDetentions(term: string): Observable<Detention[]> {
    if (!term.trim()) {
    return of([]);}
      return this.http.get<Detention[]>(`${this.DetentionsUrl}/getdetention/${term}`).pipe(            
        catchError(this.handleError<Detention[]>('searchDetentions', []))
    );
}*/

  updateDetention(detention: Detention) {   
    return this.http.put(this.DetentionsUrl + '/UpdateDetention/'+ detention.detentionID, detention);
  }

/** Search detentions by date*/
  searchDetentionsByDate(date: Date): Observable<SmartDetention[]> {  
    let latest_date = this.datepipe.transform(date, 'yyyy-MM-dd');     
    console.log(latest_date)
    //date.toString;
    
    var json = JSON.stringify(latest_date);
    return this.http.post<SmartDetention[]>(`${this.DetentionsUrl}/GetDetentionsByDate/`, json, httpOptions).pipe(            
      catchError(this.handleError<SmartDetention[]>('searchDetentions', []))
    );
  }

  getSmartDetentionsByDetaineeID(id: number): Observable<SmartDetention[]> {
    return this.http.get<SmartDetention[]>(`${this.DetentionsUrl}/GetSmartDetentionsByDetaineeID/${id}`).pipe(              
      catchError(this.handleError<SmartDetention[]>(`getDetention id=${id}`))
    );
  }

  getSmartDetentionByDetentionID(id: number): Observable<SmartDetention> {    
    return this.http.get<SmartDetention>(`${this.DetentionsUrl}/GetSmartDetentionsByDetentionID/${id}`).pipe(              
      catchError(this.handleError<SmartDetention>(`getDetention id=${id}`))
    );
  }

  getDetentionByDetentionID(id: number): Observable<Detention> {
    return this.http.get<Detention>(`${this.DetentionsUrl}/GetDetention/${id}`).pipe(              
      catchError(this.handleError<Detention>(`getDetention id=${id}`))
    );
  }   

  getSmartDetentions(): Observable<SmartDetention[]> {    
    return this.http.get<SmartDetention[]>(`${this.DetentionsUrl}/GetSmartDetentions`).pipe(              
      catchError(this.handleError<SmartDetention[]>('searchHeroes', []))
    );
  }

  /*getSmartDetentionsByDate(): Observable<SmartDetention[]> {    
    return this.http.get<SmartDetention[]>(`${this.DetentionsUrl}/GetDetentionsByDate`).pipe(              
      catchError(this.handleError<SmartDetention[]>('searchHeroes', []))
    );
  }*/

  addDetention (detention: Detention) : Observable<Detention> {         
    return this.http.post<Detention>(`${this.DetentionsUrl}/InsertDetention/`, detention);
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {     
    console.error(error); 
    return of(result as T);};
    }    
    
    deleteDetention(id: number) {
      console.log(id)
      return this.http.delete(this.DetentionsUrl + '/DeleteDetention/' + id);
    }
}