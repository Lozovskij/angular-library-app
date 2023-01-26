import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookInstance } from '../shared/models/books';
import { Patron } from '../shared/models/patron';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private http: HttpClient) { }

    getPatronProfile(): Observable<PatronProfile> {
        return this.http.get<PatronProfile>('/api/user/patron-profile');
    }
}

export interface PatronProfile {
    patron: Patron;
    books: BookInstance[];
    //checkedOutBooks: Book[];
}
