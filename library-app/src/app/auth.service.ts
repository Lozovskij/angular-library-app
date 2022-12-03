import { Injectable } from '@angular/core';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  login(value: string) {
    localStorage.setItem('USER_TYPE', value);
    return of(true);
  }

  logout() {
    localStorage.removeItem('USER_TYPE');
    return of(true);
  }
}
