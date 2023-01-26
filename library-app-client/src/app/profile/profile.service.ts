import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book, BookInstance } from '../temp-data/books';
import { Patron } from '../temp-data/user';

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
