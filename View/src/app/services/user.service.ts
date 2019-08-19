import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models/user';
import { SmartEmployee } from '../models/smartemployee';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({ providedIn: 'root' })
export class UserService {

    private userUrl = 'http://localhost:58653/api/account'; 

    constructor(private http: HttpClient) { }

        getUserByID(id: number){           
            return this.http.get(`${this.userUrl}/GetUser/${id}`);
        }

        createUser(user: User) { 
          console.log(user)
          return this.http.post(`${this.userUrl}/RegisterUser`, user);
        }

        getUsers() {          
            return this.http.get(`${this.userUrl}/GetUsers/`);    
        }

        updateUser(user: User) {
            return this.http.put(`${this.userUrl}/UpdateUser/${user.userID}`, user);
        }

        deleteUser(id: number) {
            return this.http.delete(`${this.userUrl}/DeleteUser/${id}`);
        }

        searchUsers(term: string): Observable<SmartEmployee[]> {
            if (!term.trim()) {
              return of([]);
            }
            return this.http.get<SmartEmployee[]>(`${this.userUrl}/GetEmploy?term=${term}`).pipe(            
              catchError(this.handleError<SmartEmployee[]>('searchEmployees', []))
            );
        }
        
        private handleError<T> (operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
            };
        }  
    }