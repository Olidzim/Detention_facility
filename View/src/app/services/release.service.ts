import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { SmartDelivery } from '../models/smartdelivery';
import { Delivery } from '../models/delivery';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class ReleaseService {
  private DeliveryUrl = 'http://localhost:58653/api/release';
  constructor(private http: HttpClient) { }

  getSmartDelivery (detaineeID, detentionID): Observable<SmartDelivery> {
    return this.http.get<SmartDelivery>(`${this.DeliveryUrl}/GetSmartDeliveryByIDs/${detaineeID}/${detentionID}`)
      .pipe(  
        map(x => x),
        catchError(this.handleError<SmartDelivery>('', ))
      );
  }

  getsmartDeliverysByIDs(DetaineeID: number, DetentionID: number) {
    const url = `${this.DeliveryUrl}/GetDeliveryByIDs/${DetaineeID}/${DetentionID}`;
    return this.http.get(url);    
  }

  getReleaseByIDs (detaineeID, detentionID): Observable<Delivery> {
    return this.http.get<Delivery>(`${this.DeliveryUrl}/GetReleaseByIDs/${detaineeID}/${detentionID}`)
      .pipe(  
        map(x => x),
        catchError(this.handleError<Delivery>('', ))
      );
  }

  getDeliveryByID(id: number): Observable<Delivery> {   
    return this.http.get <Delivery>(`${this.DeliveryUrl}/GetDelivery/${id}`);
  }

  getsmartDeliverys() { 
    return this.http.get(`${this.DeliveryUrl}/GetDeliveries`);
  }

  createDelivery(delivery: Delivery) {
    return this.http.post(`${this.DeliveryUrl}/InsertDelivery`, delivery);
  }

  updateDelivery(delivery: Delivery) {
    return this.http.put(`${this.DeliveryUrl}/UpdateDelivery/${delivery.deliveryID}`, delivery);
  }

  deleteDelivery(deliveryID: number) {
    return this.http.delete(`${this.DeliveryUrl}/DeleteDelivery/${deliveryID}`);
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {    
      console.error(error); 
      return of(result as T);};
  }       

}