import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { SmartDelivery } from '../models/smartdelivery';
import { Release } from '../models/release';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class ReleaseService {
  private ReleaseUrl = 'http://localhost:58653/api/release';
  constructor(private http: HttpClient) { }

  getSmartDelivery (detaineeID, detentionID): Observable<SmartDelivery> {
    return this.http.get<SmartDelivery>(`${this.ReleaseUrl}/GetSmartDeliveryByIDs/${detaineeID}/${detentionID}`)
      .pipe(  
        map(x => x),
        catchError(this.handleError<SmartDelivery>('', ))
      );
  }

  getsmartDeliverysByIDs(DetaineeID: number, DetentionID: number) {
    const url = `${this.ReleaseUrl}/GetDeliveryByIDs/${DetaineeID}/${DetentionID}`;
    return this.http.get(url);    
  }

  getReleaseByIDs (detaineeID, detentionID): Observable<Release> {
    return this.http.get<Release>(`${this.ReleaseUrl}/GetReleaseByIDs/${detaineeID}/${detentionID}`)
      .pipe(  
        map(x => x),
        catchError(this.handleError<Release>('', ))
      );
  }

  getDeliveryByID(id: number): Observable<Release> {   
    return this.http.get <Release>(`${this.ReleaseUrl}/GetRelease/${id}`);
  }

  getsmartReleases() { 
    return this.http.get(`${this.ReleaseUrl}/GetReleases`);
  }

  createRelease(release: Release) {
    return this.http.post(`${this.ReleaseUrl}/InsertRelease`, release);
  }

  updateRelease(release: Release) {
    return this.http.put(`${this.ReleaseUrl}/UpdateRelease/${release.releaseID}`, release);
  }

  deleteRelease(releaseID: number) {
    return this.http.delete(`${this.ReleaseUrl}/DeleteRelease/${releaseID}`);
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {    
      console.error(error); 
      return of(result as T);};
  }       

}