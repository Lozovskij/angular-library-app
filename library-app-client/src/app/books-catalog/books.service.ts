import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../temp-data/books';

@Injectable({
    providedIn: 'root'
})
export class BooksService {

    constructor(private http: HttpClient) { }

    getBooks(): Observable<Book[]> {
        return this.http.get<Book[]>('/api/books');
    }

    getBook(id: number): Observable<Book> {
        return this.http.get<Book>(`/api/books/${id}`, {
            params: { id: id.toString() }
        });
    }
}