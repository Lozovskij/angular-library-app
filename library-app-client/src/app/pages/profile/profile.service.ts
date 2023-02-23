import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BookInstance, BookInstanceStatus } from '../../shared/models/books';
import { Patron } from '../../shared/models/patron';

@Injectable({
    providedIn: 'root'
})
export class ProfileService {

    constructor(private http: HttpClient) { }

    getPatron(): Observable<PatronApi> {
        return this.http.get<PatronApi>('/api/patron');
    }

    getPatronBooks(): Observable<PatronBooksApi> {
        return this.http.get<PatronBooksApi>('/api/patron/books');
    }
}

export interface PatronApi {
    id: number;
    firstName: string;
    lastName: string;
    cardNumber: string;
}

export interface PatronBooksApi {
    bookInstances: PatronBookApi[];
}

export interface PatronBookApi {
    id: number;
    bookId: number;
    title: string;
    status: BookInstanceStatus;
}