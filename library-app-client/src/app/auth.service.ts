import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _subscriptions : Subscription[] = [];

  constructor(private http: HttpClient) { }

  ngOnDestroy() {
    this._subscriptions.forEach(sub => sub.unsubscribe()) ;
  }

  login(value: string) {
    const sub = this.http.get<any>('/api/weatherforecast')
      .subscribe(res => console.log(res));
    this._subscriptions.push(sub);

    localStorage.setItem('USER_TYPE', value);
    return of(true);
  }

  logout() {
    localStorage.removeItem('USER_TYPE');
    return of(true);
  }
}
