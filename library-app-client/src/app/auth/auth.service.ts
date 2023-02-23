import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, Subscription } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _subscriptions: Subscription[] = [];

    constructor(private http: HttpClient) { }

    ngOnDestroy() {
        this._subscriptions.forEach(sub => sub.unsubscribe());
    }

    //returns token
    loginRandomDemoPatron(): Observable<string> {
        return this.http.post('/api/demo/create-and-login-demo-patron', {}, {
            responseType: 'text'
        })
    }

    loginDemoPatron(demoId: number): Observable<string> {
        const headers = new HttpHeaders().set('Content-Type', 'text/plain; charset=utf-8');
        return this.http.get(`/api/demo/login-demo-patron/${demoId}`, { headers, responseType: 'text' });
    }

    // logout() {
    //   localStorage.removeItem('USER_TYPE');
    //   return of(true);
    // }
}
