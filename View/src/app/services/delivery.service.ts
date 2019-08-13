import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { SmartDelivery } from '../models/smartdelivery';
import { Delivery } from '../models/delivery';
import { DatePipe } from '@angular/common';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({ providedIn: 'root' })
export class DeliveryService {
  private DeliveryUrl = 'http://localhost:58653/api/delivery';
  constructor(private http: HttpClient,
    public datepipe: DatePipe) { }

  getSmartDelivery (detaineeID, detentionID): Observable<SmartDelivery> {
    return this.http.get<SmartDelivery>(`${this.DeliveryUrl}/GetSmartDeliveryByIDs/${detaineeID}/${detentionID}`)
      .pipe(  
        map(x => x),
        catchError(this.handleError<SmartDelivery>('', ))
      );
  }

  getSmartDeliveriesByDate(date: Date): Observable<SmartDelivery[]> {  
    let latest_date = this.datepipe.transform(date, 'yyyy-MM-dd');     
    console.log(latest_date)
    //date.toString;
    
    var json = JSON.stringify(latest_date);
    return this.http.post<SmartDelivery[]>(`${this.DeliveryUrl}/GetSmartDeliveriesByDate/`, json, httpOptions).pipe(            
      catchError(this.handleError<SmartDelivery[]>('searchDetentions', []))
    );
  }

  getsmartDeliverysByIDs(DetaineeID: number, DetentionID: number) {
    const url = `${this.DeliveryUrl}/GetDeliveryByIDs/${DetaineeID}/${DetentionID}`;
    return this.http.get(url);    
  }

  getDelivery (detaineeID, detentionID): Observable<Delivery> {
    return this.http.get<Delivery>(`${this.DeliveryUrl}/GetDeliveryByIDs/${detaineeID}/${detentionID}`)
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